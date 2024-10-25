using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru{

    public class DialogManager : MonoBehaviour
    {
        private Queue<dataDialog> sentences;

        public TMPro.TMP_Text dialogText;

        public UI_Dialog uiDialog;

        public GameObject narasiObj;
        public TMPro.TMP_Text narasiText;
        bool done = true;

        public UnityEngine.UI.Button btnNextDialog;
        
    // public Dialog cakap;

        [SerializeField] int jumlahDialog;
        public Animator animator;

        void Start()
        {
            sentences = new Queue<dataDialog>();
        }

        void DisplayVO(dataDialog data){
            narasiObj.SetActive(true);
            dialogText = narasiText;
            
            uiDialog.VOActive();
            

        }

        int CountWords(string sentence)
        {
            // Memisahkan string berdasarkan spasi atau karakter pembatas lainnya
            int count = 0;
            foreach (char c in sentence)
            {
                // Mengecek apakah karakter adalah huruf
                if (char.IsLetter(c))
                {
                    count++;
                }
            }
            return count;
        }

        public Dialog testDialog;

        public void StartDialog(Dialog dialog)
        {

            testDialog = dialog;
            //Debug.Log("Mulai Dialog");
            uiDialog.gameObject.SetActive(true);
            uiDialog.startDialog();
            jumlahDialog = dialog.dialogObject.data.Length;
            sentences.Clear();
            done = true;
            

            foreach(var tempData in dialog.dialogObject.data)
            {
                sentences.Enqueue(tempData);
            }

            DisplayNextDialog();
        }

        public void paksa(){
            StartDialog(testDialog);
        }


        public void DisplayNextDialog()
        {
            //Debug.Log("Display Dialog");
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

            if(sentence.narasi.narasiText != ""){
                DisplayVO(sentence);
                
                StopAllCoroutines();
                btnNextDialog.gameObject.SetActive(false);
                float delay = 0.1f;
                if(sentence.narasi.VO != null){
                    
                    delay = sentence.narasi.VO.length / CountWords(sentence.narasi.narasiText) ;
                    //Debug.Log($"delay : {delay} - VO : {sentence.narasi.VO.length * 60} - Word : {CountWords(sentence.narasi.narasiText)}");
                    SoundManager.instance.sfx(sentence.narasi.VO);
                }
                StartCoroutine(TypeSentenceNarasi(sentence.narasi.narasiText, delay));

            }else{

                uiDialog.displayBallonDialog(index, sentence);
                dialogText = uiDialog.tempText;

                StopAllCoroutines();
                btnNextDialog.gameObject.SetActive(false);
                StartCoroutine(TypeSentence(sentence.sentences));
                narasiObj.SetActive(false);
            
            }

            
        }

        IEnumerator TypeSentence(string sentence)
        {
            dialogText.text = "";
            done = false;
            foreach(char letter in sentence.ToCharArray())
            {
                dialogText.text += letter;
                SoundManager.instance.sfx(27);
                yield return null;
            }

            yield return new WaitForSeconds(.5f);
            done = true;
            btnNextDialog.gameObject.SetActive(true);
        }

        IEnumerator TypeSentenceNarasi(string sentence, float delay)
        {
            dialogText.text = "";
            done = false;
            foreach(char letter in sentence.ToCharArray())
            {
                dialogText.text += letter;
                yield return new WaitForSeconds(delay);
            }

            yield return new WaitForSeconds(.5f);
            done = true;
            btnNextDialog.gameObject.SetActive(true);
        }

        public void EndDialog()
        {
            //UiManager.instance.Chinematic(false);
            var objectInteract = FindObjectOfType<Controller>().interaction;
            objectInteract.interactObject.GetComponent<IDialog>().endDialog();
            Debug.Log("End Dialog Manager");
        }

        public void closeDialog(){
            uiDialog.gameObject.SetActive(false);
            done = true;
            btnNextDialog.gameObject.SetActive(true);

            //Debug.Log("Close Dialog Manager");
        }
    }
}
