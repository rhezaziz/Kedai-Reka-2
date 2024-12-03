using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace MiniGame5_4{
    public class DragItem : MonoBehaviour
    {
        // Start is called before the first frame update
        Vector2 posAwal;

        public float jarak;
        public SpriteRenderer isi;

        bool terisi;

        public float time1, time2;

        public Transform Timbangan, Karung;

        public Material matIsi;

        Animator anim;
        bool move = true;
        void Start()
        {
            anim = GetComponent<Animator>();
            posAwal = transform.position;
            //col = GetComponent<Collider2D>();
        }
        Transform temp;
        void OnMouseDown(){
            temp = terisi ? Timbangan : Karung;
            GetComponent<SpriteRenderer>().sortingOrder = 6;
            isi.sortingOrder = 7;
        }   

        void OnMouseDrag(){
            if(!move)
                return;

            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(pos.x, pos.y);

            objectClose();

        }

        void OnMouseUp(){
            temp.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
            if(!closes){
                transform.DOMove(posAwal, .5f);
                return;
            }
            //anim.enabled = true;
            move = false;
            if(terisi){
                taruhTimbangan();
            }else{
                ambil();
            }
        }

        bool closes;
        void objectClose(){
            float dir = Vector2.Distance(transform.position, temp.position);

            if(dir <= jarak){
                closes = true;

                temp.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,.75f);
            }else{
                closes = false;
                temp.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
            }
        }

        public void enableAnimator(){
            //anim.SetTrigger("Pindah");
            move = true;
            anim.enabled = false;
            Debug.Log("enabled");
            //Invoke("enabledAnim",.5f);
        }

        void enabledAnim(){
            
            
        }

        void taruhTimbangan(){
            GetComponent<SpriteRenderer>().sortingOrder = 2;
            isi.sortingOrder = 3;
            transform.localPosition = new Vector2(-6.58f, 2.28f);
            transform.DOLocalMove(new Vector2 (-6.72f, 2.72f), .58f);
            transform.DOLocalRotate(new Vector3(0f,0f, -21.61f), .58f).OnComplete(() =>{
                transform.DOLocalMove(new Vector2(-6.66f, 2.56f), .16f).OnComplete(() => {
                    transform.DOLocalMove(new Vector2 (-6.72f, 2.72f), .58f).OnComplete(() =>{
                        transform.DOLocalMove(new Vector2(-6.66f, 2.56f), .16f).OnComplete(() => {
                            FindObjectOfType<Timbangan>().tambahTakaran(500f);
                            isi.gameObject.SetActive(false);
                            transform.DOLocalMove(new Vector2 (-6.72f, 2.72f), .58f).OnComplete(() =>{
                                transform.DOLocalMove(new Vector2(-6.66f, 2.56f), .16f).OnComplete(() => {
                                    transform.DOLocalMove(new Vector2 (-6.72f, 2.72f), .58f).OnComplete(() =>{
                                        transform.DOLocalRotate(Vector3.zero, .5f);
                                        GetComponent<SpriteRenderer>().sortingOrder = 6;
                                        isi.sortingOrder = 7;
                                        transform.DOLocalMove(posAwal, 0.5f).OnComplete(() =>{
                                            move = true;
                                            terisi = false;
                                        });
                                    });
                                });
                            });
                        });
                    });
                });
            });
            //anim.SetTrigger("Tuang");

            //Invoke("enableAnimator", time2);
            //terisi = false;
        }

        public void taruhTepung(){
            FindObjectOfType<Timbangan>().tambahTakaran(500);
            terisi = false;
        }

        void ambil(){
            //anim.enabled = true;
            terisi = true;
            animasiAmbil();
            // anim.SetTrigger("Awal");
            // anim.SetTrigger("Ambil");

            // Invoke("enableAnimator", time1);
        }


        void animasiAmbil(){
            Sequence animSeq = DOTween.Sequence();
            transform.localPosition = new Vector2(4.45f, 1.71f);
            transform.DOLocalMoveY(2.27f, .5f).OnComplete(() =>{
                transform.DOLocalRotate(new Vector3(0f,0f, -21.61f), .16f).OnComplete(() =>{
                    transform.DOLocalMove(new Vector2(5.04f, 1.6f), .16f);
                    transform.DOLocalRotate(new Vector3(0f,0f, -12), .16f).OnComplete(() => {
                        transform.DOLocalMove(new Vector2(5.07f, 1.59f), .016f);
                        
                        transform.DOLocalRotate(new Vector3(0f,0f, -9.725f), .016f).OnComplete(() =>{
                            isi.gameObject.SetActive(true);
                            transform.DOLocalRotate(Vector3.zero, .16f).OnComplete(() =>{
                                transform.DOMove(posAwal, .5f).OnComplete(() =>{
                                    move = true;
                                    terisi = true;
                                });
                            });
                        });
                    });
                });
            });
        }

    }
}
