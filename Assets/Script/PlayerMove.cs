using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    
    // Start is called before the first frame update

    public float speed, groundDist;

    public LayerMask terrantMask;

    SpriteRenderer sp;
    Animator anim;

    float temp;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        //RaycastHit hit;
        /*
        if (PlayerState._state == PlayerState.state.Berjalan)
        {
            
        }
        */
    }

    void Move()
    {
        Vector3 castPos = transform.position;

        temp = speed;

       

        Vector3 movePos = transform.position;
        // movePos.y = hit.point.y + groundDist;
        transform.position = movePos;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y);
        rb.velocity = moveDir * temp;

        //Debug.Log(y);
        if (x != 0)
        {
            anim.SetFloat("Speed", Mathf.Abs(x));
        }
        else if (y != 0)
        {
            if(y > 0.25f)
            {
                anim.SetFloat("Atas", y);
            }
            else if(y < 0.25f)
            {
                anim.SetFloat("Bawah", y);
            }
            
        }

        if (x != 0 && x < 0)
        {
            sp.flipX = false;
        }
        else if (x != 0 && x > 0)
        {
            sp.flipX = true;
            
        }
    }
}


