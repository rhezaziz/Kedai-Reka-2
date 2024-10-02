using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


namespace  Terbaru
{ 
    public class Tap_Hold : MonoBehaviour
    {
        public UnityEngine.UI.Image value;
        public Transform foto;
        public Transform dialog1, dialog2;
        private float timer;
        bool finished = false;

        private Tweener tweener;

        Sequence temp;
        Animator anim;

        void Start(){
            timer = Random.Range(10f, 20f);
            anim = GetComponent<Animator>();
            //value = transform.Find("Value").gameObject.GetComponent<UnityEngine.UI.Image>();
            

        }
        void OnMouseDown(){
            if(finished)
                return;
            temp = DOTween.Sequence();
            anim.SetBool("Rotate", true);
            temp.Append(value.DOFillAmount(1f, timer).OnComplete(() => selesai()));
            temp.Insert(timer / 8f, dialog1.DOScale(Vector3.one / 2f,  .5f));
            temp.Insert(timer / 8f + 4f, dialog2.DOScale(Vector3.one / 2f,  .5f));
            temp.Insert(timer / 8f + 6f, dialog1.DOScale(Vector3.zero,  .5f));
            temp.Insert(timer / 8f + 8f, dialog2.DOScale(Vector3.zero,  .5f));



            //tweener = value.DOFillAmount(1f, timer).OnComplete(() => selesai());

            //StartCoroutine(startValue());
        }

        void OnMouseUp(){
            if(finished)
                return;

            value.fillAmount = 0f;
            dialog1.localScale = Vector3.zero;
            dialog2.localScale = Vector3.zero;
            anim.SetBool("Rotate", false);

            temp.Kill();
            
        }

        void balonDialog_1(){
            var anim = dialog1.GetComponent<Animator>();
        }

        void selesai(){
            finished = true;
            dialog1.localScale = Vector3.zero;
            dialog2.localScale = Vector3.zero;
            anim.SetBool("Rotate", false);

        }

        IEnumerator startValue(){
            float index = 0f;
            float duration = timer / 10f;

            while (index < 100f)
            {
                index += 1;
                Debug.Log(index);
                
                yield return new WaitForSeconds(1);
            }

        }
    }   
}
