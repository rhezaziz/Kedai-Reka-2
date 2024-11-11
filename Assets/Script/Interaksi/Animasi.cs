using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animasi : MonoBehaviour
{
    private Animator anim;
    public GameObject panelAnimasi;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void startAnimasi(string AnimasiNama)
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        float PosX = transform.GetChild(0).position.x;
        float PosY = player.transform.position.y;
        float PosZ = transform.GetChild(0).position.z;

        player.transform.position = new Vector3(PosX, PosY, PosZ);

        player.startAnimation(AnimasiNama);

        anim.SetTrigger("Animasi");
        panelAnimasi.SetActive(true);
    }

    public void startAnimasi(int index)
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        float PosX = transform.GetChild(0).position.x;
        float PosY = player.transform.position.y;
        float PosZ = transform.GetChild(0).position.z;

        player.profil.position = new Vector3(PosX, PosY, PosZ);

        PlayerPrefs.SetInt("indexAnimasi", index);
        FindObjectOfType<DemoLoadScene>().LoadScene("Animasi");

    }

    public void endAnimasi()
    {
        panelAnimasi.SetActive(false);
    }
}
