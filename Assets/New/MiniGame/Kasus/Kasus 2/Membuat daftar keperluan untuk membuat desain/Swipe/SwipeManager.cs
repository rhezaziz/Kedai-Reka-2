using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame2_2{
    public class SwipeManager : MonoBehaviour
    {
        
        public static SwipeManager instance;

        //public List<infoBarang> valueBarang;

        //public GameObject controller;

        //public GameObject barang;
        //public Transform parent;

        public List<Throw> file;

        int jmlSalah, jmlBenar, Salah;
        int total, maxBarang;

        //public TMPro.TMP_Text textTotal, textBenar, textSalah;

        //public GameObject panelGame;
        public int jumlahDimasukkan;

        public TMPro.TMP_Text jumlahText;


        private void Awake()
        {
            instance = this;
            //file = new List<Throw>();
            StartGame();
        }

        private void Start()
        {
            jumlahText.text = $"{jumlahDimasukkan}";
        //  CountUI.action += StartGame;
        }

        public void updateJumlah(int value){
            jumlahDimasukkan -= value;
            
            jumlahText.text = $"{jumlahDimasukkan}";

            if(jumlahDimasukkan <= 0){
                    jumlahDimasukkan = 0;
                    Invoke("Selesai", 2f);

                    foreach(var col in file){
                        col.GetComponent<Collider2D>().enabled = false;

                    }
                }

        }

        

        private void StartGame()
        {

            foreach(var barang in file){
                StartCoroutine(delay(1f, barang.transform));
            }
            // for(int  i = 0; i <  file.Count; i++)
            // {

            //     GameObject barangSpawn = Instantiate(barang);
            //     Throw Throw = barangSpawn.GetComponent<Throw>();
            //     barangSpawn.SetActive(true);
            //     //Throw.initValueBarang(valueBarang[i].correct, valueBarang[i].gambar);
            //     barangSpawn.transform.SetParent(parent);
            //     barangSpawn.GetComponent<Collider2D>().enabled = false;
            //     barangSpawn.transform.localPosition = Vector2.zero;
            //     StartCoroutine(delay(1f, barangSpawn.transform));

            //     if (valueBarang[i].correct)
            //     {
            //         maxBarang += 1;
            //     }
            //     else
            //     {
            //         jmlSalah += 1;
            //     }
            // }
            // textSalah.text = Salah + " / " + jmlSalah;
            // textBenar.text = jmlBenar + "/ " + maxBarang;
            // textTotal.text = total + " / " + maxBarang;
        }

        IEnumerator delay(float delayTime, Transform barang)
        {
            Debug.Log(barang.name);
            yield return new WaitForSeconds(delayTime);
            float RotateZ = Random.Range(0f, 360f);
            float PosY = Random.Range(-1f, 1f);
            float PosX = Random.Range(-4f, 4f);
            barang.GetComponent<Collider2D>().enabled = true;
            barang.DOLocalMove(new Vector2(PosX, PosY), 1f);
            barang.DOLocalRotate(new Vector3(0f, 0f, RotateZ), 1f);

        }

        void randomBarang(Transform barang)
        {
            //
        }

        public void checkBarang(bool value)
        {
            if (value)
                jmlBenar += 1;

            else
                Salah += 1;

            // total += 1;
            // textSalah.text = Salah + " / " + jmlSalah;
            // textBenar.text = jmlBenar + "/ " + maxBarang;
            // textTotal.text = total + " / " + maxBarang;
            checkJumlah();
            Debug.Log("Benar : " + jmlBenar + "| Salah : " + jmlSalah);
        }

        void Selesai()
        {
            Debug.Log("Selesai");
            //panelGame.SetActive(false);
            //FindObjectOfType<Dialog_Sekretaris>().done = true;

            //Dialog dialog = FindObjectOfType<Dialog_Sekretaris>().Setelah_Ambil_Berkas();

            //FindObjectOfType<DialogManager>().StartDialog(dialog);
        }

        void checkJumlah()
        {
            Debug.Log("check");
            // if(total == maxBarang)
            // {
            //     Invoke("Selesai", 2f);
            //     for(int i = 0; i < parent.childCount; i++)
            //     {
            //         parent.GetChild(i).GetComponent<Collider2D>().enabled = false;
            //     }
            // }
        }
    }

    [System.Serializable]
    public class infoBarang
    {
        public Sprite gambar;
        public bool correct;


    }
}
