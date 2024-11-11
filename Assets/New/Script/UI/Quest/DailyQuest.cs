using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Terbaru{
    public class DailyQuest : MonoBehaviour, Interaction, IDialog
    {

        public listQuest quest;

        public bool isObject;

        public MiniGame miniGame;

        public string sceneName;

        public UnityEvent unityAction;



        bool interactable;
        public bool Interactable(){
            return interactable;
        }

        public void changeInteractable(bool value){
            interactable = value;
            // return interactable;
        }

        
        // Start is called before the first frame update
        public Transform Player;
        public GameObject point;
        public float distancePlayer;

        public Dialog awal;
        public Dialog Akhir;
        public void checkDistance(){
            float distance = Vector3.Distance(transform.position, Player.position);

            if(distancePlayer >= distance){
                point.gameObject.SetActive(true);
            }else{
                point.gameObject.SetActive(false);
            } 
        }

        public void mulaiDialogAwal(){

        }

        public void mulaiDialogAkhir(){

        }


        void Update(){
            if(isObject)
                checkDistance();
        }

        public Dialog[] dialog;

        public void action(Transform player){
            FindObjectOfType<QuestManager>().StartQuest(quest);
            //miniGame.pindahMiniGame(sceneName);

            //StartCoroutine(TimerCoroutine(4f));
        }  


        private IEnumerator TimerCoroutine(float duration)
        {
            // Tunggu sesuai durasi timer
            yield return new WaitForSeconds(duration);

            // Jika ada action yang sudah diset, panggil action tersebut
            unityAction?.Invoke();
        } 

        public void btnActive(GameObject btn, bool interactable)
        {
            btn.SetActive(interactable);
            btn.GetComponent<UnityEngine.UI.Button>().interactable = Interactable();
            
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Sapu";
            var button = btn.GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(() =>{
                unableButton(button);
            });
        }

        void unableButton(UnityEngine.UI.Button button){
            button.gameObject.SetActive(false);
            button.onClick.RemoveListener(() => unableButton(button));
        }

        public void isTutorial(bool value)
        {
            //throw new System.NotImplementedException();
        }

        public bool pindah;

        public void endDialog()
        {
            UiManager.instance.Chinematic(false);

            FindObjectOfType<Controller>().GetComponentInChildren<Animator>().SetBool("Ngomong", false);
            
            UiManager.instance.panelUtama.SetActive(true);
            //;
            
            FindObjectOfType<Player_Interaction>().interactObject = null;
            
            //FindObjectOfType<QuestManager>().CheckAction(tempAction);
            //Invoke("startAction",1f);
            FindObjectOfType<DialogManager>().closeDialog();

            QuestManager.instance.CheckActionQuest("Talk");

            //FindObjectOfType<UiManager>().panelUtama.SetActive(true);
            FindObjectOfType<Controller>().currentState(state.Default);
            //QuestManager.instance.CheckActionQuest("Talk");

            
        }

        public void startDialog()
        {
            FindObjectOfType<Player_Interaction>().interactObject = this.gameObject; 
            FindObjectOfType<Controller>().currentState(state.Interaction); 
                      
            UiManager.instance.Chinematic(true);
            
            UiManager.instance.panelUtama.SetActive(false);

            FindObjectOfType<Controller>().GetComponentInChildren<Animator>().SetBool("Ngomong", true);

            
        }

        public void StartDialogIndex(int index){
            
            FindObjectOfType<UiManager>().panelUtama.SetActive(true);
            FindObjectOfType<Controller>().currentState(state.Default);
            startDialog();
            FindObjectOfType<DialogManager>().StartDialog(dialog[index]);

        }
    }
}