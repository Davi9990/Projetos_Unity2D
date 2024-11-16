using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAnimation : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
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
                    anim.SetBool("isHeadStroked", true);
                }
                break;

                case TouchPhase.Moved:

                if(GetComponent<Collider2D>() != Physics2D.OverlapPoint(touchPos))
                {
                    anim.SetBool("isHeadStroked", false);
                }
                break;

                case TouchPhase.Ended:

                anim.SetBool("isHeadStroked", false);
                break;
            }

        }
    }
}
