using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terbaru{
    public class Narasi : MonoBehaviour, IDialog
    {
        public NarasiDialog dialog;

        public Camera cameraUtama;
        public GameObject Sekre;

        public Controller player;

        public List<Kasus> listKasus = new List<Kasus>();
        public int indexKasus = 0;
        

        Dialog tempDialog;

        NarasiDialog getNarasi(Dialog temp){
            foreach(var narasi in listKasus[indexKasus].dataNarasi){
                if(narasi.narasi == temp){
                    return narasi;
                }
            }

            return null;
        }

        

        public void startDialog(){
            tempDialog = listKasus[indexKasus].dataNarasi[0].narasi;
            FindObjectOfType<Player_Interaction>().interactObject = this.gameObject; 
            FindObjectOfType<Controller>().currentState(state.Interaction); 
                      
            UiManager.instance.Chinematic(true);
            
            UiManager.instance.panelUtama.SetActive(false);
            
           StartCoroutine(mulai());
           
        }

        public void startNarasi(Dialog temp){
            
            dialog = getNarasi(temp);
            //tempDialog = dialog;

            FindObjectOfType<Player_Interaction>().interactObject = this.gameObject; 
            FindObjectOfType<Controller>().currentState(state.Interaction); 
                      
            UiManager.instance.Chinematic(true);
            
            UiManager.instance.panelUtama.SetActive(false);
            
            //FindObjectOfType<DialogManager>().StartDialog(tempDialog);
            FindObjectOfType<DialogManager>().StartDialog(dialog.narasi);
        }

        public void startNarasi(int temp){
            
            tempDialog = listKasus[indexKasus].dataNarasi[temp].narasi;
            //tempDialog = dialog;

            FindObjectOfType<Player_Interaction>().interactObject = this.gameObject; 
            //FindObjectOfType<Controller>().currentState(state.Interaction); 
                      
            //UiManager.instance.Chinematic(true);
            
            //UiManager.instance.panelUtama.SetActive(false);
            
            //FindObjectOfType<DialogManager>().StartDialog(tempDialog);
            FindObjectOfType<DialogManager>().StartDialog(tempDialog);
        }

        IEnumerator mulai(){
            UiManager.instance.Chinematic(true);

            yield return new WaitForSeconds(3f);

            UiManager.instance.Chinematic(true);

            yield return new WaitForSeconds(2f);
            cameraUtama.gameObject.SetActive(false);
            Sekre.SetActive(true);
            UiManager.instance.Chinematic(true);

            yield return new WaitForSeconds(1f);
            FindObjectOfType<DialogManager>().StartDialog(tempDialog);
            
        }

        IEnumerator kembali(){
            FindObjectOfType<DialogManager>().closeDialog();
            yield return new WaitForSeconds(1f);
            UiManager.instance.Chinematic(false);

            yield return new WaitForSeconds(2f);
            Sekre.SetActive(false);
            cameraUtama.gameObject.SetActive(true);
            player.setPlayerOnFrontDoor();

            yield return new WaitForSeconds(1f);
            closeDialog();
            // UiManager.instance.Chinematic(false);

            // yield return new WaitForSeconds(1f);
            
            // // UiManager.instance.Chinematic(true);

            // yield return new WaitForSeconds(1f);
            //  FindObjectOfType<DialogManager>().StartDialog(tempDialog);
            
        }

        

        
        

        public void nextDialog(Dialog dialog){
            tempDialog = dialog;
            FindObjectOfType<DialogManager>().StartDialog(dialog);
        }

        public void endDialog(){
            
            if(tempDialog.quest.proses.Length > 0){
                listQuest quest = tempDialog.quest;
                QuestManager.instance.StartQuest(quest);
                Debug.Log("End Dialog Have Quest");
                return;
            } else if(tempDialog.events.GetPersistentEventCount() > 0){
                Debug.Log("Invoke");
                tempDialog.events?.Invoke();
            }
            
            else
            
            {
                StartCoroutine(kembali());
            }
            
        }

        public void closeDialog(){
            //FindObjectOfType<DialogManager>().closeDialog();
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
    public class Kasus{
        public int indexKasus;
        public List<NarasiDialog> dataNarasi;
    }
    
}
