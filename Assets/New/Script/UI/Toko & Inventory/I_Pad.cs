using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using food;
using Unity.VisualScripting;

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
        public void animasiUI_Ipad()
        {
            profil = GameManager.instance.profil;
            ipadUI.gameObject.SetActive(true);
            RectTransform iPad = ipadUI.transform.GetChild(0).GetComponent<RectTransform>();
            iPad.transform.DOScaleY(1f, 1.5f);
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
