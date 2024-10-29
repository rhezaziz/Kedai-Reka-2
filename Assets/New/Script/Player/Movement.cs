using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Terbaru
{
    public class Movement : MonoBehaviour
    {
        public float minZ = -150f;
        public float maxZ = -120f;
        Animator anim;
        SpriteRenderer sp;
        Rigidbody rb;
        Vector3 Direction;
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
            else
            {
                if (anim.GetBool("Jalan"))
                    anim.SetBool("Jalan", false);

            }

        }
        private Vector3 currentDirection = Vector3.zero;
        void Move()
        {
            Direction.x = Mathf.RoundToInt(CrossPlatformInputManager.GetAxis("Horizontal"));
            Direction.z = Mathf.RoundToInt(CrossPlatformInputManager.GetAxis("Vertical"));
            
            Vector3 currentPos = rb.position;
            currentDirection.z = Mathf.Clamp(currentPos.z ,minZ, maxZ);
            Vector3 inputDirection = Direction;
            if (!inputDirection.Equals(Vector3.zero))
            {
                currentDirection = inputDirection;
                this.rb.velocity = currentDirection * moveSpeed;
                
                //rb.MovePosition(currentPos + currentDirection * moveSpeed * Time.fixedDeltaTime);
            }

            if (Direction.x != 0 || Direction.z != 0)
                {
                    //petunjukLine();
                    anim.SetBool("Jalan", true);
                    anim.SetFloat("Xinput", Direction.x);
                    anim.SetFloat("Yinput", Direction.z);

                    if (Direction.x != 0 && Direction.x < 0)
                    {
                        sp.flipX = false;
                    }
                    else if (Direction.x != 0 && Direction.x > 0)
                    {
                        sp.flipX = true;
                    }

                    sp.sortingOrder = sorting.valueLayer(transform.position.z);

                }
                else if (Direction.x == 0 && Direction.z == 0)
                {
                    anim.SetBool("Jalan", false);
                    currentDirection = Vector3.zero;
                    this.rb.velocity = Vector3.zero;
            
                }
            /*
            currentPos.z = Mathf.Clamp(currentPos.z ,minZ, maxZ);

            rb.MovePosition(currentPos + Direction * moveSpeed * Time.fixedDeltaTime);

            if (Direction.x != 0 || Direction.z != 0)
            {
                anim.SetBool("Jalan", true);
                anim.SetFloat("Xinput", Direction.x);
                anim.SetFloat("Yinput", Direction.z);

                if (Direction.x != 0 && Direction.x < 0)
                {
                    sp.flipX = false;
                }
                else if (Direction.x != 0 && Direction.x > 0)
                {
                    sp.flipX = true;
                }

                sp.sortingOrder = sorting.valueLayer(transform.position.z);

            }
            else if (Direction.x == 0 && Direction.z == 0)
            {
                anim.SetBool("Jalan", false);
            }

            */
        }
    }
}