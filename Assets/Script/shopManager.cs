using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Globalization;

public class shopManager : MonoBehaviour
{
    public GameObject prefbas;

    public GameObject parent;

    public playerProfil profil;

    public GameObject panelKonfirmasi;

    public TMP_Text textSaldo;




    void initShop()
    {
        GameObject itemShop;
        //textSaldo.text = "Saldo : "+ "Rp" + string.Format("{0:n}",profil.Saldo);
        NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
        textSaldo.text = "Saldo : " + "Rp" + profil.Saldo.ToString("n0", info);

        for (int i = 0; i < profil.item.Count; i++)
        {
            itemShop = Instantiate(prefbas);

            itemShop.name = i.ToString();

            itemShop.SetActive(true);
            Image gambar = itemShop.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            TMP_Text textNama = itemShop.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
            TMP_Text textHarga = itemShop.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>();

            TMP_Text textButton = itemShop.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<TMP_Text>();

            Button btnBeli = itemShop.transform.GetChild(0).GetChild(3).GetComponent<Button>();

            gambar.sprite = profil.item[i].gambarItem;
            textNama.text = profil.item[i].namaItem;
            textHarga.text = "Rp" + profil.item[i].Harga.ToString("n0",info);
            itemShop.transform.SetParent(parent.transform);
            itemShop.transform.localScale = new Vector2(1f, 1f);
            itemShop.transform.Rotate(0f, 0f, -90, Space.Self);
            btnBeli.name = i.ToString();
            if (!profil.item[i].isShop)
            {
                btnBeli.interactable = false;
                textButton.text = "Punya";
            }
            else
            {
                btnBeli.interactable = true;
                btnBeli.onClick.AddListener(() => buyItem(itemShop.transform, btnBeli));
            }
        }
    }

    void buyItem(Transform itemKlik, Button btnBeli)
    {
        Debug.Log(btnBeli.transform.parent.name);
        int index = int.Parse(btnBeli.name);

        int harga = profil.item[index].Harga;

        int saldo = profil.Saldo;

        Debug.Log(index);
        if(saldo >= harga)
        {
            profil.Saldo -= harga;
            profil.item[index].isShop = false;
            Debug.Log(gameObject.name);
            btnBeli.interactable = false;
            btnBeli.transform.GetChild(0).GetComponent<TMP_Text>().text = "Punya";
            NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
            textSaldo.text = "Saldo : " + "Rp" + profil.Saldo.ToString("n0", info);
            FindObjectOfType<UIManager>().updateCoint();
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


