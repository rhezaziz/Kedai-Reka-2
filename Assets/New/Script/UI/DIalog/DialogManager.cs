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
        public GameObject panelDialog;
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
            panelDialog.gameObject.SetActive(false);
            

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

        public void StartDialog(Dialog dialog, GameObject interactDialog)
        {
            //uiDialog.ResetDialog();
            if (interactDialog) dialogOnKampung = interactDialog;

            panelDialog.SetActive(true);
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
            StartDialog(testDialog, null);
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
            SoundManager.instance.stopSFX();    

            if(sentence.narasi.narasiText != ""){
                DisplayVO(sentence);
                
                StopAllCoroutines();
                btnNextDialog.gameObject.SetActive(false);
                float delay = 0.1f;
                float durationVO = .1f;
                if(sentence.narasi.VO != null){
                    
                    durationVO = sentence.narasi.VO.length ;
                    //Debug.Log($"delay : {delay} - VO : {sentence.narasi.VO.length * 60} - Word : {CountWords(sentence.narasi.narasiText)}");
                    SoundManager.instance.sfx(sentence.narasi.VO);
                }else {
                    durationVO = 1f;
                }
                delay = 2f / CountWords(sentence.narasi.narasiText) ;
                StartCoroutine(TypeSentenceNarasi(sentence.narasi.narasiText, delay, durationVO));

            }else{
                panelDialog.SetActive(true);

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
            SoundManager.instance.sfx(27);
            foreach(char letter in sentence.ToCharArray())
            {
                dialogText.text += letter;
                
                yield return null;
            }

            yield return new WaitForSeconds(.5f);
            done = true;
            btnNextDialog.gameObject.SetActive(true);
        }

        IEnumerator TypeSentenceNarasi(string sentence, float delay, float durationVO)
        {
            dialogText.text = "";
            done = false;
            foreach(char letter in sentence.ToCharArray())
            {
                dialogText.text += letter;
                yield return new WaitForSeconds(delay);
            }
            float tempDelay = Mathf.Clamp(durationVO - 1f, 0, 100);
            yield return new WaitForSeconds(tempDelay);
            done = true;
            btnNextDialog.gameObject.SetActive(true);
        }

        public bool inKampung;
        public GameObject dialogOnKampung;
        public void EndDialog()
        {
            if (!inKampung)
            {
                var objectInteract = FindObjectOfType<Controller>().interaction;
                objectInteract.interactObject.GetComponent<IDialog>().endDialog();
            }
            else
            {
                dialogOnKampung.GetComponent<IDialog>().endDialog();
                dialogOnKampung = null;
            }
            //UiManager.instance.Chinematic(false);
            //Debug.Log("End Dialog Manager");
        }
        public void playEvent()
        {
            btnNextDialog.gameObject.SetActive(false);
            narasiObj.SetActive(false);
            panelDialog.gameObject.SetActive(false);
        }
        public void closeDialog(){
            uiDialog.ResetDialog();
            uiDialog.gameObject.SetActive(false);
            done = true;
            btnNextDialog.gameObject.SetActive(true);
            UiManager.instance.ChinematicPanel.endChinematic();
            Debug.Log("Close Dialog Manager");
        }
    }
}
