using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Terbaru{
    public class DialogObjectClick : MonoBehaviour, IDialog
    {
        public Dialog dialog;
        public UnityEvent extendAction;
        public bool onDialog = true;
        public GameObject Indikator;
        public bool warga;

        void OnMouseDown(){
            Debug.Log("Klik");
            if (!onDialog) {
                if (warga)
                    return;

                else if (FindObjectOfType<MapsManager>().onAnimation)
                {
                    return;    
                }

                return;
                

            }
            startDialog();
        }
        public void startDialog(){
            onDialog = false;
            if (!warga) UiManager.instance.startChinematicWithoutCam();

            else if(warga) Manager_Ending.instance.Chinematic(true);


            if (!warga) FindObjectOfType<Player_Interaction>().interactObject = this.gameObject;
            else if (warga) FindObjectOfType<Manager_Ending>().onDialogWarga(this);
            GameObject temp = warga ? gameObject : null;
            FindObjectOfType<DialogManager>().StartDialog(dialog, temp);
        
        }

        public void canDialog(bool value){
            if(Indikator)
                Indikator.SetActive(value);
            onDialog = value;
        }

        public void interactNPC(bool value){
            onDialog = value;
            if(Indikator)
                Indikator.SetActive(value);
        }

        public void endDialog(){

            if (dialog.quest.proses.Length > 0)
            {
                QuestManager.instance.StartQuest(dialog.quest);
            }
            else if(extendAction.GetPersistentEventCount() <= 0)
            {
                if (!warga) UiManager.instance.endChinematicWithoutCam();

                else if (warga) Manager_Ending.instance.Chinematic(false);

                FindObjectOfType<DialogManager>().closeDialog();

            }


            interactNPC(false);
            extendAction?.Invoke();
            //canDialog(false);
            //Mulai Quiz
            //Debug.Log("Selesai Dialog, Saat nya Quiz");
            
        }
    }
}