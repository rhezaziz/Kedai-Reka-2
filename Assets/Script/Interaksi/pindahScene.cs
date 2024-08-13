using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pindahScene : MonoBehaviour
{
    [SerializeField] private string namaScene;
    [SerializeField] private Transform spawnPoint;

    public GameObject Interaction;

    public UnityEngine.Events.UnityEvent action;

    public void interaction() => action?.Invoke();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(action != null)
            {
                interaction();
            }
            else
            {
                pindah();
            }
        }
    }

    

    public void pindah()
    {
        

        PlayerController player = FindObjectOfType<PlayerController>();

        float PosX = transform.GetChild(0).position.x;
        float PosY = player.transform.position.y;
        float PosZ = transform.GetChild(0).position.z;

        player.profil.position = new Vector3(PosX, PosY, PosZ);
        FindObjectOfType<DemoLoadScene>().LoadScene(namaScene);

    }


    public void indexAnimasi(int index)
    {
        PlayerPrefs.SetInt("indexAnimasi", index);
    }
}
