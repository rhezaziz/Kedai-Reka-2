using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terbaru
{
    public class NPC_Controller : MonoBehaviour
    {
        //Kondisi
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
        bool couting;

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

        private void Start()
        {
            currentCondition(animasi.Idle);
        }

        private void Update()
        {
            if (couting)
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
                    couting = true;
                    interupt = false;

                    if (timer > 5)
                    {
                        timer = 0;
                        couting = false;
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


                    couting = true;

                    anim.SetBool("Kegiatan", true);

                    if (timer > 5)
                    {
                        anim.SetBool("Kegiatan", false);
                        if (timer > 7)
                        {
                            timer = 0;

                            couting = false;
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
                        couting = false;
                        
                        anim.SetBool("Ngomong", true);
                        return;
                    }
                    
                    //randomState();
                    break;
            }
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
        Ngobrol
    }
}