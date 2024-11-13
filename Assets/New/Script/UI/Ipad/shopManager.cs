using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Globalization;

namespace Terbaru{

    public class shopManager : MonoBehaviour
    {

        public static shopManager instance;
        public GameObject prefbas;

        public GameObject parent;

         playerProfil profil;

        public GameObject panelKonfirmasi;

        public TMP_Text textSaldo;

        public float time;

        public GameObject Paket;

        List<itemInfo> items = new List<itemInfo>();

        void Awake(){
            instance = this;
        }


        void initShop()
        {
            profil =  GameManager.instance.profil;
            itemTerbeli = false;

            
            //textSaldo.text = "Saldo : "+ "Rp" + string.Format("{0:n}",profil.Saldo);
            NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
            textSaldo.text = "Point : "  + profil.Saldo.ToString("n0", info);

            foreach(var item in profil.item){
                //if(){
                //    return;
                //}

                GameObject itemShop = Instantiate(prefbas);

                //itemShop.name = i.ToString();

                itemShop.SetActive(item.isShop);
                itemShop.transform.SetParent(parent.transform);
                itemShop.transform.localScale = new Vector2(1f, 1f);
                //itemShop.transform.Rotate(0f, 0f, -90, Space.Self);
                itemShop.GetComponent<ContainerItem>().ItemInShop(item, info);
                var temp = new itemInfo
                (
                    itemShop,
                    item
                );
                
                items.Add(temp);
            }
        }

        bool itemTerbeli;

        public void buyItem(Items item, Button btnBeli)
        {
            int harga = item.Harga;
            int saldo = profil.Saldo;
            
            if(saldo >= harga)
            {
                itemTerbeli = true;
                item.Terbeli = true;
                Debug.Log(Paket);
                
                Debug.Log("Active");
                profil.Saldo -= harga;
                //item.isShop = false;
                btnBeli.interactable = !item.Terbeli;
//btnBeli.transform.GetChild(0).GetComponent<TMP_Text>().text = "Habis";
                NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
                textSaldo.text = "Point : " + profil.Saldo.ToString("n0", info);
                FindObjectOfType<UiManager>().UpdateSaldo(profil.Saldo);
                Paket.GetComponent<GrabableItem>().item.Add(item);
                
            }
            else
            {
                panelKonfirmasi.SetActive(true);
            }
        }

        public void closeShop(){
            if(itemTerbeli)
                StartCoroutine(paketData());
        }


        IEnumerator paketData(){
            yield return new WaitForSeconds(time);
            if(!Paket.activeInHierarchy){
                Debug.Log("Non Activa");
                Paket.SetActive(true);
                Paket.GetComponent<Interaction>().changeInteractable(true);
                //Paket.GetComponent<GrabableItem>().item.Clear();
            }

            SoundManager.instance.sfx(0);
            itemTerbeli = false;
        }

        void initItem()
        {
            profil = GameManager.instance.profil;
            itemTerbeli = false;


            //textSaldo.text = "Saldo : "+ "Rp" + string.Format("{0:n}",profil.Saldo);
            NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
            textSaldo.text = "Point : " + profil.Saldo.ToString("n0", info);

            foreach (var item in items)
            {
                //if (!item.itemData.isShop)
                //{
                //    return;
                //}

                GameObject itemShop = item.itemObj;

                //itemShop.name = i.ToString();

                itemShop.SetActive(item.itemData.isShop);
                //itemShop.transform.SetParent(parent.transform);
                //itemShop.transform.localScale = new Vector2(1f, 1f);
                //itemShop.transform.Rotate(0f, 0f, -90, Space.Self);
                itemShop.GetComponent<ContainerItem>().ItemInShop(item.itemData, info);

            }
        }


        public void lihatShop()
        {
            bool kosong = parent.transform.childCount < 2;

            if (kosong)
                initShop();
            else
                initItem();

        }
    }
    [System.Serializable]
    public class itemInfo
    {
        public GameObject itemObj;
        public Items itemData;

        public itemInfo(GameObject itemObj, Items itemData) { 
            this.itemObj = itemObj;
            this.itemData = itemData;
        }
    }
    

    
}


