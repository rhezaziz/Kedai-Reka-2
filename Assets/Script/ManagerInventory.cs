using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ManagerInventory : MonoBehaviour
{
    public GameObject prefabs;
    public Transform parent;

    public playerProfil profil;


    public void mulai()
    {
        if(parent.transform.childCount > 1)
        {
            lihatItem();
        }
        else
        {
            initInventory();
        }
    }
    void initInventory()
    {
        GameObject item;

        for(int i = 0; i < profil.item.Count; i++)
        {
            //profil.name = profil.item[i].name;
            item = Instantiate(prefabs);
            item.name = profil.item[i].namaItem;
            Image gambarItem = item.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            TMP_Text namaItem = item.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();

            gambarItem.sprite = profil.item[i].gambarItem;
            namaItem.text = profil.item[i].namaItem;
            item.transform.SetParent(parent);
            item.transform.localScale = new Vector2(1f, 1f);
            item.transform.Rotate(0f, 0f, -90, Space.Self);
            if (profil.item[i].isShop)
            {
                item.SetActive(false);
            }
            else
            {
                item.SetActive(true);
            }
        }
    }

    void lihatItem()
    {
        for(int i = 0; i < parent.childCount - 1; i++)
        {
            if (profil.item[i].isShop)
                parent.GetChild(i + 1).gameObject.SetActive(false);
        
            else
                parent.GetChild(i + 1).gameObject.SetActive(true);
        }
    }
}
