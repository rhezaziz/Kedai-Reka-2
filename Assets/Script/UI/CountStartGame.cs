using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class CountStartGame : MonoBehaviour
{
    [SerializeField] private int Timer = 3;
    public TMPro.TMP_Text countText;

    public UnityEvent startGame;

    public void mulaiGame()
    {
        StartCoroutine(StartCount());
    }

    IEnumerator StartCount()
    {
        countText.transform.localScale = Vector3.zero;
        while(Timer >= 0)
        {

            if(Timer != 0)
            {
                countText.text = Timer.ToString();
            }
            else
            {
                countText.text = "Mulai!";
            }
            
            countText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetEase(Ease.InBounce);
            yield return new WaitForSeconds(1f);

            countText.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.5f);
            Timer -= 1;
        }

        startGame?.Invoke();
    }

    public void test()
    {
        Debug.Log("Mulai");
        gameObject.SetActive(false);
    }
}
