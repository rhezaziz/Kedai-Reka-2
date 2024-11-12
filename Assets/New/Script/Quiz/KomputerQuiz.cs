using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Terbaru
{
    public class KomputerQuiz : MonoBehaviour
    {
        public List<soalData> soals = new List<soalData>();
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
        public Button closeKomputer;

        soalData temp;
        public void setSoal(int index)
        {
            temp = soals[index];

            panelResult.DOColor(new Color(0f, 0f, 0f, 0f), .5f).OnComplete(() =>
            {
                panelResult.gameObject.SetActive(true);
            });

            Result.transform.DOScale(Vector3.zero, .5f);
        }

        public void initSoal()
        {
            //temp = data;
            closeKomputer.interactable = false;
            panelQuiz.SetActive(true);
            soal.text = temp.soal;
            pilihanA.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = temp.pilA;
            pilihanB.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = temp.pilB;
            pilihanC.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = temp.pilC;
            pilihanD.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = temp.pilD;

            pilihanA.transform.GetChild(0).GetComponent<Button>().interactable = true;
            pilihanB.transform.GetChild(0).GetComponent<Button>().interactable = true;
            pilihanC.transform.GetChild(0).GetComponent<Button>().interactable = true;
            pilihanD.transform.GetChild(0).GetComponent<Button>().interactable = true;

        }

        public void checkJawaban(string value)
        {
            pilihanA.transform.GetChild(0).GetComponent<Button>().interactable = false;
            pilihanB.transform.GetChild(0).GetComponent<Button>().interactable = false;
            pilihanC.transform.GetChild(0).GetComponent<Button>().interactable = false;
            pilihanD.transform.GetChild(0).GetComponent<Button>().interactable = false;

            if (temp != null)
            {
                bool hasil;
                if (value == temp.jawaban)
                {
                    hasil = true;
                    Debug.Log("Benar");
                }
                else
                {
                    hasil = false;
                    Debug.Log("Salah");
                }

                StartCoroutine(hasilQUiz(hasil));

            }
            QuizManager.instance.soalMingguan.Add(temp);
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
            soal.text = "";
            pilihanA.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "";
            pilihanB.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "";
            pilihanC.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "";
            pilihanD.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "";
            closeKomputer.interactable = true;

            //temp.Clear();
            //    panelQuiz.SetActive(false);
            //    FindObjectOfType<WaktuManager>().currentTime(1);
            //    FindObjectOfType<DayManager>().updateMaps();
            //if (temp.Count > 0)
            //{
            //    nextSoal();




            //    yield return new WaitForSeconds(.75f);

            //    initSoal();

            //}

            //else
            //{

            //}


        }
    }
}

