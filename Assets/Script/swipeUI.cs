using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class swipeUI : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public Slider slide;
    public GameObject Tangan, MainMenu, lockScreen;
    public Transform slideText;
    public Transform toggle;

    bool animated;
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
       // Debug.Log(slide.value);
       
        if(slide.value != 1)
        {
            //slide.value = 0;
            InvokeRepeating("DecresaseValue", 0.1f, 0.1f);
        }
        else
        {
            Tangan.SetActive(true);
            lockScreen.SetActive(false);
            MainMenu.SetActive(true);
            slide.value = 0;
            SoundManager.instance.uiSFX(12);
            //Debug.Log("Selesai");
        }
    }

    void OnEnable()
    {
       // animated = true;
        StartCoroutine(mulaiAnimasi());
    }
    private void OnDisable()
    {
        StopCoroutine(mulaiAnimasi());
    }



    IEnumerator mulaiAnimasi()
    {
        while (true)
        {
            Sequence anim = DOTween.Sequence();

            anim.Append(toggle.DOLocalMoveX(15f , 1f));
            anim.Append(toggle.DOLocalMoveX(0f, 1f));
            anim.SetLoops(-1, LoopType.Yoyo);
            slideText.DOScaleX(1f, 1.5f);
            yield return new WaitForSeconds(2.5f);
           // Debug.Log("Animasi");
            slideText.localScale = new Vector2(0f, 1f);
        }
    }


    void DecresaseValue()
    {
        slide.value -= 0.1f;
        if(slide.value < 0)
        {
            CancelInvoke("DecresaseValue");
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    { 
        CancelInvoke("DecresaseValue"); 
    }
}
