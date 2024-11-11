using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Terbaru{
    public class NPC_Dialog : MonoBehaviour, IDialog, Interaction
    {

        public Nama nama;

        public bool value;
        public UnityEvent OnAction = new UnityEvent();
        public Dialog dialog;

        public bool onDialog;

        public float distance;
        public GameObject animasiObj;
        public bool isObject;

        void OnDisable(){
            value = false;
        }
        
        void OnEnable(){
            value = true;
        }


        public void Awake(){
            if(isObject){
                point = transform.GetChild(1).gameObject;
                Player = GameObject.FindWithTag("Player").transform;
                distancePlayer = 10f;
            }
        }

        int currentDialog = 0;

        public void SetindexDialog(int value){
            currentDialog = value;
        }

        public int indexDialog(){
            return currentDialog;
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
            if(isObject && Interactable()){
                checkDistance();
            }else if(!Interactable()){
                point.gameObject.SetActive(false);
            }
                
        }

        public void isTutorial(bool temp){
            //enabled = temp;
        }

        bool interactable = true;
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
            changeInteractable(false);
            dialog = FindObjectOfType<NPCDialogManager>().tempDialog(indexDialog(), nama);

            FindObjectOfType<Player_Interaction>().interactObject = this.gameObject; 
            FindObjectOfType<Controller>().currentState(state.Interaction); 
                      
            UiManager.instance.Chinematic(true);
            
            UiManager.instance.panelUtama.SetActive(false);

            GetComponent<NPC_Controller>().currentCondition(animasi.Ngobrol);

            FindObjectOfType<DialogManager>().StartDialog(dialog);
           
        }

        public void endDialog(){
            onDialog = false;
            UiManager.instance.Chinematic(false);
            Debug.Log("End");
            GetComponent<NPC_Controller>().currentCondition(animasi.Idle);
            
            UiManager.instance.panelUtama.SetActive(true);
            //;
            
            FindObjectOfType<Player_Interaction>().interactObject = null;
            
            //FindObjectOfType<QuestManager>().CheckAction(tempAction);
            Invoke("startAction",1f);
            FindObjectOfType<DialogManager>().closeDialog();
            //gameObject.SetActive(false);
        }

        void startAction(){
            changeInteractable(true);
            FindObjectOfType<Controller>().currentState(state.Default);

            if(OnAction.GetPersistentEventCount() > 0)
                OnAction.Invoke();
            else if(dialog.quest.proses.Length != 0){
                QuestManager.instance.StartQuest(dialog.quest);
            }
            
        }
    }
}