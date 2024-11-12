using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

namespace Terbaru{

    public class QuizManager : MonoBehaviour
    {
        public List<soalData> soalDosen = new List<soalData>();
        public List<soalData> soalMingguan = new List<soalData>();

        public KomputerQuiz komputer;

        public Sprite wrong, correct;

        public static QuizManager instance;
        [Header("UI")]
        public GameObject panelQuiz;
        public GameObject pilihanA;
        public GameObject pilihanB;
        public GameObject pilihanC;
        public GameObject pilihanD;
        public TMP_Text soal;
        public Image panelResult;
        public Image Result;

        List<soalData> temp = new List<soalData>();

        void Awake() {
            if (instance != null)
                instance = this;
        }
        //public void initSoal(soalData data){
        //    temp = data;
        //    panelQuiz.SetActive(true);
        //    soal.text = data.soal;
        //    pilihanA.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilA;
        //    pilihanB.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilB;
        //    pilihanC.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilC;
        //    pilihanD.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = data.pilD;

        //}

        public void initSoalWithData(soalData soal)
        {
            temp[0] = soal;
            initSoal();
        }

        public void setSoal(int index)
        {
            if (index % 7 != 0)
                temp.Add(soalDosen[index]);
            else
            {
                foreach (var soal in soalMingguan)
                {
                    temp.Add(soal);
                }
            }

            if(komputer) komputer.setSoal(index);
        }

        public void initSoal()
        {
            //temp = data;
            panelQuiz.SetActive(true);
            soal.text = temp[0].soal;
            pilihanA.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = temp[0].pilA;
            pilihanB.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = temp[0].pilB;
            pilihanC.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = temp[0].pilC;
            pilihanD.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = temp[0].pilD;

            pilihanA.transform.GetChild(0).GetComponent<Button>().interactable = true;
            pilihanB.transform.GetChild(0).GetComponent<Button>().interactable = true;
            pilihanC.transform.GetChild(0).GetComponent<Button>().interactable = true;
            pilihanD.transform.GetChild(0).GetComponent<Button>().interactable = true;

        }

        public void checkJawaban(string value) {
            pilihanA.transform.GetChild(0).GetComponent<Button>().interactable = false;
            pilihanB.transform.GetChild(0).GetComponent<Button>().interactable = false;
            pilihanC.transform.GetChild(0).GetComponent<Button>().interactable = false;
            pilihanD.transform.GetChild(0).GetComponent<Button>().interactable = false;

            if (temp != null) {
                bool hasil;
                if (value == temp[0].jawaban) {
                    hasil = true;
                    //Debug.Log("Benar");
                }
                else {
                    hasil = false;
                    
                }

                //temp = null;
                //panelQuiz.SetActive(false);

                StartCoroutine(hasilQUiz(hasil));

            }

            temp.RemoveAt(0);
            
        }


        IEnumerator hasilQUiz(bool hasil)
        {
            panelResult.color = new Color(0f, 0f, 0f, 0f);
            panelResult.gameObject.SetActive(true);

            panelResult.DOColor(new Color(0f, 0f, 0f, .5f), 1f);
            Result.sprite = hasil ? correct : wrong;

            Result.transform.DOScale(Vector3.one * 1.2f, .75f).OnComplete(() =>
            {
                Result.transform.DOScale(Vector3.one, .25f);
            });
            yield return new WaitForSeconds(1.25f);

            if(temp.Count > 0)
            {
                nextSoal();
                

                panelResult.DOColor(new Color(0f, 0f, 0f, 0f), .5f).OnComplete(() =>
                {
                    panelResult.gameObject.SetActive(true);
                });

                Result.transform.DOScale(Vector3.zero, .5f);

                yield return new WaitForSeconds(.75f);

                initSoal();

            }

            else
            {
                Result.transform.localScale = Vector3.zero;
                panelResult.color = new Color(0f, 0f, 0f, 0f);
                panelResult.gameObject.SetActive(false);
                temp.Clear();
                panelQuiz.SetActive(false);
                FindObjectOfType<WaktuManager>().currentTime(1);
                FindObjectOfType<DayManager>().updateMaps();
            }


        }

        void nextSoal()
        {
            soal.text = "";
            pilihanA.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "";
            pilihanB.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "";
            pilihanC.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "";
            pilihanD.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "";
        }   

                
    }



}