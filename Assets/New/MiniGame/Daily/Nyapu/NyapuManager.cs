using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;
using System.Drawing;
using EasyTransition;

public class NyapuManager : MonoBehaviour
{
    public TMP_Text textScore;


    public static NyapuManager instance;

    public GameObject bintang;

    public Transform[] Noda;

    public GameObject GameOver;
    public playerProfil profil;

    int skor;
    private void Start()
    {
        instance = this;
    }

    public void changeSkor(GameObject position)
    {
        /*
        bintang.transform.localScale = Vector2.zero;
        bintang.transform.position = Noda[index].position;
        bintang.SetActive(true);
        bintang.transform.DOScale(new Vector2(.15f, .15f), 0.5f).SetEase(Ease.OutBounce);
        Noda[index].GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1f);

        bintang.transform.DOMove(UISkor.transform.position, 1f);
        yield return new WaitForSeconds(1f);
        textScore.text = scoreGame() + "/" + Noda.Length;
        yield return new WaitForSeconds(0.1f);
        bintang.SetActive(false);
        */

        StartCoroutine(bintang.GetComponent<Star>().changeSkor(position));
        Invoke("scoreGame", 2f);
    }

    void scoreGame()
    {
        skor += 1;
        if(skor >= Noda.Length)
        {
            GameOver.SetActive(true);
            Invoke("pindahScene", 1.5f);
        }

        textScore.text = skor + "/" + Noda.Length;
    }
    public void updateScore()
    {

    }

    public void pindahScene()
    {
        PlayerPrefs.SetInt("indexAnimasi", 1);
        FindObjectOfType<Terbaru.MiniGame>().pindah("Nyapu");
        FindObjectOfType<Terbaru.QuestManager>().CheckActionQuest("Nyapu");
    }


    // Start is called before the first frame update

}
