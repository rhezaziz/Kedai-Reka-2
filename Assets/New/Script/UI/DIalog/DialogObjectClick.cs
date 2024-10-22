using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Terbaru{
    public class DialogObjectClick : MonoBehaviour, IDialog
    {
        public Dialog dialog;
        public UnityEvent extendAction;
        public bool isQuiz = true;
        void OnMouseDown(){
            Debug.Log("Klik");
            if(!isQuiz)
                return;
            startDialog();
        }
        public void startDialog(){
            isQuiz = false;
            FindObjectOfType<Player_Interaction>().interactObject = this.gameObject;
            FindObjectOfType<DialogManager>().StartDialog(dialog);

        }

        public void endDialog(){
            extendAction?.Invoke();
            //Mulai Quiz
            //Debug.Log("Selesai Dialog, Saat nya Quiz");
            FindObjectOfType<DialogManager>().closeDialog();
        }
    }
}