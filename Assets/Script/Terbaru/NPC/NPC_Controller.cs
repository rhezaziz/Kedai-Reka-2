//using AI_NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPC_Controller : MonoBehaviour
    {
        public List<Interaksi> npc_interaksi;
        [Header("Kondisi")]
        public bool isMove;
        public bool haveKegiatan;
        public bool haveObrol;
        public bool canHorizontal;
        public animasi state;
        //Component
        Animator anim;
        //Patroli moving;
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
              //  moving = gameObject.AddComponent<Patroli>();
                //moving.Horizontal = canHorizontal;

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
            switch (temp)
            {
                case animasi.Idle:
                    Idle();
                    break;

                case animasi.Walking:
                    Walking();
                    break;

                case animasi.Kegiatan:

                    Kegiatan();
                    break;

                case animasi.Ngobrol:
                    Speech();
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

        #region Interaksi
        void Idle()
        {
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
        }

        void Walking()
        {
            state = animasi.Walking;

            if (isMove)
            {
               // moving.mulaiJalan();
                return;
            }
            randomState();
        }

        void Speech()
        {
            if (haveObrol)
            {
                state = animasi.Ngobrol;
                resetAnimor();

                interupt = true;
                couting = false;

                anim.SetBool("Ngomong", true);
                return;
            }
        }

        void Kegiatan()
        {
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
        }

        #endregion
    }

    public enum animasi
    {
        Idle,
        Walking,
        Kegiatan,
        Ngobrol
    }
}
