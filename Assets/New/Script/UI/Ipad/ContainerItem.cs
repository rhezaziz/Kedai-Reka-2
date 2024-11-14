using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

namespace Terbaru{
    public class ContainerItem : MonoBehaviour
    {
        
        [Header("Universal")]
        public Image gambarItem;
        public TMP_Text namaItem;

        [Header("Item di Shop")]
        public TMP_Text hargaItem;
        public Button btnBeli;

        public Sprite Beli, punya;

        public void ItemInShop(Items item, NumberFormatInfo info)
        {
            gambarItem.sprite = item.gambarItem;
            namaItem.text = item.namaItem;
            gambarItem.name = $"Item {item.namaItem}";
            hargaItem.text = item.Harga.ToString("n0", info);
            btnBeli.name = $"Beli {item.namaItem}";
            btnBeli.interactable = true;
            if (item.isInventory)
            {
                btnBeli.enabled = false;
                //btnBeli.GetComponent<Butto>().enabled = false;
                btnBeli.GetComponent<Image>().sprite = punya;
                //btnBeli.transform.GetChild(0).GetComponent<TMP_Text>().text = "Punya";
            }
            else if (item.Terbeli)
            {
                btnBeli.enabled = false;
                btnBeli.interactable = false;
                //btnBeli.GetComponent<Butto>().enabled = false;
                //btnBeli.GetComponent<Image>().sprite = punya;
                //btnBeli.transform.GetChild(0).GetComponent<TMP_Text>().text = "Punya";
            }
            else if (!item.isInventory && !item.Terbeli)
            {
                //btnBeli.GetComponent<Image>().enabled = true;
                btnBeli.enabled = true;
                btnBeli.GetComponent<Image>().sprite = Beli;
                btnBeli.onClick.AddListener(() => shopManager.instance.buyItem(item, btnBeli));
            }
        }

        public void ItemInInventory(Items item){
            gambarItem.sprite = item.gambarItem;
            namaItem.text = item.namaItem;

            if (!item.isInventory)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}