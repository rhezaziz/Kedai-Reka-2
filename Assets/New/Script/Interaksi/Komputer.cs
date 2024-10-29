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

        public void isTutorial(bool temp){
            //enabled = temp;
        }

        
        void OnDisable(){
            value = false;
        }
        
        void OnEnable(){
            value = true;
        }

        public Transform Player;
        public GameObject point;
        public float distancePlayer;
        public void checkDistance(){
            float distance = Vector3.Distance(transform.position, Player.position);

            if(distancePlayer >= distance){
                point.gameObject.SetActive(true);
            }else{
                point.gameObject.SetActive(false);
            } 
        }

        void Update(){
            checkDistance();
        }

        bool interactable;
        public bool Interactable(){
            return interactable;
        }

        public void changeInteractable(bool value){
            interactable = value;
            //return interactable;
        }

        public void action(Transform player)
        {
            UiManager.instance.displayRekrut(player.GetComponent<Controller>().profil);
            SoundManager.instance.sfx(34);
        }

        public void btnActive(GameObject btn, bool interactable){
            btn.SetActive(interactable);
            btn.GetComponent<UnityEngine.UI.Button>().interactable = Interactable();
            
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Lihat Komputer";
        }
    }
}
