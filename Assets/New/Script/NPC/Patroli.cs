
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Terbaru
{

    public class Patroli : MonoBehaviour
    {
        public float distance = 7.5f;
        public bool Horizontal;

        [SerializeField] List<Vector3> Coordinate = new List<Vector3>();

        NPC_Controller controller;
        Animator anim;
        SpriteRenderer sprite;
        Rigidbody rb;
        CollisionObject check = new CollisionObject();

        //Value
        public float speed = 5f;
        public float timer;
        Vector3 tempCoordinate;
        bool isMove = false;

        private void Start()
        {
            controller = GetComponent<NPC_Controller>();
            anim = GetComponentInChildren<Animator>();
            sprite = GetComponentInChildren<SpriteRenderer>();
            rb = GetComponent<Rigidbody>();
            timer = 10f;


            inisiasiDir();
            //initialCoordinate();
        }

        private void Update()
        {
            if(isMove)
                jalan();
        }

        #region Horizontal dan Vertical

        void initialCoordinate()
        {
            Coordinate.Add(transform.position);
            Coordinate.Add(new Vector3(setCoordinateValue(transform.position.x, 1), transform.position.y, transform.position.z));
            Coordinate.Add(new Vector3(setCoordinateValue(transform.position.x, -1), transform.position.y, transform.position.z));

            if (Horizontal)
            {
                Coordinate.Add(new Vector3(transform.position.x, transform.position.y, setCoordinateValue(transform.position.z, -1)));
                Coordinate.Add(new Vector3(transform.position.x, transform.position.y, setCoordinateValue(transform.position.z, 1)));
            }

            for (int i = 0; i < Coordinate.Count; i++)
            {
                int indexRandom = Random.Range(0, Coordinate.Count - 1);
                Vector3 temp = Coordinate[i];
                Coordinate[i] = Coordinate[indexRandom];
                Coordinate[indexRandom] = temp;
            }

            tempCoordinate = dir();
            //tempCoordinate = Coordinate[0];
        }
        private void OnEnable()
        {
            Debug.Log("Enabled");
        }

        float setCoordinateValue(float temp, int value)
        {
            return temp + (distance / 2) * value;
        }

        void move()
        {
            
            anim.SetFloat("Xinput", valueDir(transform.position.x, Coordinate[0].x));
            anim.SetFloat("Yinput", valueDir(transform.position.y, Coordinate[0].y));
            if (!inCoordinate())
            {
                rb.MovePosition(rb.position + tempCoordinate * speed * Time.fixedDeltaTime);
                anim.SetBool("Jalan", true);

                sprite.flipX = valueDir(transform.position.x, Coordinate[0].x) > 0 ? true : false;
            }
            else
            {
                anim.SetBool("Jalan", false);

                Coordinate.Clear();
                //initialCoordinate();
                controller.currentCondition(animasi.Idle);
                isMove = false;
            }
        }

        bool inCoordinate()
        {
            return transform.position == Coordinate[0];
        }

        Vector3 dir()
        {
            return new Vector3(valueDir(Coordinate[0].x, transform.position.x), 0, valueDir(Coordinate[0].z, transform.position.z));
        }

        int valueDir(float start, float end)
        {
            int dir = (int)(end - start);
            if(dir != 0)
            {
                if (dir > 0)
                    return -1;
                else
                    return 1;
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region Vertical

        void inisiasiDir()
        {
            Coordinate.Add(transform.position);
            Coordinate.Add(new Vector3(transform.position.x + distance / 2, transform.position.y, transform.position.z));
        }
        int indexDir = 1;
        void jalan()
        {
            timer += Time.deltaTime;
            
            anim.SetFloat("Xinput", KananKiri());
            if (!_inCoordinate() && !GetComponent<NPC_Controller>().interupt)
            {
                rb.MovePosition(rb.position + new Vector3(KananKiri(), 0f, 0f) * speed * Time.fixedDeltaTime);
                anim.SetBool("Jalan", true);
                
                sprite.flipX =  KananKiri() < 0;
            }
            else
            {
                anim.SetBool("Jalan", false);

                indexDir = transform.position == Coordinate[0] ? 1 : 0;
                controller.currentCondition(animasi.Idle); 
                isMove = false;
            }
        }
        bool _inCoordinate()
        {
            return transform.position == Coordinate[indexDir] || Vector3.Distance(transform.position, Coordinate[indexDir]) < 0.1f;
        }
        int KananKiri()
        {
            return indexDir > 0 ? 1 : -1;
        }

        #endregion

        public void mulaiJalan()
        {
            isMove = true;
        }
    }
}