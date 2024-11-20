using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace minigame{
    public class DragItem : MonoBehaviour
    {
        public UnityEvent events;
        Vector2 posAwal;
        public Transform pot;
        public float jarak;
        public float jarakAtas;
        public Vector2 posAtas;

        public SpriteRenderer gambar;
        

        void OnMouseDown (){
            GetComponent<SpriteRenderer>().sortingOrder += 2;
            gambar.sortingOrder += 2;
        }

        void OnMouseDrag(){
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector2(mousePos.x, mousePos.y);
        }

        void OnMouseUp(){
            float distance = Vector2.Distance(transform.position, pot.position);

            if(distance <= jarak){
                GetComponent<Collider2D>().enabled = false;
                transform.position = posAtas;
                transform.DORotate(new Vector3(0f,0f, 60f), 1f).OnComplete(() => {
                    transform.DOMoveY(posAtas.y + jarakAtas, .15f).OnComplete(() => {
                         transform.DOMoveY(posAtas.y - jarakAtas, .25f).OnComplete(() => {
                            transform.DOMoveY(posAtas.y + jarakAtas, .25f).OnComplete(() => {
                                transform.DOMoveY(posAtas.y - jarakAtas, .25f).OnComplete(() => {
                                    transform.DOMoveY(posAtas.y + jarakAtas, .25f).OnComplete(() => {
                                        events?.Invoke();
                                    });
                                });
                            });
                        });
                    });
                });
            }else{
                kembali();
            }
        }

        void kembali(){
            transform.DOMove(posAwal, .5f).OnComplete(() => {
                GetComponent<SpriteRenderer>().sortingOrder -= 2;
                gambar.sortingOrder -= 2;
            });
        }
    }
}
