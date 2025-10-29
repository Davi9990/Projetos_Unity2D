using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AnimaAgua : MonoBehaviour
{
        [Header("Movimento da Textura")]
        public Vector2 scrollDirection = new Vector2(1f, 0f); // direção e velocidade do scroll
        public float scrollSpeed = 0.5f;

        [Header("Flutuação em Y")]
        public float floatAmplitude = 0.5f; // altura máxima da flutuação
        public float floatSpeed = 2f;       // velocidade do sobe-desce

        private Renderer rend;
        private Vector3 startPos;
        private float offsetX, offsetY;

        void Start()
        {
            rend = GetComponent<Renderer>();
            startPos = transform.position;
        }

        void Update()
        {
            // --- Movimento da textura (scroll) ---
            offsetX += scrollDirection.x * scrollSpeed * Time.deltaTime;
            offsetY += scrollDirection.y * scrollSpeed * Time.deltaTime;

            rend.material.mainTextureOffset = new Vector2(offsetX, offsetY);

            // --- Movimento flutuante em Y ---
            float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
            transform.position = new Vector3(startPos.x, newY, startPos.z);
        }
    }