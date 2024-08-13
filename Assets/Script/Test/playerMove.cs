using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru
{

    public class playerMove : MonoBehaviour
    {
        Animator anim;
        SpriteRenderer sp;
        Rigidbody rb;
        Vector3 Movement;
        WorldPos sorting = new WorldPos();
        /*
    public List<Mimik> mimik; 
    public List<Mimik> mimikPlayer;*/
        //Value
        public float moveSpeed = 5f;
        public bool move;
        private void Start()
        {
            anim = GetComponentInChildren<Animator>();
            sp = GetComponentInChildren<SpriteRenderer>();
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (move)
                Move();
        }

        void Move()
        {
            Movement.x = Input.GetAxisRaw("Horizontal");
            Movement.z = Input.GetAxisRaw("Vertical");

            rb.MovePosition(rb.position + Movement * moveSpeed * Time.fixedDeltaTime);

            if (Movement.x != 0 || Movement.z != 0)
            {
                anim.SetBool("Jalan", true);
                anim.SetFloat("Xinput", Movement.x);
                anim.SetFloat("Yinput", Movement.z);

                if (Movement.x != 0 && Movement.x < 0)
                {
                    sp.flipX = false;
                }
                else if (Movement.x != 0 && Movement.x > 0)
                {
                    sp.flipX = true;
                }

                sp.sortingOrder = sorting.valueLayer(transform.position.z);

            }
            else if (Movement.x == 0 && Movement.z == 0)
            {
                anim.SetBool("Jalan", false);
            }
        }
    }
}
