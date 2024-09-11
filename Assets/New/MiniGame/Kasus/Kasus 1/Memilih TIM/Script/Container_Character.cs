using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MiniGame1_1{
    public class Container_Character : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public Image character, valueResult;
        public TMPro.TMP_Text Nama, Sifat;

        public bool value;

        public float kebawah;
        public bool onClick;
        public Button click;
        

        public void initValue(data value){
            onClick = false;
            Sifat.text = null;
            click.onClick.RemoveAllListeners();
            int jml = value.sifat.Count;
            click.interactable = true;

            if(jml != 1){

                for(int i = 0; i < jml; i++){
                    Sifat.text += "<br>" + "-" + sifatText(value.sifat[i]);
                }

            }else{
                Sifat.text = sifatText(value.sifat[0]);

            }
            Nama.text = value.Nama;
            //this.value = value.sesuai;
            click.onClick.AddListener(() => pilihCharacter(value));
             //SkillText.text += "<br>" + "-" + profil.character[index].skills[i].ToString();
        }

        public void result(data value){
            initValue(value);
            
            valueResult.sprite = FindObjectOfType<Manager>().getSprite(value.sesuai);
            valueResult.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).
                OnComplete(() => valueResult.transform.DOScale(Vector3.one, .5f));
            
            onClick = true;
            
            click.interactable = false;

        }

        string sifatText(sifat temp){
            if(temp == sifat.Jajan)
                return "Suka Jajan";
            
            
            return temp.ToString();
        }

        public void pilihCharacter(data temp){
            transform.GetChild(0).gameObject.SetActive(false);
            //var parent = transform.GetChild(0).GetComponent<RectTransform>();
            //Debug.Log(parent.localPosition);
            

            // parent.DOAnchorPos(new Vector2(parent.localPosition.x, parent.localPosition.y + 25), .5f).OnComplete(
            //     () => parent.DOAnchorPos(Vector2.zero, 1f)
            // );
            FindObjectOfType<Manager>().check(temp);
            onClick = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(onClick)
                return;
            transform.DOScale(new Vector2(0.75f, 0.75f), .5f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(onClick)
                return;
            transform.DOScale(Vector2.one, .5f);
        }
    }
}
