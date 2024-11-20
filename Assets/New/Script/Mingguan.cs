using System.Collections;
using System.Collections.Generic;
using food;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;


namespace Terbaru {

    public class Mingguan : MonoBehaviour, IDialog
    {
        public List<UnityEvent> eventsReady = new List<UnityEvent>();

        public List<UnityEvent> eventOver = new List<UnityEvent>();
        public Dialog temp;
        public UnityEvent eventTemp;
        public UnityEvent eventCloseDialog;

        [Header("Minggu ke 2")]
        public VideoClip minggu2;
        public Dialog dialog1;
        public UnityEvent eventMinggu2;

        [Header("Minggu ke 3")]
        public UnityEngine.UI.Button uang;
        public GameObject paneLquiz;
        public VideoClip minggu3;
        public Transform Player;
        public Dialog dialog2;
        public UnityEvent eventMinggu3;

        

        public void mulaiEvent(){
            int index = FindObjectOfType<DayManager>().day;

            if(index == 7){
                eventsReady[0]?.Invoke();
            }

            else if(index == 14){
                // bool haveIt = GameManager.instance.haveItem(itemType.Bibit) &&
                //             GameManager.instance.haveItem(itemType.Water_can) &&
                //             GameManager.instance.haveItem(itemType.Pupuk);

                // if(haveIt){
                //     eventsReady[1]?.Invoke();
                //     //FindObjectOfType<VideoManager>().action(minggu2);
                //     playDialogMinggu2();
                // }
            }

            else if(index == 21){
                // eventsReady[2]?.Invoke();
                // //FindObjectOfType<VideoManager>().action(minggu3);
                // playDialogMinggu3();
                
            }
        }
        public void startEvent(){
            Invoke("mulaiEvent", 7f);
        }

        public void QuizKucing(){
           Invoke("mulaiQuiz", 6f); 
        }

        public void mulaiQuiz(){
            paneLquiz.SetActive(true);
            bool haveIt = GameManager.instance.profil.Saldo >= 10000;
            uang.interactable = haveIt;
            uang.onClick.AddListener(() => {
                UiManager.instance.UpdateSaldo(-10000);
            });
        }


        void playDialogMinggu2(){
            Debug.Log("Minggu 2");
            temp = dialog1;
            startDialog();
            eventTemp.RemoveAllListeners();
            eventTemp = eventMinggu2;
        }

        void playDialogMinggu3(){
            temp = dialog2;
            Debug.Log("Minggu 3");
            startDialog();
            eventTemp.RemoveAllListeners();
            eventTemp = eventMinggu3;
        }

        public void stopEvent(){
            int index = (int)FindObjectOfType<DayManager>().day;

            if(index == 7){
                eventOver[0]?.Invoke();
            }

            else if(index == 14){
                eventOver[0]?.Invoke();
            }

            else if(index == 21){
                eventOver[0]?.Invoke();
            }
        }

         public void startDialog(){
            // controller = GetComponent<NPC_Controller>();

            // if(controller){
            //     controller.currentCondition(animasi.Ngobrol);
            // }
            UiManager.instance.panelUtama.SetActive(false);
            Player.GetComponent<Controller>().currentState(state.Interaction);
            UiManager.instance.Chinematic(true);
            //Invoke("chinematic", 1f);
            FindObjectOfType<Player_Interaction>().interactObject = gameObject; 

            Player.GetComponentInChildren<Animator>().SetBool("Ngomong", true);

            FindObjectOfType<DialogManager>().StartDialog(temp); 
        }

        public void chinematic(){
            UiManager.instance.Chinematic(true);
            //  UiManager.instance.Chinematic(true);
        }

        

        public void endDialog(){
            //UiManager.instance.Chinematic(false);

            Player.GetComponentInChildren<Animator>().SetBool("Ngomong", false);
            Player.GetComponent<Controller>().currentState(state.Default);
            UiManager.instance.panelUtama.SetActive(true);
            //FindObjectOfType<QuestManager>().CheckAction(tempAction);

            FindObjectOfType<DialogManager>().closeDialog();
            eventTemp?.Invoke();
        }
    }
}
