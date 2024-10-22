using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;


namespace MiniGame5_6{
    public class Placement : MonoBehaviour
    {

        public GameObject temp;
        public bool active;
        public float jarak;

        public GameObject obj;

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
                    temp.GetComponent<DragItem>().place = null;
                
                    isCloseEnoughToDrop = false;
                    obj.SetActive(false);
                    isClose = false;
                
                }
                //reset tint color
                //GetComponent<Renderer>().material.color = new Color(1, 1, 1);
            }   
        }

        public void onPlacement(){
            obj.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
            active = false;
        }
    }
}
