using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru{

    public class DialogManager : MonoBehaviour
    {
        private Queue<dataDialog> sentences;

        public TMPro.TMP_Text dialogText;

        public UI_Dialog uiDialog;
        bool done = true;

        public UnityEngine.UI.Button btnNextDialog;
        
    // public Dialog cakap;

        [SerializeField] int jumlahDialog;
        public Animator animator;

        void Start()
        {
            sentences = new Queue<dataDialog>();
        }

        public void StartDialog(Dialog dialog)
        {
            //animator.SetBool("pindah", true);
            uiDialog.gameObject.SetActive(true);
            uiDialog.startDialog();
            //GameObject.Find("Trigger").SetActive(false);

        // GameObject.Find("Level1").SetActive(false);

            jumlahDialog = dialog.data.Length;
            sentences.Clear();

            foreach(var tempData in dialog.data)
            {
                sentences.Enqueue(tempData);
            }

            DisplayNextDialog();
        }


        public void DisplayNextDialog()
        {
            if(!done)
                return;
            done = false;
            if(sentences.Count == 0)
            {
                EndDialog();
                return;
            }
            int index = jumlahDialog - sentences.Count;
            

            var sentence = sentences.Dequeue();

            uiDialog.displayBallonDialog(index, sentence);
            dialogText = uiDialog.tempText;
            StopAllCoroutines();
            btnNextDialog.interactable = false;
            StartCoroutine(TypeSentence(sentence.sentences));
        }

        IEnumerator TypeSentence(string sentence)
        {
            dialogText.text = "";
            done = false;
            foreach(char letter in sentence.ToCharArray())
            {
                dialogText.text += letter;
                yield return null;
            }

            yield return new WaitForSeconds(.5f);
            done = true;
            btnNextDialog.interactable = true;
        }

        public void EndDialog()
        {
            //UiManager.instance.Chinematic(false);
            var objectInteract = FindObjectOfType<Controller>().interaction;
            objectInteract.interactObject.GetComponent<IDialog>().endDialog();
            uiDialog.gameObject.SetActive(false);
            done = true;
            btnNextDialog.interactable = true;
        }
    }
}
