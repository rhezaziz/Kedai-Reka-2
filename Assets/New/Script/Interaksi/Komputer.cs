using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru
{

    public class Komputer : MonoBehaviour, Interaction
    {
        public Transform position;
        public GameObject uiRekruit;

        public bool value;

        public bool isTutorial(){
            return value;
        }

        
        void OnDisable(){
            value = false;
        }
        
        void OnEnable(){
            value = true;
        }


        void Start(){

        }

        public void action(Transform player)
        {
            UiManager.instance.displayRekrut(player.GetComponent<Controller>().profil);
        }

        public void btnActive(GameObject btn, bool interactable){
            btn.SetActive(interactable);
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Lihat Komputer";
        }
    }
}
