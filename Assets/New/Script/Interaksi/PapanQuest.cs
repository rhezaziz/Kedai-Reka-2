using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru{

    public class PapanQuest : MonoBehaviour, Interaction
    {
        public Transform Berdiri;

        

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
        public GameObject quest;
        public void action(Transform player){
            player.position = new Vector3(Berdiri.position.x, player.position.y, Berdiri.position.z);
            FindObjectOfType<Misi_Manager>().initKontenQuest();
            quest.SetActive(true);
        }

        public void btnActive(GameObject btn, bool interactable){
            bool isActive = interactable && !QuestManager.instance.isActive;

            btn.SetActive(isActive);
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Lihat Tugas";

        }
    }
}