using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Terbaru{

    public class UI_Dialog : MonoBehaviour
    {
        // Start is called before the first frame update
        public Sprite dialogKanan, dialogKiri, polosKana, polosKiri;

        public UiComponent components;

        public GameObject panelNarasi;
        Vector2 Atas, Bawah;

        public Color UnSelect;
        public float testSpeed;

        TMP_Text dialogText;

        public TMP_Text tempText{
            set{
                dialogText = value;
            }

            get{
                return dialogText;
            }
        }

        

        
        void Start()
        {
            Atas = components.panelDialog[1].transform.localPosition;
            Bawah = components.panelDialog[0].transform.localPosition;

            //Debug.Log($"Atas : {Atas} -- Bawah : {Bawah}");
        }


        public void startDialog(){
            Debug.Log("Mulai");
            for(int i = 0; i < components.textNama.Length; i++){
                components.textNama[i].text = "";
                components.GambarCharacter[i].gameObject.SetActive(false);
                components.panelDialog[i].gameObject.SetActive(false); 
            }
        }

        public void ResetDialog(){
            Debug.Log("ResetDIalog");
            for(int i = 0; i < components.textNama.Length; i++){
                components.textNama[i].text = "";
                components.textNama[i].transform.parent.gameObject.SetActive(false);
                components.GambarCharacter[i].gameObject.SetActive(false);
                components.panelDialog[i].gameObject.SetActive(false); 
            }
        }

        public void VOActive(){
            
            foreach(var ui in components.GambarCharacter){
                ui.color = UnSelect;
            }
        }

        bool kiri;
        public void displayBallonDialog(int value, dataDialog data){
            SoundManager.instance.uiSFX(3);
            int index = value  % 2;
            int tempIndex = (value + 1) % 2;
            int activeIndex = data.Kiri ? 0 : 1;
            int unActiveIndex = (activeIndex + 1) % 2;

            var panelBawah = components.panelDialog[index];
            var panelAtas = components.panelDialog[tempIndex];
            var namaCharacter = components.textNama[activeIndex];
            var activeSprite = components.GambarCharacter[activeIndex];
            var unActiveSprite = components.GambarCharacter[unActiveIndex];


            activeSprite.gameObject.SetActive(true);
            namaCharacter.transform.parent.gameObject.SetActive(true);
            bool haveSprite = data.GambarKarakter != null;
            activeSprite.enabled = haveSprite;
            activeSprite.sprite = data.GambarKarakter;
            namaCharacter.text = data.Nama.ToString();

            activeSprite.color = Color.white;
            unActiveSprite.color = UnSelect;

            

            tempText = panelBawah.GetComponentInChildren<TMP_Text>();          

            panelBawah.gameObject.SetActive(false);
            panelBawah.transform.localPosition = Bawah;
            
            if(value == 0){
                components.panelDialog[0].gameObject.SetActive(true);
            }
            
            panelBawah.transform.DOScale(Vector3.one, testSpeed);

            panelAtas.transform.DOLocalMoveY(Atas.y, testSpeed);
            panelAtas.transform.DOScale(new Vector3 (0.9f, 0.9f, 1f), testSpeed).OnComplete(()=>
                panelAtas.transform.SetAsFirstSibling());
            panelAtas.sprite = kiri ? polosKiri : polosKana;
            panelBawah.gameObject.SetActive(true);
            kiri = data.Kiri;
            panelBawah.sprite = data.Kiri ? dialogKiri : dialogKanan;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        [System.Serializable]
        public class UiComponent{
            public TMP_Text[] textNama;
            public Image[] panelDialog;

            public Image[] GambarCharacter;

        }
    }
}
