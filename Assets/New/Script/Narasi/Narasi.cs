using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terbaru{
    public class Narasi : MonoBehaviour, IDialog
    {
        public NarasiDialog dialog;
        

        Dialog tempDialog;

        public void startDialog(){
            tempDialog = dialog.dialogs;
            FindObjectOfType<Player_Interaction>().interactObject = this.gameObject; 
            FindObjectOfType<Controller>().currentState(state.Interaction); 
                      
            UiManager.instance.Chinematic(true);
            
            UiManager.instance.panelUtama.SetActive(false);

            FindObjectOfType<DialogManager>().StartDialog(tempDialog);
           
        }

        
        public void startDialog(Dialog dialog){
            tempDialog = dialog;

            FindObjectOfType<Player_Interaction>().interactObject = this.gameObject; 
            FindObjectOfType<Controller>().currentState(state.Interaction); 
                      
            UiManager.instance.Chinematic(true);
            
            UiManager.instance.panelUtama.SetActive(false);

            FindObjectOfType<DialogManager>().StartDialog(tempDialog);
        }

        public void nextDialog(Dialog dialog){
            tempDialog = dialog;
            FindObjectOfType<DialogManager>().StartDialog(tempDialog);
        }

        public void endDialog(){
            
            if(tempDialog.quest.proses.Length > 0){
                listQuest quest = tempDialog.quest;
                QuestManager.instance.StartQuest(quest);
                Debug.Log("End Dialog Have Quest");
                return;
            }else{
                closeDialog();
            }
            
        }

        void closeDialog(){
            //Debug.Log("Close Dialog");
            UiManager.instance.Chinematic(false);
            
            UiManager.instance.panelUtama.SetActive(true);
            //;
            
            FindObjectOfType<Player_Interaction>().interactObject = null;
            
            FindObjectOfType<QuestManager>().CheckAction("Narasi");
            Invoke("startAction",1f);

            FindObjectOfType<DialogManager>().closeDialog();
            //gameObject.SetActive(false);
            //Debug.Log("Close Dialog 2 ");
        }

        void startAction(){
            FindObjectOfType<Controller>().currentState(state.Default);
            //OnAction.Invoke();
            
        }
    }


    [System.Serializable]
    public class NarasiDialog{
        public Dialog dialogs;

    }

    [System.Serializable]
    public class NarasiData{
        
        [TextArea(5,10)]
        public string narasiText;
        public AudioClip VO;
    }
}
