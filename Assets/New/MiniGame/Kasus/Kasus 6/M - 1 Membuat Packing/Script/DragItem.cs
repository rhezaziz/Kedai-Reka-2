using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MiniGame6_1{
    public class DragItem : MonoBehaviour
    {

        [System.Serializable]
        public class posisiItem{
            public Vector3 Rotation;
            public Vector3 size;
        }

        public float sizeOnDrag;


        bool onDrag = false;
        bool isCloseEnoughToDrop = false;
        Vector2 posAwal;
        Vector2 rotAwal;
        Vector2 size;
        SpriteRenderer sprite;
        public SpriteRenderer Placement;


        
        public float jarak;


        void Start(){
            posAwal = transform.position;
            rotAwal = transform.eulerAngles;
            size = transform.localScale;

            sprite = GetComponent<SpriteRenderer>();
        }
        void OnMouseDown(){
            transform.DOLocalRotate(Vector3.zero, .25f);
            transform.DOScale(Vector3.one * sizeOnDrag, .25f);
            sprite.sortingOrder += 1;
            onDrag = true;
        }

        void OnMouseDrag(){
            var camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector2(camPos.x, camPos.y);

            checkDistancePlacement();
        }

        void OnMouseUp(){
            if(isCloseEnoughToDrop){
                Placement.color = new Color(1f,1f,1f,1f);
                gameObject.SetActive(false);
                FindObjectOfType<Manager>().itemInPlace(gameObject);
                return;
            }

            kembali();


        }

        void kembali(){
            transform.DOMove(posAwal,.25f);
            transform.DOLocalRotate(rotAwal, .25f);
            transform.DOScale(size, .25f).OnComplete(() =>{
                sprite.sortingOrder -= 1;
            });
            onDrag = false;
        }



        void checkDistancePlacement()
        {
            float distanceItem = Vector3.Distance(transform.position, Placement.transform.position);

            //bool isClose = false;
            //frameDistanceToDelivery = Vector3.Distance(requestBubble.transform.position, deliveryPlate.transform.position);
            //print(gameObject.name + " distance to candy is: " + distanceToDelivery + ".");

            //Hardcoded integer for distance.
            if (distanceItem < jarak )
            {
                
                // isClose = true;
                isCloseEnoughToDrop = true;
                // obj.transform.SetParent(transform);
                // obj.transform.localPosition = new Vector2(0f, obj.transform.localPosition.y);
                // obj.SetActive(true);
                // temp.GetComponent<DragItem>().place = gameObject.GetComponent<Placement>();
                //tint color
                //GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f);
                Placement.color = new Color(1f,1f,1f, .5f);
            }
            else
            {
                // if(isClose){
                //     temp.GetComponent<DragItem>().place = null;
                
                isCloseEnoughToDrop = false;
                //     obj.SetActive(false);
                //     isClose = false;
                
                // }
                //reset tint color
                Placement.color = new Color(0, 0, 0, .75f);
            }   
        }
    }

    
}
