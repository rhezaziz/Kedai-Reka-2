using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

//using System.Numerics;
using UnityEngine;


namespace MiniGame5_6{
    public class Placement : MonoBehaviour
    {

        public GameObject temp;
        public bool active;
        public float jarak;

        public GameObject obj;

        public bool hasil;

        bool isCloseEnoughToDrop;

        public Takaran takaran;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if(temp && active)
                checkDistanceItem();
        }
        bool isClose;
        
        void checkDistanceItem()
        {
            float distanceItem = Vector3.Distance(transform.position, temp.transform.position);

            //bool isClose = false;
            //frameDistanceToDelivery = Vector3.Distance(requestBubble.transform.position, deliveryPlate.transform.position);
            //print(gameObject.name + " distance to candy is: " + distanceToDelivery + ".");

            //Hardcoded integer for distance.
            if (distanceItem < jarak )
            {
               // Debug.Log("Dekat");
                isClose = true;
                isCloseEnoughToDrop = true;
                obj.transform.SetParent(transform);
                obj.transform.localPosition = new Vector2(0f, obj.transform.localPosition.y);
                obj.SetActive(true);
                temp.GetComponent<DragItem>().place = gameObject.GetComponent<Placement>();
                //tint color
                //GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f);
            }
            else
            {
                if(isClose){
                    if(obj != null)
                        obj.SetActive(false);
                    temp.GetComponent<DragItem>().place = null;
                
                    isCloseEnoughToDrop = false;
                    
                    isClose = false;
                
                }
                //reset tint color
                //GetComponent<Renderer>().material.color = new Color(1, 1, 1);
            }   
        }

        public void resetGame(){
            transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .3f);
            transform.GetChild(1).gameObject.SetActive(false);
            
            item.Kembali();
            item = null;
            active = true;
            hasil = false;
        }

        public void checkHasil(Sprite temp){
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = temp;
            transform.GetChild(0).DOScale(Vector3.one * .75f, 1f).OnComplete(() => {
                transform.GetChild(0).DOScale(Vector3.one  * .5f, .25f);
            });
        }
        public DragItem item;
        public void onPlacement(DragItem temp){
            //Debug.Log("On Place");
            item = temp;
            obj.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
            active = false;
        }
    }
}
