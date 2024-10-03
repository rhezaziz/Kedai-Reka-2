using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Terbaru;

namespace MiniGame1_5{
    public class Game_Manager : QuizManager
    {


        public string action;
        public soalData data;

        void Start(){
            initPanelQuiz();
        }
        public void checkAction(){
            QuestManager.instance.CheckAction(action);
        }

        void initPanelQuiz(){
            panelQuiz.SetActive(true);
            soal.text = data.soal;
            pilihanA.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilA;
            pilihanB.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilB;
            pilihanC.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilC;
            pilihanD.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilD;
        }
    }
}
