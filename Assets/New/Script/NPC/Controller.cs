using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terbaru
{
    public class NPC_Controller : MonoBehaviour
    {
        //Kondisi
        public bool kanan;
        public bool isMove;
        public bool haveKegiatan;
        public bool haveObrol;
        public bool canHorizontal;
        public animasi state;
        //Component
        Animator anim;
        Patroli moving;
        SpriteRenderer sprite;

        float speed;
        //Parameter
        public bool interupt = false;
        int jumlahState;
        bool counting;

        WorldPos sorting = new WorldPos();
        float timer;
        private void Awake()
        {
            jumlahState = System.Enum.GetNames(typeof(animasi)).Length;

            if (isMove)
            {
                moving = gameObject.AddComponent<Patroli>();
                moving.Horizontal = canHorizontal;
                
            }

            anim = GetComponentInChildren<Animator>();
            sprite = GetComponentInChildren<SpriteRenderer>();
        }

        void mulai(){
            GetComponent<Rigidbody>().isKinematic = true;

            if(moving)
                moving.inisiasiDir();
        }

        private void Start()
        {
            currentCondition(animasi.Idle);
            Invoke("mulai", 0.5f);
        }

        private void Update()
        {
            if (counting)
            {
                timer += Time.deltaTime;
                currentCondition(state);
            }

            sprite.sortingOrder = sorting.valueLayer(transform.position.z);

        }

        public void currentCondition(animasi temp)
        {
            switch(temp)
            {
                case animasi.Idle:
                    state = animasi.Idle;
                    
                    resetAnimor();
                    counting = true;
                    interupt = false;

                    if (timer > 5)
                    {
                        timer = 0;
                        counting = false;
                        randomState();
                    }

                    break;

                case animasi.Walking:
                    state = animasi.Walking;

                    if (isMove)
                    {
                        moving.mulaiJalan();
                        return;
                    }
                    randomState();
                    break;

                case animasi.Kegiatan:

                    state = animasi.Kegiatan;


                    counting = true;

                    anim.SetBool("Kegiatan", true);

                    if (timer > 5)
                    {
                        anim.SetBool("Kegiatan", false);
                        if (timer > 7)
                        {
                            timer = 0;

                            counting = false;
                            randomState();
                        }
                    }
                    break;

                case animasi.Ngobrol:
                    if (haveObrol)
                    {
                        state = animasi.Ngobrol;
                        resetAnimor();
                        
                        interupt = true;
                        counting = false;
                        
                        anim.SetBool("Ngomong", true);
                        return;
                    }
                    
                    //randomState();
                    break;
            }
        }

        public void selectedQuest(bool flipX){
            counting = false;
            
            anim.SetBool("Kegiatan", false);
            currentCondition(animasi.Ngobrol);
            sprite.flipX = flipX;
        }
        void randomState()
        {
            int index = Random.Range(1, jumlahState - 1);
            //Debug.Log(index);
            currentCondition(getAnimasi(index));
        }

        private animasi getAnimasi(int index)
        {
            return (animasi)index;
        }

        void resetAnimor()
        {
            foreach (AnimatorControllerParameter parameter in anim.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Bool)
                    anim.SetBool(parameter.name, false);
            }
        }
    }

    public enum animasi
    {
        Idle,
        Walking,
        Kegiatan,
        Ngobrol,
    }
}