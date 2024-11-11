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
        void OnMouseDown(){
            Debug.Log("Klik");
            if(!onDialog || FindObjectOfType<MapsManager>().onAnimation)
                return;
            startDialog();
        }
        public void startDialog(){
            onDialog = false;
            UiManager.instance.chinematicDialog(true);
            FindObjectOfType<Player_Interaction>().interactObject = this.gameObject;
            FindObjectOfType<DialogManager>().StartDialog(dialog);
        
        }

        public void canDialog(bool value){
            Indikator.SetActive(value);
            onDialog = value;
        }

        public void interactNPC(bool value){
            onDialog = value;
            Indikator.SetActive(value);
        }

        public void endDialog(){
            UiManager.instance.chinematicDialog(false);
            interactNPC(false);
            extendAction?.Invoke();
            //canDialog(false);
            //Mulai Quiz
            //Debug.Log("Selesai Dialog, Saat nya Quiz");
            FindObjectOfType<DialogManager>().closeDialog();
        }
    }
}