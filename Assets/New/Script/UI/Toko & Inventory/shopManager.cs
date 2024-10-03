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

        public GameObject Paket;

        void Awake(){
            instance = this;
        }


        void initShop()
        {
            profil =  GameManager.instance.profil;

            
            //textSaldo.text = "Saldo : "+ "Rp" + string.Format("{0:n}",profil.Saldo);
            NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
            textSaldo.text = "Saldo : " + "Rp" + profil.Saldo.ToString("n0", info);

            foreach(var item in profil.item){
                if(!item.isShop){
                    return;
                }

                GameObject itemShop = Instantiate(prefbas);

                //itemShop.name = i.ToString();

                itemShop.SetActive(true);
                itemShop.transform.SetParent(parent.transform);
                itemShop.transform.localScale = new Vector2(1f, 1f);
                itemShop.transform.Rotate(0f, 0f, -90, Space.Self);
                itemShop.GetComponent<ContainerItem>().ItemInShop(item, info);
            
            }
        }

        public void buyItem(Items item, Button btnBeli)
        {
            int harga = item.Harga;
            int saldo = profil.Saldo;
            
            if(saldo >= harga)
            {
                Debug.Log(Paket);
                if(!Paket.activeInHierarchy){
                    Debug.Log("Non Activa");
                    Paket.SetActive(true);
                    Paket.GetComponent<GrabableItem>().item.Clear();
                }
                Debug.Log("Active");
                profil.Saldo -= harga;
                item.isShop = false;
                btnBeli.GetComponent<Image>().enabled = false;
                btnBeli.transform.GetChild(0).GetComponent<TMP_Text>().text = "Habis";
                NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
                textSaldo.text = "Saldo : " + "Rp" + profil.Saldo.ToString("n0", info);
                FindObjectOfType<UiManager>().UpdateSaldo(profil.Saldo);
                Paket.GetComponent<GrabableItem>().item.Add(item);
            }
            else
            {
                panelKonfirmasi.SetActive(true);
            }
        }


        public void lihatShop()
        {
            bool kosong = parent.transform.childCount < 2;

            if (kosong)
                initShop();
        }
    }    
}


