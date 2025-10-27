using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 10f;
    public float lateralSpeed = 8f; // sensitivity for accelerometer/tilt
    public float maxLateral = 4.5f; // confine player in X
    public float jumpForce = 6f;
    public LayerMask groundMask;
    public Transform groundCheck;
    public float groundCheckRadius = 0.25f;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public Transform projectileSpawn;
    public float projectileForce = 20f;
    public float projectileLifetime = 3f;

    [Header("Rotate / Invincible")]
    public float rotateDuration = 1.0f;
    public float invincibleDuration = 1.2f;
    public float rotateAngularSpeed = 720f; // degrees per second while spinning

    Rigidbody rb;
    bool isGrounded;
    bool isInvincible = false;
    Vector2 lastTouchPos;
    Vector2 startTouchPos;
    float minSwipeDistance = 50f; // pixels - requires Screen.dpi normalizing if necessary

    // For two-finger rotation detection
    float lastTwoTouchAngle = 0f;
    bool twoFingerActive = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

        // forward constant movement
        Vector3 vel = rb.velocity;
        vel.z = forwardSpeed;
        rb.velocity = new Vector3(vel.x, vel.y, vel.z);

        // lateral control: accelerometer X (portrait)
        Vector3 acc = Input.acceleration;
        float targetX = transform.position.x + acc.x * lateralSpeed * Time.deltaTime * 60f; // scaled
        targetX = Mathf.Clamp(targetX, -maxLateral, maxLateral);
        // smooth move toward targetX by setting velocity
        float deltaX = (targetX - transform.position.x) / Time.fixedDeltaTime;
        rb.velocity = new Vector3(Mathf.Lerp(rb.velocity.x, deltaX, 0.2f), rb.velocity.y, rb.velocity.z);

        HandleTouchInput();
    }

    #region Jump (New Input System friendly)
    // This method can be wired as callback via PlayerInput or called from UI Button
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) TryJump();
    }

    // Public method so UI Button can call it
    public void JumpFromButton() => TryJump();

    void TryJump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }
    #endregion

    #region Touch & Swipe & Rotate (mixed Touch API)
    void HandleTouchInput()
    {
        int touchCount = Input.touchCount;

        // Single touch: swipe detection (shoot)
        if (touchCount == 1)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == UnityEngine.TouchPhase.Began)
            {
                startTouchPos = t.position;
            }
            else if (t.phase == UnityEngine.TouchPhase.Ended)
            {
                Vector2 end = t.position;
                Vector2 diff = end - startTouchPos;
                if (diff.magnitude > minSwipeDistance)
                {
                    FireProjectile(diff.normalized);
                }
            }
            twoFingerActive = false;
        }
        else if (touchCount >= 2)
        {
            // Two-finger twist (rotate) detection
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            Vector2 p0 = t0.position;
            Vector2 p1 = t1.position;

            float angle = Mathf.Atan2(p1.y - p0.y, p1.x - p0.x) * Mathf.Rad2Deg;

            if (!twoFingerActive)
            {
                twoFingerActive = true;
                lastTwoTouchAngle = angle;
            }
            else
            {
                float delta = Mathf.DeltaAngle(lastTwoTouchAngle, angle);
                if (Mathf.Abs(delta) > 20f) // threshold to consider a rotate gesture
                {
                    lastTwoTouchAngle = angle;
                    StartCoroutine(SpinAndInvincible());
                }
            }
        }
        else
        {
            twoFingerActive = false;
        }
    }

    void FireProjectile(Vector2 swipeDir)
    {
        if (projectilePrefab == null || projectileSpawn == null) return;

        // Convert swipe direction on screen to world direction roughly forward + lateral
        Vector3 worldDir = new Vector3(swipeDir.x, swipeDir.y * 0.0f, 1f).normalized;
        GameObject p = Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity);
        Rigidbody pr = p.GetComponent<Rigidbody>();
        if (pr != null)
        {
            Vector3 force = (transform.forward * projectileForce) + new Vector3(worldDir.x * projectileForce, 0f, 0f);
            pr.AddForce(force, ForceMode.VelocityChange);
        }
        Destroy(p, projectileLifetime);
    }
    #endregion

    #region Rotate & Invincible Coroutine
    IEnumerator SpinAndInvincible()
    {
        if (isInvincible) yield break;

        float t = 0f;

        // rotate visual while invincible
        isInvincible = true;
        float spinTime = rotateDuration;
        while (t < spinTime)
        {
            float step = rotateAngularSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, step, Space.Self);
            t += Time.deltaTime;
            yield return null;
        }

        // keep invincible for a short time
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
    }
    #endregion

    #region Collisions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!isInvincible)
            {
                GameManager.Instance.PlayerHit();
            }
            // optional: push back or play effect
        }
    }
    #endregion

    // Public accessor for invincibility state
    public bool IsInvincible() => isInvincible;
}
