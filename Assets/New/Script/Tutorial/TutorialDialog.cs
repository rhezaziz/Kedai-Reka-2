using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Terbaru{
    public class TutorialDialog : MonoBehaviour, IDialog, Interaction
    {

        public bool value;
        public UnityEvent OnAction = new UnityEvent();
        public Dialog dialog;

        public float distance;
        public GameObject animasiObj;
        public bool isObject;

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
            if(isObject)
                checkDistance();
        }

        public void isTutorial(bool temp){
            //enabled = temp;
        }

        bool interactable;
        public bool Interactable(){
            return interactable;
        }

        public void changeInteractable(bool value){
            interactable = value;
            // return interactable;
        }

        public void action(Transform player){
            int temp = transform.position.x - player.position.x > 0 ? -1 : 1;
            float xDir = player.position.x + (distance * temp);
            float zDir = transform.position.z;
            player.transform.position = new Vector3(xDir, player.position.y, zDir);
            player.GetComponentInChildren<SpriteRenderer>().flipX = temp > 0 ? false : true;
            GetComponentInChildren<SpriteRenderer>().flipX = temp > 0 ? false : true;
            // mimikPlayer = new List<Mimik>();
            // mimikPlayer = player.GetComponent<Terbaru.Controller>().mimik;
            // InteraksiPlayer.Interaksi = true;
            //UiM.Instance.parentDialog.SetActive(true);
            // isDialog = true;
            // dialogText = UIManager.Instance.text_Dialog;

            animasiObj.GetComponent<NPC_Controller>().currentCondition(animasi.Ngobrol);
            //animator = player.GetComponentInChildren<Animator>();
            startDialog();
        }

        public void btnActive(GameObject btn, bool interactable){
            btn.SetActive(interactable);
            btn.GetComponent<UnityEngine.UI.Button>().interactable = Interactable();
            
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Ajak Obrol";
        }

        public void startDialog(){
             
            FindObjectOfType<Player_Interaction>().interactObject = this.gameObject; 
            FindObjectOfType<Controller>().currentState(state.Interaction); 
                      
            UiManager.instance.Chinematic(true);
            
            UiManager.instance.panelUtama.SetActive(false);

            FindObjectOfType<Controller>().GetComponentInChildren<Animator>().SetBool("Ngomong", true);

            FindObjectOfType<DialogManager>().StartDialog(dialog, null);
           
        }

        public void starDialogWithData(Dialog_Object obj){
            FindObjectOfType<Player_Interaction>().interactObject = this.gameObject; 
            FindObjectOfType<Controller>().currentState(state.Interaction); 
                      
            UiManager.instance.Chinematic(true);
            
            UiManager.instance.panelUtama.SetActive(false);

            FindObjectOfType<Controller>().GetComponentInChildren<Animator>().SetBool("Ngomong", true);

            
        }

        public void endDialog(){
            UiManager.instance.Chinematic(false);

            FindObjectOfType<Controller>().GetComponentInChildren<Animator>().SetBool("Ngomong", false);
            
            UiManager.instance.panelUtama.SetActive(true);
            //;
            
            FindObjectOfType<Player_Interaction>().interactObject = null;
            
            //FindObjectOfType<QuestManager>().CheckAction(tempAction);
            Invoke("startAction",1f);
            FindObjectOfType<DialogManager>().closeDialog();

            QuestManager.instance.CheckActionQuest("Talk");
            //gameObject.SetActive(false);
        }

        void startAction(){
            FindObjectOfType<Controller>().currentState(state.Default);
            OnAction.Invoke();
            
        }
    }
}