
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace MiniGame5_2{

    public class SelectedItem : MonoBehaviour
    {
        public UnityEvent extendAction;
        public float jarak;

        Vector2 posAwal;
        Collider2D col;
        SpriteRenderer spriteRender;

        int sorting;

        SpriteRenderer Trolli;
        bool onTrolli;

        void Awake(){
            posAwal = transform.position;
            col = GetComponent<Collider2D>();
            spriteRender = GetComponent<SpriteRenderer>();
            sorting = spriteRender.sortingOrder;
        }
        void Start()
        {
            extendAction.AddListener(kembali);
        }

        public void initialItem(GameObject temp){
            Trolli = temp.GetComponent<SpriteRenderer>();
            //temp = this.Trolli.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        }

        void OnMouseUp(){
            if(!onTrolli){
                kembali();
                return;
            }
            
            extendAction?.Invoke();
        }
        //SpriteRenderer temp;
        void OnMouseDrag(){
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spriteRender.sortingOrder = 10;

            transform.position = new Vector2(mousePos.x, mousePos.y);
            float distance = Vector2.Distance(transform.position, Trolli.transform.position);

            if(distance <= jarak){
                onTrolli = true;
                Trolli.GetComponent<SpriteRenderer>().color = new Color(Trolli.color.r, Trolli.color.g, Trolli.color.b, 1f);
            }else{
                
                onTrolli = false;
                Trolli.GetComponent<SpriteRenderer>().color = new Color(Trolli.color.r, Trolli.color.g, Trolli.color.b, 0f);
        
            }
        }

        void kembali(){
            transform.DOMove(posAwal, 1f).OnComplete(()
            => col.enabled = true
            );
            //var temp = Trolli.GetComponent<SpriteRenderer>();
            Trolli.color = new Color(Trolli.color.r, Trolli.color.g, Trolli.color.b, 0f);
            spriteRender.sortingOrder = sorting;
        }     
    }
}