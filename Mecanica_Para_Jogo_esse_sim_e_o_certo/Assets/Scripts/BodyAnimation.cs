using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodtAnimation : MonoBehaviour
{
    Animator anim2;

    bool strokingAllowed = false;
    void Start()
    {
        anim2 = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch(touch.phase)
            {
                case TouchPhase.Began:
                if(GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                {
                    strokingAllowed = true;
                }
                break;

                case TouchPhase.Moved:
                if(GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && strokingAllowed)
                {
                    anim2.SetBool("IsBodyStroked", true);
                }

                if(GetComponent<Collider2D>() != Physics2D.OverlapPoint(touchPos))
                {
                    anim2.SetBool("IsBodyStroked", false);
                }
                break;

                case TouchPhase.Stationary:
                anim2.SetBool("IsBodyStroked", false);
                break;

                case TouchPhase.Ended:
                anim2.SetBool("IsBodyStroked", false);
                strokingAllowed = false;
                break;
            }
        }
    }
}
