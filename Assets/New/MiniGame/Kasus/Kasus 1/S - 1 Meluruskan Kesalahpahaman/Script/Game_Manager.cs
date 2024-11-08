using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Terbaru;
using DG.Tweening;

namespace MiniGame1_5{
    public class Game_Manager : MonoBehaviour
    {

        //public static QuizManager instance;

        public bool DailyQuest;
        [Header("UI")]
        public GameObject panelQuiz;
        public GameObject pilihanA;
        public GameObject pilihanB;
        public GameObject pilihanC;
        public GameObject pilihanD;
        public TMP_Text soal;

        public Sprite Wrong, Correct;

        public List<UnityEngine.UI.Button> option = new List<UnityEngine.UI.Button>();

        public soalData data;
        public string action;
        //public soalData data;

        public void StartGame(){
            if(data != null)
                initPanelQuiz();
        }
        public void checkAction(){
            
            if(!DailyQuest)
                QuestManager.instance.CheckAction(action);

            else
                QuestManager.instance.CheckActionQuest(action);
            // Debug.Log(action);
        }

        void initPanelQuiz(){
            panelQuiz.SetActive(true);
            soal.text = data.soal;
            pilihanA.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilA;
            pilihanB.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilB;
            pilihanC.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilC;
            pilihanD.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilD;
        }

        public void clickUp(UnityEngine.UI.Button btn){

            string jawaban = btn.name;

            btn.transform.DOScale(Vector2.one * .85f, .5f).OnComplete(() =>{
                btn.transform.DOScale(Vector2.one, .25f).OnComplete(() =>{
                    checkJawaban(jawaban, btn.transform.parent);
                });
            });
            foreach(var button in option){
                button.interactable = false;
            }

            
        }
        Sprite spriteTemp;
        public void checkJawaban(string value, Transform parent){

            
            if(data != null){
                
                if(value == data.jawaban){
                    Debug.Log("Benar");
                    spriteTemp = Correct;
                    if(!DailyQuest) QuestManager.instance.currentQuest.quest.pointBonus += 50;
                }
                else{
                    spriteTemp = Wrong;
                    Debug.Log("Salah");
                }

                data = null;
                //panelQuiz.SetActive(false);

            }

            GameObject resultObj = parent.GetChild(1).gameObject;
            resultObj.SetActive(true);
            UnityEngine.UI.Image result = resultObj.GetComponent<UnityEngine.UI.Image>();
            result.sprite = spriteTemp;
            //result.DOColor(new Color (1f , 1f, 1f, 1f), 1f);
            result.DOColor(new Color(1,1,1,1), 1f).OnComplete(() =>{
                //Debug.Log("Selesai");
                checkAction();
            });

            
        }

       
    }
}
