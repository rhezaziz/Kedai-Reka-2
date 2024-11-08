using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Terbaru;
using UnityEngine;

namespace MiniGame5_4{
    public class Manager : MonoBehaviour
    {

        public UnityEngine.UI.Image resultPanel;
        public Sprite Wrong, Correct; 

        public UnityEngine.UI.Button buttonOk, resetBtn, CheckBtn;

        public GameObject panelGameOver;
        public TMPro.TMP_Text takaranText;
        public int Takaran;
        public int currentTimbangan;

        public Collider2D col;

        public Timbangan timbangan;

        bool sesuai;


        public void startGame(){
            col.enabled = true;

            resultPanel.transform.localScale = Vector3.zero;
            buttonOk.interactable = false;
            panelGameOver.SetActive(false);
        }

        public void setCurrentTakaran(int value){
            currentTimbangan += value;

            sesuai = currentTimbangan == Takaran;
            resetBtn.interactable = true;
            CheckBtn.interactable = true;
        }


        public void check(){
            col.enabled = false;
            Sprite temp = sesuai ? Correct : Wrong;

            resultPanel.sprite = temp;
            panelGameOver.SetActive(true);
            takaranText.text = $"{currentTimbangan} Gram";
            StartCoroutine(checkAnimation());
           
        }


        IEnumerator checkAnimation(){
            yield return new WaitForSeconds(2f);
             resultPanel.transform.DOScale(Vector3.one * 1.25f, 1f).OnComplete(() => {
                resultPanel.transform.DOScale(Vector3.one, .25f).OnComplete(() => {
                   resultPanel.transform.DOScale(Vector3.one, 1f).OnComplete(() => {
                        buttonOk.interactable = true;
                    }); 
                });
            });
        }

        public void resetGame(){
            timbangan.resetIsi();
            resetBtn.interactable = false;
            CheckBtn.interactable = false;
            currentTimbangan = 0;
        }
        public string action;
        public void Action(){
            int point = sesuai ? (currentTimbangan / 500) : 0;
            QuestManager.instance.currentQuest.quest.pointBonus += point;
            QuestManager.instance.CheckAction(action);
        }
    }
}