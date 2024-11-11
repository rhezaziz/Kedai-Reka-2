using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbilBenda : MonoBehaviour
{
    public Items item;


    public void Ambil()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        gameObject.SetActive(false);
        player.profil.item.Add(item);
    }
}
