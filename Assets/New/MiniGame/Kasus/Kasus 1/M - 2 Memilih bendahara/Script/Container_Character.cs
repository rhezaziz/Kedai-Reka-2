using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MiniGame1_2{
    public class Container_Character : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public Image character, valueResult;
        public TMPro.TMP_Text Nama, Sifat;

        public data data;

        public bool value;

        public float kebawah;
        public bool onClick;
        public Button click;

        public TMPro.TMP_Text txt;

         
        

        public void initValue(data value){
            onClick = false;
            Sifat.text = null;
            click.onClick.RemoveAllListeners();
            int jml = value.sifat.Count;
            click.interactable = true;

            if(jml != 1){

                for(int i = 0; i < jml; i++){
                    Sifat.text += "<br>" + "-" + value.sifat[i];
                }

            }
            
            else
            {
                Sifat.text = value.sifat[0];

            }
            this.value = value.sesuai;
            character.sprite = value.ImageInSelection;
            Nama.text = value.Nama;
            //this.value = value.sesuai;
            click.onClick.AddListener(() => pilihCharacter(value));
             //SkillText.text += "<br>" + "-" + profil.character[index].skills[i].ToString();
        }

        public void unSelect(){
            onClick = false;
            //transform.GetChild(0).gameObject.SetActive(false);
            //var parent = transform.GetChild(0).GetComponent<RectTransform>();
            //Debug.Log(parent.localPosition);
            click.GetComponent<Image>().DOColor(new Color(1f,1f,1f,.1f), .5f);

            transform.DOScale(Vector2.one * 1f, 0.5f);
            txt.DOColor(new Color(0f,0f,0f, 0f), .5f).OnComplete(()=>{
                txt.transform.localScale = Vector2.zero;
                txt.color= new Color(0f,0f,0f,1f);
            });

            // parent.DOAnchorPos(new Vector2(parent.localPosition.x, parent.localPosition.y + 25), .5f).OnComplete(
            //     () => parent.DOAnchorPos(Vector2.zero, 1f)
            // );
            //FindObjectOfType<Manager>().check(temp);
        }

        public void result(data value){
            initValue(value);
            
            valueResult.sprite = FindObjectOfType<Manager>().getSprite(value.sesuai);
            valueResult.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).
                OnComplete(() => valueResult.transform.DOScale(Vector3.one, .5f));
            
            onClick = true;
            
            click.interactable = false;

        }


        public void notSelected(){
            onClick = true;
            var rectParent = GetComponent<RectTransform>();
            rectParent.DOPivot(new Vector2(.5f, 0f), .5f).OnComplete(() =>{
                rectParent.DOPivot(new Vector2(.5f, 2f), 1f);
            });
            //gameObject.SetActive(false);
            
        }
        public Transform tempParent;
        public Vector2 test;
        public float timeAnim;
        public void Terpilih(){
            var rectParent = GetComponent<RectTransform>();
            
            transform.SetParent(tempParent);

            var size = rectParent.sizeDelta;
            rectParent.anchorMin = new Vector2(.5f, 1f);
            rectParent.anchorMax = new Vector2(.5f, 1f);
            rectParent.pivot = test;
            txt.DOColor(new Color(0f,0f,0f,0f), timeAnim);
            click.GetComponent<Image>().DOColor(new Color(1f,1f,1f,0f), timeAnim);
            rectParent.DOLocalMove(Vector2.zero, timeAnim);
            transform.DOScale(Vector2.one * 1.2f, timeAnim).OnComplete(() =>{
                 valueResult.sprite = FindObjectOfType<Manager>().getSprite(value);
                    valueResult.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).
                        OnComplete(() => valueResult.transform.DOScale(Vector3.one, .5f));
            });

            //rectParent.DOSizeDelta(size * 1.5f, .5f);
            
        }

        string sifatText(sifat temp){
            if(temp == sifat.Jajan)
                return "Suka Jajan";
            
            
            return temp.ToString();
        }

        public void pilihCharacter(data temp){
            if(onClick)
                return;

            Debug.Log("Terpilih");
            onClick = true;
            //transform.GetChild(0).gameObject.SetActive(false);
            //var parent = transform.GetChild(0).GetComponent<RectTransform>();
            //Debug.Log(parent.localPosition);
            click.GetComponent<Image>().color = new Color(1f,1f,1f,.5f);

            transform.DOScale(Vector2.one * .85f, 0.5f).OnComplete(()=>{
                txt.transform.localScale = Vector2.one * 1.5f;
                txt.transform.DOScale(Vector2.one, 0.5f).OnComplete(() => {
                   
                });
            });

            // parent.DOAnchorPos(new Vector2(parent.localPosition.x, parent.localPosition.y + 25), .5f).OnComplete(
            //     () => parent.DOAnchorPos(Vector2.zero, 1f)
            // );
            FindObjectOfType<Manager>().check(GetComponent<Container_Character>());
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(onClick)
                return;
            transform.DOScale(new Vector2(0.95f, 0.95f), .5f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(onClick)
                return;
            transform.DOScale(Vector2.one, .5f);
        }
    }
}
