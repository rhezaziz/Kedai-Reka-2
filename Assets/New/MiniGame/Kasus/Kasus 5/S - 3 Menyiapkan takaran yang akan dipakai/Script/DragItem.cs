using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace MiniGame5_6{

    public class DragItem : MonoBehaviour
    {

        public Placement place;
        public Takaran takaran;

        int tempSorting;

        SpriteRenderer sprite;

        Vector2 posAwal;
        void Start(){
            posAwal = transform.position;
            sprite = GetComponent<SpriteRenderer>();
            tempSorting = sprite.sortingOrder;
        }

        void OnMouseDown(){
            sprite.sortingOrder = 5;
            FindObjectOfType<Manager>().itemOnDrag(gameObject);
        }

        
        void OnMouseDrag(){
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }

        public void Kembali(){
            place = null;
            gameObject.SetActive(true);
            transform.DOMove(posAwal, 1f).OnComplete(()=>{
                    sprite.sortingOrder = tempSorting;
            });
        }


        void OnMouseUp(){
            

            if(place){
                place.onPlacement(this);
                
                gameObject.SetActive(false);
                FindObjectOfType<Manager>().checkJawaban(place.takaran == takaran);
                place.hasil = place.takaran == takaran;
            }else{
                transform.DOMove(posAwal, 1f).OnComplete(()=>{
                    sprite.sortingOrder = tempSorting;
                });
                
            }
            FindObjectOfType<Manager>().itemOnDrag(null);
        }
    }
}
