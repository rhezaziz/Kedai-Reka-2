using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MiniGame4_1{
    public class SelectedObject : MonoBehaviour
    {

        public bool value;
        bool clicked = false;

         public bool hidden;
        public bool tutup;

        Vector2 PosAwal;

        public Items item;
        [SerializeField] Transform loctText;

        void Start(){
            PosAwal = transform.localPosition;
        }


        void OnMouseDown(){
            if(clicked)
                return;
            
            FindObjectOfType<Manager>().itemOnClick(GetComponent<SpriteRenderer>());
            // if(value){
            //     correctSelected();
            // }else{
            //     wrongSelected();
            // }
            // clicked = true;
            
        }

        void correctSelected(){
            //StartCoroutine(animasHiddenObject());
        }

        public void setItemsValue(Transform posText){
            loctText = posText;
            value = true;
        }

        IEnumerator animasHiddenObject()
        {
            GetComponent<Collider2D>().enabled = false;
            clicked = true;
            Vector2 end = loctText.localPosition;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 6;
            Vector2 start = transform.localPosition;
            transform.SetParent(loctText.transform.parent);

            //transform.localPosition = Vector2.Lerp(start, end, 0.5f);
            transform.DOLocalMove(end, 0.5f);
            yield return new WaitForSeconds(0.5f);

            string namaBarang =  loctText.GetComponent<UnityEngine.UI.Text>().text;

            loctText.transform.GetChild(0).DOScaleX(1f, 1f);
            //loctText.GetComponent<UnityEngine.UI.Text>().text = "<s>" + namaBarang + "</s>";
            gameObject.SetActive(false);
            //StopCoroutine();
        }

        void wrongSelected(){
            Sequence anim = DOTween.Sequence();
            float distX = transform.localPosition.x;
            float x = .15f;
            anim.Append(transform.DOLocalMoveX((distX + x), 0.1f));
            anim.Append(transform.DOLocalMoveX((distX - x), 0.1f));
            anim.Append(transform.DOLocalMoveX((distX + x), 0.1f));
            anim.Append(transform.DOLocalMoveX((distX - x), 0.1f));
            anim.Append(transform.DOLocalMoveX((distX + x), 0.1f));
            anim.Append(transform.DOLocalMoveX((distX - x), 0.1f));
            anim.Append(transform.DOLocalMoveX((distX + x), 0.1f));
            anim.Append(transform.DOLocalMoveX((distX - x), 0.1f));
            anim.Append(transform.DOLocalMoveX(distX, 0.1f));
            anim.Append(transform.DOLocalMove(PosAwal, .1f)).OnComplete(()
                => clicked = false);
        }
        
    }
}