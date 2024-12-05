using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Terbaru
{

    public class Pintu : MonoBehaviour, Interaction , IDialog
    {
         public Dialog dialog;
        public UnityEvent extendAction;
        public UnityEvent ExtraAction;
        public Vector3 PosUI;
        public Transform spawn;
        public GameObject Trigger;

        public Transform engselPintu;

        

         
        Vector3 Rotate;


        public float rotateBuka;

        public bool value;

        public bool keluar;

        public void isTutorial(bool temp){
           // enabled = temp;
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
            if(Interactable())
                checkDistance();
        }

        public bool interactable;
        public bool Interactable(){
            return interactable;
        }

        public void changeInteractable(bool value){
            //Debug.Log($"{gameObject.name} : {value}");
            interactable = value;
            // return interactable;
        }
        public void action(Transform Player)
        {

            if (FindObjectOfType<Controller>().profil.Energy >= 1 || !keluar) {
                Player.transform.position = new Vector3(spawn.position.x, Player.position.y, spawn.position.z);

                if (Trigger != null)
                {
                    Trigger.GetComponent<Collider>().enabled = false;
                }

                Rotate = new Vector3(0f, rotateBuka, 0f);
                SoundManager.instance.sfx(29);
                engselPintu.DOLocalRotate(Rotate, 1.5f).OnComplete(() => {
                    Debug.Log("Buka");
                    extendAction?.Invoke();
                    ExtraAction?.Invoke();
                    });
            }
            else
            
            {
                //Player.GetComponent<Controller>().currentState(state.Interaction);
                UiManager.instance.startChinematic();

                //UiManager.instance.panelUtama.SetActive(false);

                Invoke("startDialog", 1f);
            }
            
            
        }

        public void tutupPintu(){
            
            engselPintu.DOLocalRotate(Vector3.zero, 1.5f)
            .OnComplete( () => {
                //FindObjectOfType<Controller>().currentState(state.Default);
                SoundManager.instance.sfx(30);
                })
                ;
            point.gameObject.SetActive(false);
            Trigger.GetComponent<Collider>().enabled = true;
        }

        public void btnActive(GameObject btn, bool interactable){
            btn.SetActive(interactable);
            btn.GetComponent<UnityEngine.UI.Button>().interactable = Interactable();
            
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Buka Pintu";
        }

        public void tutup(){
            var video = FindObjectOfType<VideoManager>();
            float duration = (float)video.video.length;
            Invoke("tutupPintu", duration + 8.5f);
        }

        public void endDialog()
        {
            UiManager.instance.ChinematicPanel.endChinematic();

            Player.GetComponentInChildren<Animator>().SetBool("Ngomong", false);
           // Player.GetComponent<Controller>().currentState(state.Default);
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