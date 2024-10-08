using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Terbaru;

namespace MiniGame1_5{
    public class Game_Manager : MonoBehaviour
    {

        //public static QuizManager instance;
        [Header("UI")]
        public GameObject panelQuiz;
        public GameObject pilihanA;
        public GameObject pilihanB;
        public GameObject pilihanC;
        public GameObject pilihanD;
        public TMP_Text soal;

        public soalData data;
        public string action;
        //public soalData data;

        void Start(){
            initPanelQuiz();
        }
        public void checkAction(){
            QuestManager.instance.CheckAction(action);
            Debug.Log(action);
        }

        void initPanelQuiz(){
            panelQuiz.SetActive(true);
            soal.text = data.soal;
            pilihanA.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilA;
            pilihanB.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilB;
            pilihanC.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilC;
            pilihanD.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilD;
        }

        public void checkJawaban(string value){
            if(data != null){
                
                if(value == data.jawaban){
                    Debug.Log("Benar");
                }
                else{
                    Debug.Log("Salah");
                }

                data = null;
                panelQuiz.SetActive(false);

            }

            checkAction();
        }
    }
}
