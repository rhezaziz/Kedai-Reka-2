using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using DG.Tweening;


namespace MiniGame2_3{
    public class Manager : MonoBehaviour
    {
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
            textUang.text = "Saldo : " + "Rp" + jumlahUang.ToString("n0", info);
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
            
            textUang.text = "Saldo : " + "Rp" + jumlahUang.ToString("n0", info);

            checkUang();
        }


        void checkUang(){
            foreach(var item in items){
                if(item.hargaBarang > jumlahUang){
                    if(!item.done){
                        RectTransform panel = item.panel.GetComponent<RectTransform>();
                        panel.DOSizeDelta(new Vector2(panel.sizeDelta.x, 60), 1f);
                        panel.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                        item.done = true;
                    }
   
                    
                    //item.panel. = false;
                }
            }
        }
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
        public GameObject panel;
        public int hargaBarang;
        public int jumlahBarang;
        public TMPro.TMP_Text jumlahBarangText;
    }

    
}
