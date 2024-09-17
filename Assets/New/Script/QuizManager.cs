using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Terbaru{

    public class QuizManager : MonoBehaviour
    {
        public static QuizManager instance;
        [Header("UI")]
        public GameObject panelQuiz;
        public GameObject pilihanA;
        public GameObject pilihanB;
        public GameObject pilihanC;
        public GameObject pilihanD;
        public TMP_Text soal;

        soalData temp;

        void Awake(){
            instance = this;
        }
        public void initSoal(soalData data){
            temp = data;
            panelQuiz.SetActive(true);
            soal.text = data.soal;
            pilihanA.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilA;
            pilihanB.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilB;
            pilihanC.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilC;
            pilihanD.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilD;

        }

        public void checkJawaban(string value){
            if(temp != null){
                
                if(value == temp.jawaban){
                    Debug.Log("Benar");
                }
                else{
                    Debug.Log("Salah");
                }

                temp = null;
                panelQuiz.SetActive(false);

            }
        }

                
    }



}