using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MiniGame4_2{
    public class Kunci : MonoBehaviour
    {
        Vector2 posAwal;
        GameObject mobil;

        public bool value;
        // Start is called before the first frame update
        void Start()
        {
            posAwal = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void OnMouseDrag(){
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePos.x, mousePos.y);

        }

        void OnMouseUp(){
            if(mobil == null || !value){
                transform.position = posAwal;
                mobil = null;
                return;
            }

            mobil.GetComponent<Mobil>().menyalakanMobil();
            gameObject.SetActive(false);

        }


        void OnTriggerEnter2D(Collider2D coll){
            if(coll.CompareTag("Mobil")){
                mobil = coll.gameObject;
            }
        }
        
        void OnTriggerExit2D(Collider2D coll){
                if(coll.CompareTag("Mobil")){
                mobil = null;
            }
        }
    }
}