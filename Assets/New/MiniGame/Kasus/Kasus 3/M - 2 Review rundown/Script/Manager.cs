using System.Collections;
using System.Collections.Generic;
using Terbaru;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGame3_2{
    public class Manager : MonoBehaviour
    {
        public bool isDaily;
        public Text jumlahText;
        public int jumlah;
        int currentJumlah;

        public List<Button> listKegiatan = new List<Button>();

        [Header("Value Akhir")]
        public Sprite Benar;
        public Sprite Salah;
        public Color Terpilih;

        void Start(){
            jumlahText.text = $"{currentJumlah}/{jumlah}";
        }

        List<bool> values = new List<bool>();
        public void pilih(SelectedList data){
            values.Add(data.value);
            data.pilih();

            currentJumlah += 1;
            jumlahText.text = $"{currentJumlah}/{jumlah}";
            data.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = Color.black;

            if(currentJumlah >= jumlah){
                Selesai();
            }
        }

        public void garis(){

        }

        void Selesai(){
            foreach(var kegiatan in listKegiatan){
                kegiatan.enabled = false;
            }

            StartCoroutine(akhir());

        }
        public string action;
        IEnumerator akhir(){

            yield return new WaitForSeconds(1f);
            foreach(var kegiatan in listKegiatan){
                var temp = kegiatan.GetComponent<SelectedList>();
                
                if(temp.selected)
                    continue;

                var sprite = temp.value ? Benar : Salah;
                int point = temp.value ? 50 : 0;
                if(!isDaily) QuestManager.instance.currentQuest.quest.pointBonus += point;
                var indikatorValue = kegiatan.transform.GetChild(0).GetChild(0).GetComponent<Image>();

                indikatorValue.color = Color.white;
                indikatorValue.sprite = sprite;
                yield return new WaitForSeconds(1f);
                // if(testGame)
                //     balikMainMenu();
            }

            yield return new WaitForSeconds(1f);
            if(!isDaily)
                QuestManager.instance.CheckAction(action);
            else
                QuestManager.instance.CheckActionQuest(action);
        }

        // public bool testGame = true;
        // void balikMainMenu(){
        //     FindObjectOfType<MainMenu>().PindahScene("New Scene");
        // }

        // void Update(){
        //     if(Input.GetKeyDown(KeyCode.Escape) && testGame){
        //         testGame = false;
        //         balikMainMenu();
        //     }
            
        
    }
}
