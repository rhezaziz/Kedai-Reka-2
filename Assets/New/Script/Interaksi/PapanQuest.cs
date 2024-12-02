using System.Collections;
using System.Collections.Generic;
using Check;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Terbaru{

    public class PapanQuest : MonoBehaviour, Interaction, IDialog
    {
        public Dialog dialog;
        public Transform Berdiri;

        public List<int> HariEvent = new List<int>();

        public bool value;

        public void isTutorial(bool temp){
            //enabled = temp;
        }

        public bool haveNarasi;

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
            if(Interactable())
                checkDistance();
        }

        bool interactable;
        public bool Interactable(){
            return interactable;
        }

        public void changeInteractable(bool value){
            interactable = value;
            haveNarasi = value;
            // return interactable;
        }
        public GameObject quest;
        public void action(Transform player){
            point.gameObject.SetActive(false);
            if (FindObjectOfType<Controller>().profil.Energy >= 2)
            {
                player.position = new Vector3(Berdiri.position.x, player.position.y, Berdiri.position.z);
                int hari = FindObjectOfType<DayManager>().day;
                if (HariEvent.Contains(hari) && haveNarasi)
                {
                    int index = HariEvent.IndexOf(hari);
                    UiManager.instance.bantuanText("");
                    //Debug.Log(index);
                    FindObjectOfType<Narasi>().haveNarasi(index);
                    changeInteractable(false);
                }
                else
                {
                    FindObjectOfType<Misi_Manager>().initKontenQuest();
                    quest.SetActive(true);
                }
            }
            else
            {
                //Player.GetComponent<Controller>().currentState(state.Interaction);
                UiManager.instance.startChinematic();

                UiManager.instance.panelUtama.SetActive(false);

                Invoke("startDialog", 1f);
            }
            
        }

        public bool haveCerita()
        {
            int hari = FindObjectOfType<DayManager>().day;
            return HariEvent.Contains(hari) && haveNarasi;
        }

        public void btnActive(GameObject btn, bool interactable){
            bool isActive = interactable && !QuestManager.instance.isActive || 
                    interactable && Interactable();
            btn.GetComponent<UnityEngine.UI.Button>().interactable = Interactable();
            
            btn.SetActive(isActive);
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Lihat Tugas";

        }

        public void endDialog()
        {
            UiManager.instance.ChinematicPanel.endChinematic();

            Player.GetComponentInChildren<Animator>().SetBool("Ngomong", false);
//Player.GetComponent<Controller>().currentState(state.Default);
            //UiManager.instance.panelUtama.SetActive(true);
            //FindObjectOfType<QuestManager>().CheckAction(tempAction);

            FindObjectOfType<DialogManager>().closeDialog();
        }

        public void startDialog()
        {

            Player.GetComponentInChildren<Animator>().SetBool("Ngomong", true);

            FindObjectOfType<DialogManager>().StartDialog(dialog, null);
        }
    }
}