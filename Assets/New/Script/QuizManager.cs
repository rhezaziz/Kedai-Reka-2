using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Terbaru{

    public class QuizManager : MonoBehaviour
    {
        public List<soalData> soalDosen = new List<soalData>();
        public List<soalData> soalMingguan = new List<soalData>();

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
            if(instance != null)
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

        public void setSoal(int index)
        {
            temp = soalDosen[index];
        }

        public void initSoal()
        {
            //temp = data;
            panelQuiz.SetActive(true);
            soal.text = temp.soal;
            pilihanA.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = temp.pilA;
            pilihanB.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = temp.pilB;
            pilihanC.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = temp.pilC;
            pilihanD.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = temp.pilD;

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
            FindObjectOfType<WaktuManager>().currentTime(1);
            FindObjectOfType<DayManager>().updateMaps();
        }

                
    }



}