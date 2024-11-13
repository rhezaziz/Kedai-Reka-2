using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace MiniGame1_4
{

    public class Clicked : MonoBehaviour
    {
        public Button thisObj;
        public Image garis;
        public Image Indikator;
        public Sprite[] silang;
        public Image hasil;


        public bool value;
        public void click()
        {
            FindObjectOfType<Manager>().clicked.Add(this);
            Indikator.color = new Color(1f, 1f, 1f, 1f);
            Indikator.sprite = silang[Random.Range(0,silang.Length - 1)];
            garis.transform.DOScale(Vector3.one, .5f);
            thisObj.interactable = false;

        }
        public void checkObj(Sprite value)
        {
            hasil.sprite = value;
            //hasil.text = value ? "Sesuai" : "Tidak Sesuai";
            hasil.transform.DOScale(Vector3.one * 1.25f, .75f).OnComplete(() =>
            {
                hasil.transform.DOScale(Vector3.one, .25f);
            });
        }

        public void resetObj()
        {
            thisObj.onClick.RemoveAllListeners();
            hasil.sprite = null;
            hasil.transform.localScale = Vector3.zero;
            garis.transform.localScale = Vector3.zero;
            Indikator.sprite = null;
            Indikator.color = new Color(1f, 1f, 1f, 0f);
            thisObj.interactable = true;
            thisObj.onClick.AddListener(click);
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
