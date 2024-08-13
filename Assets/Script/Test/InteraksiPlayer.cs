using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraksiPlayer : MonoBehaviour
{
    bool interaksi;
    GameObject ObjectGame;
    bool animasi = false;
    public static bool Interaksi;
    //public List<Interaksi> list_Interaksi;
    private void OnTriggerEnter(Collider other)
    {
        if(other != null && other.GetComponent<interaksi_Object>() != null)
        {
            interaksi = true;
            ObjectGame = other.gameObject;
        }       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.GetComponent<interaksi_Object>() != null)
        {
            interaksi = false;
            ObjectGame = null;
        }
    }



    private void Update()
    {
        interaksiAction();
    }

    void interaksiAction()
    {
        if(Input.GetKeyDown(KeyCode.Space) && interaksi)
        {
            //Debug.Log("Kena");
            ObjectGame.GetComponent<interaksi_Object>().action(transform);
        }
    }

    public void ChangeAnimation(string boolean, string trigger)
    {
        Animator anim = GetComponentInChildren<Animator>();
        animasi = !animasi;

        if(!animasi)
            anim.SetTrigger(trigger);
        
        anim.SetBool(boolean, animasi);
    }
}

//Interaksi Ngobrol
interface interaksi_Object
{
    public void action(Transform player = null);
}

//Ambil Item
interface Ambil
{
    public void ambilBarang();
}

public enum Interaksi
{
    Kucing,
    Masak,
    Ngobrol,
    Tanaman,
    Siram_Tanaman,
    Nonton
}