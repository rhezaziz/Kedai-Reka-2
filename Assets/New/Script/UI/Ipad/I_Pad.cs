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
            int jmlHari = FindObjectOfType<DayManager>().day;
            text_Tanggal.text = $"{jmlHari}/November/2024";
            initQuest();
        }

        void initQuest(){
            int lenght = QuestManager.instance.lenghtQuest();

            lenght = lenght > questList.Length ? 3 : lenght;
            Quest[] temp = QuestManager.instance.initListQuest();
            for(int i = 0; i < lenght; i++){
                //Quest temp = QuestManager.instance.getQuest(i);
                
                TMP_Text textQuest = questList[i].transform.GetChild(0).GetComponent<TMP_Text>();
                //questList[i].SetActive(!temp.isDone);
                //textQuest.text = temp.isDone ? "" : temp.judulMisi;

                if (temp[i] != null)
                {
                    questList[i].gameObject.SetActive(true);
                    textQuest.text = temp[i].judulMisi;
                    //container.initContent(temp[i], i);
                }
                else
                {
                    questList[i].gameObject.SetActive(false);
                }
            }
        }
    }   
}
