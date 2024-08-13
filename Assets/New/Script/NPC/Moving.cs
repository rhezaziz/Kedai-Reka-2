using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru
{
    public class Moving : MonoBehaviour
    {
        NPC_Controller controller;
        Animator anim;
        SpriteRenderer sprite;
        Rigidbody rb;
        CollisionObject check = new CollisionObject();
        //Value
        public float speed = 5f;
        [SerializeField] private bool Kanan = false, Maju = false;
        public float timer;
        string direction = "Kanan";
        private void Start()
        {
            controller = GetComponent<NPC_Controller>();
            anim = GetComponentInChildren<Animator>();
            sprite = GetComponentInChildren<SpriteRenderer>();
            rb = GetComponent<Rigidbody>();
            timer = 10f;
        }

        private void Update()
        {
            move();
        }

        public void mulaiJalan()
        {
            timer = 0;
            direction = dirValue();
        }

        float value(string temp)
        {
            return temp == _direction(direction) && direction != ""? tujuan().x + tujuan().z : 0;
        }
        public void move()
        {
            //float valueDir = Kanan ? 1f : -1f;
            //Vector3 dir = new Vector3(valueDir, 0f, 0f);
            timer += Time.deltaTime;
            // value = 0;

            //float inputDir =  ?  : "Yinput";

            anim.SetFloat("Xinput", value("Vertical"));
            anim.SetFloat("Yinput", value("Horizontal"));

            if (timer < 1f)
            {
               

                //Debug.Log($"{check.check(transform.position)}");

                rb.MovePosition(rb.position + tujuan() * speed * Time.fixedDeltaTime);
                anim.SetBool("Jalan", true);
                //Debug.Log(inputDir);
                timer = check.collision(transform.position, direction) ? 1f : timer;

                sprite.flipX = direction == "Kanan" || direction == "Kiri" ? Kanan : !Kanan;
            }
            else if(timer > 1f && timer < 1.1f)
            {
                anim.SetBool("Jalan", false);
                
                if (Kanan)
                    Kanan = false;
                else
                    Kanan = true;

                controller.currentCondition(animasi.Idle);
            }
        }

        Vector3 tujuan()
        {
            return check.getDir(direction);
        }

        string _direction(string temp)
        {
            if (temp == "Kiri" || temp == "Kanan")
                return "Vertical";

            return "Horizontal";
        }

        string dirValue (string temp = "")
        {
            if(temp == "")
                temp = dirName[Random.Range(0, dirName.Length - 1)];
            
            if(temp == "Vertical")
            {
                if (Kanan)
                {
                    Kanan = false; 

                    return "Kanan";

                }
                else
                {
                    Kanan = true;
                    return "Kiri";
                }
            }
            else
            {
                if (Maju)
                {
                    Maju = false;
                    return "Depan";
                }
                else
                {
                    Maju = true;
                    return "Belakang";
                }
            }
        }

        string[] dirName = new string[]
        {
            "Vertical",
            "Horizontal",
            "Vertical",
            "Horizontal",
            "Horizontal",
            "Vertical"
        };
    }
}