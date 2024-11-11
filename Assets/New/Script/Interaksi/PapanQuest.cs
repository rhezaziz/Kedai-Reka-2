using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru{

    public class PapanQuest : MonoBehaviour, Interaction
    {
        public Transform Berdiri;

        public List<int> HariEvent = new List<int>();

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
            // return interactable;
        }
        public GameObject quest;
        public void action(Transform player){
            player.position = new Vector3(Berdiri.position.x, player.position.y, Berdiri.position.z);
            int hari = FindObjectOfType<DayManager>().day;
            if(HariEvent.Contains(hari)){
                int index = HariEvent.IndexOf(hari);
                //Debug.Log(index);
                FindObjectOfType<Narasi>().haveNarasi(index);
                changeInteractable(false);
            }else{    
                FindObjectOfType<Misi_Manager>().initKontenQuest();
                quest.SetActive(true);
            }
        }

        public void btnActive(GameObject btn, bool interactable){
            bool isActive = interactable && !QuestManager.instance.isActive || 
                    interactable && Interactable();
            btn.GetComponent<UnityEngine.UI.Button>().interactable = Interactable();
            
            btn.SetActive(isActive);
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Lihat Tugas";

        }
    }
}