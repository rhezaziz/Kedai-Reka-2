using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MoveTest : MonoBehaviour
{

    //public float minZ = -150f;
        //public float maxZ = -120f;
        public Animator anim;
        public bool animValue;
        SpriteRenderer sp;
        Rigidbody rb;
        Vector3 Direction;
        /// <summary>
        /// WorldPos sorting = new WorldPos();
        /// </summary>
        /*
    public List<Mimik> mimik; 
    public List<Mimik> mimikPlayer;*/

        public LineRenderer line;
        public Transform objectTujuan; 
        //Value
        public float moveSpeed = 5f;
        public float jarak;
        public bool move;
        private void Start()
        {
            //anim = GetComponentInChildren<Animator>();
            sp = GetComponentInChildren<SpriteRenderer>();
            rb = GetComponent<Rigidbody>();

            petunjukLine();
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


        void petunjukLine(){
            line.SetPosition(0, new Vector3(transform.position.x - jarak, -10f, transform.position.z - jarak));
            line.SetPosition(1, new Vector3(objectTujuan.position.x, -10f, objectTujuan.position.z));
        }


       
        private Vector3 currentDirection = Vector3.zero;
        void Move()
        {
            Direction.x = Mathf.RoundToInt(CrossPlatformInputManager.GetAxis("Horizontal"));
            Direction.z = Mathf.RoundToInt(CrossPlatformInputManager.GetAxis("Vertical"));
            // float x = CrossPlatformInputManager.GetAxis("Horizontal");
            // float z = CrossPlatformInputManager.GetAxis("Vertical");
            // int isDiagonal = Direction.x * Direction.z != 0 ? 0 : 1;
            // Debug.Log($"X : {x} -- Z : {z}");
            //movement.x = Input.GetAxisRaw("Horizontal");
            Vector3 currentPos = rb.position;
            Vector3 inputDirection = Direction;
            if (!inputDirection.Equals(Vector3.zero))
            {
                currentDirection = inputDirection;
                this.rb.velocity = currentDirection * moveSpeed;
                
                //rb.MovePosition(currentPos + currentDirection * moveSpeed * Time.fixedDeltaTime);
            }

            if (Direction.x != 0 || Direction.z != 0)
                {
                    petunjukLine();
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

                    //sp.sortingOrder = sorting.valueLayer(transform.position.z);

                }
                else if (Direction.x == 0 && Direction.z == 0)
                {
                    anim.SetBool("Jalan", false);
                    currentDirection = Vector3.zero;
                    this.rb.velocity = Vector3.zero;
            
                }
            

            
            //currentPos.z = Mathf.Clamp(currentPos.z ,minZ, maxZ);
            //Debug.Log(Direction);
            // rb.MovePosition(currentPos + Direction * moveSpeed * Time.fixedDeltaTime);

            // if (Direction.x != 0 || Direction.z != 0)
            // {
            //     if(animValue){

            //         anim.SetBool("Jalan", true);
            //         anim.SetFloat("Xinput", Direction.x);
            //         anim.SetFloat("Yinput", Direction.z);

            //     }
            //     // if (Direction.x != 0 && Direction.x < 0)
            //     // {
            //     //     //sp.flipX = false;
            //     // }
            //     // else if (Direction.x != 0 && Direction.x > 0)
            //     // {
            //     //     sp.flipX = true;
            //     // }

            //     //sp.sortingOrder = sorting.valueLayer(transform.position.z);

            // }
            // else if (Direction.x == 0 && Direction.z == 0)
            // {
            //     if(animValue)
            //         anim.SetBool("Jalan", false);
            // }
        }

        // void OnCollisionEnter(Collision col)
        // {
        //     if(col.gameObject.CompareTag("Noda")){
        //         Debug.Log("Kena");
        //         //currentDirection = new Vector3(0, transform.position.y, 0);
        //         //this.rb.velocity = new Vector3(0, transform.position.y, 0);
        //         currentDirection = Vector3.zero;
        //         this.rb.velocity = Vector3.zero;
        //     }
        // }

}
