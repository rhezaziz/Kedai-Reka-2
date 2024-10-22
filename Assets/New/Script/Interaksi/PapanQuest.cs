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
            enabled = temp;
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
            int hari = FindObjectOfType<DayManager>().day;
            if(HariEvent.Contains(hari)){
                FindObjectOfType<Narasi>().startDialog();
            }else{    
                FindObjectOfType<Misi_Manager>().initKontenQuest();
                quest.SetActive(true);
            }
        }

        public void btnActive(GameObject btn, bool interactable){
            bool isActive = interactable && !QuestManager.instance.isActive;

            btn.SetActive(isActive);
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Lihat Tugas";

        }
    }
}