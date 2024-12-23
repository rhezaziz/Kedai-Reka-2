using System.Collections;
using System.Collections.Generic;
using Terbaru;
using UnityEngine;

namespace Terbaru{

    public class TestDialog : MonoBehaviour, Interaction, IDialog
    {
        public Dialog dialog;

        NPC_Controller controller;

        //GameObject Player;
        public string tempAction;
        public void isTutorial(bool temp){
        }

        bool interactable;
        public bool Interactable(){
            return interactable;
        }

        public void changeInteractable(bool value){
            interactable = value;
            // return interactable;
        }

        public GameObject Player;
        public GameObject point;
        public float distancePlayer;
        public void checkDistance(){
            float distance = Vector3.Distance(transform.position, Player.transform.position);

            if(distancePlayer >= distance){
                point.gameObject.SetActive(true);
            }else{
                point.gameObject.SetActive(false);
            } 
        }

        void Update(){
            checkDistance();
        }

        public void action(Transform player){
            Player = player.gameObject;
            Player.GetComponent<Controller>().currentState(state.Interaction);
            //UiManager.instance.Chinematic(true);
            UiManager.instance.startChinematic();
            UiManager.instance.panelUtama.SetActive(false);
            
            
            Invoke("startDialog", 1f);


            
        }

        public void btnActive(GameObject btn, bool interactable){
            btn.SetActive(interactable);
            btn.GetComponent<UnityEngine.UI.Button>().interactable = Interactable();
            
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Ngobrol";
        }

        public void startDialog(){
            controller = GetComponent<NPC_Controller>();

            if(controller){
                controller.currentCondition(animasi.Ngobrol);
            }

            

            Player.GetComponentInChildren<Animator>().SetBool("Ngomong", true);

            FindObjectOfType<DialogManager>().StartDialog(dialog, null); 
        }

        

        public void endDialog(){
            UiManager.instance.Chinematic(false);

            Player.GetComponentInChildren<Animator>().SetBool("Ngomong", false);
            Player.GetComponent<Controller>().currentState(state.Default);
            UiManager.instance.panelUtama.SetActive(true);
            FindObjectOfType<QuestManager>().CheckAction(tempAction);

            FindObjectOfType<DialogManager>().closeDialog();
        }
    }
}