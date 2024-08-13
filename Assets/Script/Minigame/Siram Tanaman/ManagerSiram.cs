using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EasyTransition;

public class ManagerSiram : MonoBehaviour
{
    public static ManagerSiram instance;
    public Transform[] point;
    public TMP_Text textScore;
    public GameObject GameOver;
    public GameObject bintang;
    int value;
    public playerProfil profil;

    private void Start()
    {
        instance = this;
    }
    public void mulaiGame()
    {

    }




    public void scoreGame(GameObject position)
    {
        StartCoroutine(bintang.GetComponent<Star>().changeSkor(position));
        Invoke("setScore",2f);
    }

    void setScore()
    {
        value += 1;
        if (value >= point.Length)
        {
            GameOver.SetActive(true);
            Invoke("pindahScene", 1.5f);
        }
        textScore.text = value + "/"+point.Length;
    }

    public void pindahScene()
    {
        PlayerPrefs.SetInt("indexAnimasi", 0);
        FindObjectOfType<DemoLoadScene>().LoadScene("Animasi");
    }
}
