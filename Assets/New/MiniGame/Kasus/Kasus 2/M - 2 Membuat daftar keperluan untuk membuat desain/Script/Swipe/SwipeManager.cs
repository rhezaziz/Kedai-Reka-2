using DG.Tweening;
using food;
using System.Collections;
using System.Collections.Generic;
using Terbaru;
using UnityEngine;

namespace MiniGame2_2{
    public class SwipeManager : MonoBehaviour
    {
        
        public static SwipeManager instance;
        public Transform parent;
        public GameObject text;

        //public List<infoBarang> valueBarang;

        //public GameObject controller;

        //public GameObject barang;
        //public Transform parent;

        public List<Throw> file;
        public List<Barang> barangInList = new List<Barang>();

        int jmlSalah, jmlBenar, Salah;
        int total, maxBarang;

        //public TMPro.TMP_Text textTotal, textBenar, textSalah;

        //public GameObject panelGame;
        public int jumlahDimasukkan;

        public TMPro.TMP_Text jumlahText;

        public GameObject resultPanel;
        public TMPro.TMP_Text hasil;
        public UnityEngine.UI.Button resultBtn;

        private void Awake()
        {
            instance = this;
            //file = new List<Throw>();
            //StartGame();
        }

        private void Start()
        {
            jumlahText.text = $"Jumlah : {barangInList.Count}";
            jumlahDimasukkan = barangInList.Count;
            total = jumlahDimasukkan;
        //  CountUI.action += StartGame;
        }

        int totalBarang;

        public void updateJumlah(GameObject value){
            var b = tempBarang(value);
            GameObject tmpText = b != null ? b.listText : null;
            checkBarang(tmpText);
            //bool benar = tmpText != null;




            jumlahDimasukkan -= 1;
            
            jumlahText.text = $"Jumlah : {jumlahDimasukkan}";

            if(jumlahDimasukkan <= 0){
                jumlahDimasukkan = 0;
                //Invoke("Selesai", 2f);
                
                foreach(var col in file){
                    col.GetComponent<Collider2D>().enabled = false;

                }

                StartCoroutine(result());
            }

        }

        Barang tempBarang(GameObject temp){
            foreach(var barang in barangInList){
                if(barang.barang == temp){
                    return barang;
                }
            }

            return null;
        }

        

        public void StartGame()
        {
            for(int i = 0; i < barangInList.Count; i++){
                GameObject textList = Instantiate(text);
                textList.SetActive(true);
                textList.transform.SetParent(parent);
                textList.GetComponent<TMPro.TMP_Text>().text = barangInList[i].NamaBarang;
                barangInList[i].listText = textList;
                textList.transform.localPosition = Vector3.zero;
                textList.transform.localScale = Vector3.one;
            }

            foreach(var item in file){
                item.GetComponent<Collider2D>().enabled = true;
            }

            //
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

        public void checkBarang(GameObject barang)
        {
            bool value = barang != null;
            if (value){
                jmlBenar += 1;
                barang.transform.GetChild(0).DOScaleX(1f, 1f);
            }
                

            else
                Salah += 1;

            // total += 1;
            // textSalah.text = Salah + " / " + jmlSalah;
            // textBenar.text = jmlBenar + "/ " + maxBarang;
            // textTotal.text = total + " / " + maxBarang;
            checkJumlah();
            Debug.Log("Benar : " + jmlBenar + "| Salah : " + jmlSalah);
        }
        public string Action;
        IEnumerator result(){
            yield return new WaitForSeconds(2f);
            resultPanel.SetActive(true);
            resultPanel.transform.GetChild(0).DOScale(Vector3.one * .7f, 1f);
            hasil.text = $"0/{total}";
            yield return new WaitForSeconds(1f);

            int count = 100;
            while(count > 0){
                int tempInt = Random.Range(0, total);
                hasil.text = $"{tempInt}/{total}";
                count -= 1;
                yield return new WaitForSeconds(5 / 100);
            }

            hasil.text = $"{jmlBenar}/{total}";
            int point = jmlBenar * 50;
            QuestManager.instance.currentQuest.quest.pointBonus += point;

            yield return new WaitForSeconds(1f);

            resultBtn.interactable = true;
            //QuestManager.instance.CheckAction(Action);
        }

        public void Selesai()
        {
            QuestManager.instance.CheckAction(Action);
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
    public class Barang
    {
        public string NamaBarang;
        public GameObject listText;
        public GameObject barang;
        public Sprite gambar;
        public bool correct;


    }
}
