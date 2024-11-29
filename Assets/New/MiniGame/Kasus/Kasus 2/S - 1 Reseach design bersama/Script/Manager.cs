using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using DG.Tweening;
using Terbaru;
using UnityEngine.UI;



namespace MiniGame2_3{
    public class Manager : MonoBehaviour
    {
        public Sprite Wrong, Correct;
        public Button reset, check;
        public int jumlahUang;
        public TMPro.TMP_Text textUang;
        public string action;
        public List<dataItem> items = new List<dataItem>();
        NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
        
// #region barang
//         [Header("Keran")]
//         public int hargaKeran;
//         public int jumlahKeran;

//         [Header("Pipa Besar")]
//         public int hargaPipaBesar;
//         public int jumlahPipaBesar;

//         [Header("Pipa Kecil")]
//         public int hargaPipaKecil;
//         public int jumlahPipaKecil;
// #endregion
        
//         public void beliPipaKecil(){
//             jumlahUang -= hargaPipaKecil;
//             jumlahPipaKecil += 1;
//         }

//         public void beliPipaBesar(){
//             jumlahUang -= hargaPipaBesar;
//             jumlahPipaBesar += 1;
//         }

//         public void beliKeran(){
//             jumlahUang -= hargaKeran;
//             jumlahKeran += 1;
//         }
        void Start(){
            textUang.text ="Rp" + jumlahUang.ToString("n0", info) + ",-";
        }

        public void checkJawaban(string type){
            for(int i = 0; i < items.Count; i++){
                
                if(System.Enum.TryParse(type, out typeBarang result)){
                    if(result == items[i].type){
                        jumlahUang -= items[i].hargaBarang;

                        updateData(items[i]);


                    }
                }
            }
        }

        void updateData(dataItem value){
            var jumlahBarangText = value.jumlahBarangText;
            value.jumlahBarang += 1;
            jumlahBarangText.text = $"{value.jumlahBarang}";
            
            textUang.text ="Rp" + jumlahUang.ToString("n0", info )+ ",-";
            reset.interactable = true;
            checkUang();
        }


        public void resetGame(){
            jumlahUang = 200000;

            foreach(var value in items){
                value.jumlahBarang = -1;
                GameObject outOfMoney = value.PanelItem.transform.GetChild(0).gameObject;
                outOfMoney.SetActive(false);

                value.PanelItem.interactable = true;
                // panel.DOSizeDelta(new Vector2(panel.sizeDelta.x, 60), 1f);
                // panel.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                value.done = false;
                updateData(value);
            }

            reset.interactable = false;
        }

        public void checkGame(){
            reset.interactable = false;
            check.interactable = false;
            foreach(var item in items){
                item.panelResult.gameObject.SetActive(true);
                item.PanelItem.interactable = false;
            }
            StartCoroutine(checkResult());
        }

        IEnumerator checkResult(){
            foreach(var item in items){
                item.PanelItem.transform.GetChild(0).gameObject.SetActive(false);
                item.PanelItem.interactable = false;
                bool hasil = item.jumlahBarang == item.jumlahSesuai;
                Sprite tempSprite = hasil ? Correct : Wrong;

                item.panelResult.sprite = tempSprite;
                int point = hasil ? 50 : 0;

                item.panelResult.DOColor(new Color(1f,1f,1f,1f),1f);

                QuestManager.instance.currentQuest.quest.pointBonus += point;

                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(5f);

            QuestManager.instance.CheckAction(action);



            
            

        }

        int index;
        void checkUang(){
            foreach(var item in items){
                if(item.hargaBarang > jumlahUang){
                    if(!item.done){
                        index += 1;
                        GameObject outOfMoney = item.PanelItem.transform.GetChild(0).gameObject;
                        outOfMoney.SetActive(true);
                        item.PanelItem.interactable = false;
                        // panel.DOSizeDelta(new Vector2(panel.sizeDelta.x, 60), 1f);
                        // panel.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                        item.done = true;
                    }
                    //else
                    //{
                    //    GameObject outOfMoney = item.PanelItem.transform.GetChild(0).gameObject;
                    //    outOfMoney.SetActive(false);
                    //    item.PanelItem.interactable = true;
                    //    // panel.DOSizeDelta(new Vector2(panel.sizeDelta.x, 60), 1f);
                    //    // panel.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                    //    item.done = false;
                    //}
   
                    
                    //item.panel. = false;
                }

                if(index >= items.Count){
                    check.interactable = true;
                }
            }
        }

        public bool testGame = true;
        
    }

    public enum typeBarang{
        keran,
        pipaKecil,
        pipaBesar
    }

    [System.Serializable]
    public class dataItem{
        public typeBarang type;
        public bool done;
        public UnityEngine.UI.Button PanelItem;
        public int hargaBarang;
        public int jumlahBarang;
        public Image panelResult;
        public TMPro.TMP_Text jumlahBarangText;
        public int jumlahSesuai;
    }

    
}
