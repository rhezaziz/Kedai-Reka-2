using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

namespace Terbaru{
    public class ContainerItem : MonoBehaviour
    {
        public Image gambarItem;
        public TMP_Text namaItem;
        public TMP_Text hargaItem;
        public Button btnBeli;
        Items _item;

        public void ItemInShop(Items item, NumberFormatInfo info){
            _item = item;
            gambarItem.sprite = item.gambarItem;
            namaItem.text = item.namaItem;
            hargaItem.text = "Rp" + item.Harga.ToString("n0",info);

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
    }
}