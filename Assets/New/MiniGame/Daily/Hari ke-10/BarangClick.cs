using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


namespace Hari10{
    
    public class BarangClick : MonoBehaviour
    {
        public bool isItem;
        public Sprite sprite; 

        

        
        void OnMouseDown(){
            if(isItem){
                GetComponent<SpriteRenderer>().sprite = sprite;
                FindObjectOfType<Manager>().GameOver();
                transform.DOMove(Vector2.one * transform.position, 1f).OnComplete(() =>{
                    transform.DOMove(Vector2.zero, 1f);
                    transform.DOScale(Vector2.one * 2f, 1f).OnComplete(() => {
                        FindObjectOfType<Manager>().panelGameOver.SetActive(true);
                    });
                });
                return;
            }

            StartCoroutine(clicked());

        }

        IEnumerator clicked(){
            
            GetComponent<Collider2D>().enabled = false;
            transform.DORotate(new Vector3(0f,0f, 15f), .1f);
            yield return new WaitForSeconds(.1f);
            transform.DORotate(new Vector3(0f,0f, -15f), .2f);
            yield return new WaitForSeconds(.2f);
            transform.DORotate(new Vector3(0f,0f, 15f), .2f);
            yield return new WaitForSeconds(.2f);
            transform.DORotate(new Vector3(0f,0f, -15f), .2f);
            yield return new WaitForSeconds(.1f);
            transform.DORotate(new Vector3(0f,0f, 0f), .1f);

            yield return new WaitForSeconds(.5f);
            GetComponent<Collider2D>().enabled = true;
        }
    }
}
