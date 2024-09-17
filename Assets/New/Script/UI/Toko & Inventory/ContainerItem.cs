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

        public void ItemInShop(Items item, NumberFormatInfo info){
            gambarItem.sprite = item.gambarItem;
            namaItem.text = item.namaItem;
            gambarItem.name = $"Item {item.namaItem}";
            hargaItem.text = "Rp" + item.Harga.ToString("n0",info);
            btnBeli.name = $"Beli {item.namaItem}";
            if (!item.isShop)
            {
                btnBeli.interactable = false;
                btnBeli.GetComponent<Image>().enabled = false;
                btnBeli.transform.GetChild(0).GetComponent<TMP_Text>().text = "Punya";
            }
            else
            {
                
                btnBeli.GetComponent<Image>().enabled = true;
                btnBeli.interactable = true;
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