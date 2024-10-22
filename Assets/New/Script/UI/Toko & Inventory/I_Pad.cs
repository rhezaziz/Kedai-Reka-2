using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace  Terbaru
{
    public class I_Pad : MonoBehaviour
    {
        public RectTransform ipadUI;

        [Header("Lock Screen")]
        public GameObject[] questList;
        public TMP_Text text_Jam;
        public TMP_Text text_Tanggal;

        playerProfil profil;

        public void closeIpad(){
            RectTransform iPad = ipadUI.transform.GetChild(0).GetComponent<RectTransform>();
            //FindObjectOfType<swipeUI>().slide.value = 0f;
            iPad.DOPivotY(1f, 1.5f).OnComplete(() => ipadUI.gameObject.SetActive(false));
            //iPad.DOScaleY(0f, 1.5f).OnComplete(() => ipadUI.gameObject.SetActive(false));

        }

        
        public void animasiUI_Ipad()
        {
            SoundManager.instance.uiSFX(14);
            profil = GameManager.instance.profil;
            ipadUI.gameObject.SetActive(true);
            RectTransform iPad = ipadUI.transform.GetChild(0).GetComponent<RectTransform>();
            iPad.DOPivotY(0f, 1.5f);
            //iPad.transform.DOScaleY(1f, 1.5f);
            text_Tanggal.text = $"{profil.jmlHari}/November/2024";
            initQuest();
        }

        void initQuest(){
            for(int i = 0; i < questList.Length; i++){
                Quest temp = QuestManager.instance.getQuest(i);
                
                TMP_Text textQuest = questList[i].transform.GetChild(0).GetComponent<TMP_Text>();
                questList[i].SetActive(!temp.isDone);
                textQuest.text = temp.isDone ? "" : temp.judulMisi;
            }
        }
    }   
}
