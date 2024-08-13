using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumbuhan : MonoBehaviour
{
    float value = 0;
    public GameObject panelCount;
    public UnityEngine.UI.Image valueCount;
    public Color color;
    bool isDono;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Count(collision.gameObject));
            collision.GetComponent<WaterCan>().EnableSiram();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopAllCoroutines();
            panelCount.SetActive(false);
            collision.GetComponent<WaterCan>().DisableSiram();
        }
    }

    IEnumerator Count(GameObject waterCan)
    {
        valueCount.fillAmount = 0f;
        panelCount.SetActive(true);
        while(value < 1)
        {
            valueCount.fillAmount = value / 1f;
            yield return new WaitForSeconds(0.1f);
            value += 0.01f;
        }
        ManagerSiram.instance.scoreGame(gameObject);
        panelCount.SetActive(false);
        waterCan.GetComponent<WaterCan>().DisableSiram();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = color; 
    }

}
