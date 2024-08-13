using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_IPad : MonoBehaviour
{

    public RectTransform ipadUI;


    public GameObject prefabShopItem;
    public void animasiUI_Ipad()
    {
        ipadUI.gameObject.SetActive(true);
        /*
        ipadUI.anchorMin = new Vector2(0.5f, 0.5f);
        ipadUI.anchorMax = new Vector2(0.5f, 0.5f);

        Vector2 size = ipadUI.sizeDelta;
        Debug.Log(size);
        //ipadUI.anchorMin = new Vector2(0f, 0f);
        ///ipadUI.anchorMax = new Vector2(1f, 1f);

        iPad.sizeDelta = new Vector2(size.x, 0f);
        */
        //iPad.DOSizeDelta(new Vector2(size.x, size.y), 2.5f);

        RectTransform iPad = ipadUI.transform.GetChild(0).GetComponent<RectTransform>();
        iPad.transform.DOScaleY(1f, 1.5f);
    }


    void init_Kegiatan()
    {

    }

    void init_waktu()
    {

    }


    void init_Shop()
    {

    }

    void init_inventory()
    {

    }
}
