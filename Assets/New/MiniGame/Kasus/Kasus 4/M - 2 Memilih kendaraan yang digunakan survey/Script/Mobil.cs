using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace MiniGame4_2{
    public class Mobil : MonoBehaviour
    {
        public Animator anim;
        public GameObject kunciTerpasang;

        public GameObject[] kuncis;
        void Start(){
            //anim = GetComponent<Animator>();
        }
        public void berhenti(){
            gameObject.SetActive(false);
            //transform.position = new Vector2(20f, 0f);
            Debug.Log("Selesai");
        }

        public void menyalakanMobil(){
            jalan();
            kunciTerpasang.SetActive(true);
            foreach(var kunci in kuncis)
                kunci.transform.parent.gameObject.SetActive(false);

            float timer = Random.Range(3f, 5f);
            GetComponent<Collider2D>().enabled = false; 
            Invoke("jalan", timer);
        }

        void jalan(){
            anim.SetTrigger("Mobil");

            FindObjectOfType<Manager>().gameOver();
        }
    }
}
