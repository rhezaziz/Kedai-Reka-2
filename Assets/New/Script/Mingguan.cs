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
        public UnityEvent kosong;

        [Header("Minggu ke 2")]
        public VideoClip minggu2;
        public Dialog dialog1;
        public UnityEvent eventMinggu2;

        [Header("Minggu ke 3")]
        public UnityEngine.UI.Button uang;
        //public UnityEngine.UI.Button Character;
        public listCharacters[] characters;
        public GameObject paneLquiz;
        public VideoClip minggu3;
        public Transform Player;
        public Dialog dialog2;
        public UnityEvent eventMinggu3;

        [Header("End Game")]
        public Dialog DialogEnd;
        public Dialog dialogEnd2;
        public UnityEvent eventEnd1;
        public UnityEvent eventEnd2;




        public void mulaiEvent(){
            int index = FindObjectOfType<DayManager>().day;
            eventTemp = kosong;
            if (index == 7){
                eventsReady[0]?.Invoke();
            }

            else if(index == 14){
                bool haveIt = GameManager.instance.haveItem(itemType.Bibit) &&
                            GameManager.instance.haveItem(itemType.Water_can) &&
                            GameManager.instance.haveItem(itemType.Pupuk);

                if (haveIt)
                {
                    eventsReady[1]?.Invoke();
                    //FindObjectOfType<VideoManager>().action(minggu2);
                    playDialogMinggu2();
                }
            }

            else if(index == 21){
                eventsReady[2]?.Invoke();
                //FindObjectOfType<VideoManager>().action(minggu3);
                playDialogMinggu3();

            }

            else if (index == 28)
            {
                UiManager.instance.bantuanText("Pergi Ke Pekarangan");
                //eventsReady[2]?.Invoke();
                ////FindObjectOfType<VideoManager>().action(minggu3);
                //playDialogMinggu3();

            }

            else if (index == 30)
            {
                playDialogEnd1();
                
                // UiManager.instance.bantuanText("Pergi Ke Pekarangan");
                //eventsReady[2]?.Invoke();
                ////FindObjectOfType<VideoManager>().action(minggu3);
                //playDialogMinggu3();

            }
        }
        public void startEvent(){
            Invoke("mulaiEvent", 7f);
        }

        public void QuizKucing(){
           Invoke("mulaiQuiz", 11f); 
        }

        public void mulaiQuiz(){
            
            paneLquiz.SetActive(true);
            UiManager.instance.startChinematic();
            bool haveIt = GameManager.instance.profil.Saldo >= 10000;
            uang.interactable = haveIt;
            uang.onClick.AddListener(() => {
                int hasil = GameManager.instance.profil.Saldo - 10000;
                UiManager.instance.UpdateSaldo(hasil);
                jawabQuiz(false);
            });
        }

        public void jawabQuiz(bool isChoose)
        {

            FindObjectOfType<DialogManager>().closeDialog();
            UiManager.instance.endChinematic();
            if (isChoose)
            {
                List<GameObject> obj = new List<GameObject>();
                foreach(var i in characters)
                {
                    obj.Add(i.objectNPC);
                    i.selected = true;
                }

                GameManager.instance.QuizPilihKarakter(obj);
            }
            UiManager.instance.ChinematicPanel.endChinematic();
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

        void playDialogEnd1()
        {
            temp = DialogEnd;
            eventTemp.RemoveAllListeners();
            //Debug.Log("Minggu 3");
            startDialog();
            //eventTemp.RemoveAllListeners();
            //eventTemp = eventMinggu3;
            eventTemp.AddListener(() =>
            {
                StartCoroutine(pindahKeDepanPintu());
            });
        }

        public void endMinggu3()
        {
            foreach(var i in characters)
            {
                i.selected = false;
                GameManager.instance.npcCharacter(i.objectNPC).SetActive(!i.characterLock);
            }
        }

        IEnumerator pindahKeDepanPintu()
        {
            UiManager.instance.startChinematic();
            yield return new WaitForSeconds(2f);
            //UiManager.instance.panelUtama.SetActive(false);
           // Player.GetComponent<Controller>().currentState(state.Interaction);
            UiManager.instance.chinematicDialog(true);
            yield return new WaitForSeconds(2f);
            FindObjectOfType<Controller>().setPlayerOnFrontDoor();
            //UiManager.instance.Chinematic(false);
            yield return new WaitForSeconds(1f);
            UiManager.instance.endChinematic();
            yield return new WaitForSeconds(1f);
            UiManager.instance.ChinematicPanel.endChinematic();
            playDialogEnd2();

        }

        void playDialogEnd2()
        {
            temp = dialogEnd2;
            //Debug.Log("Minggu 3");
            startDialog();
            eventTemp.RemoveAllListeners();
            eventTemp.AddListener(() =>
            {
                StartCoroutine(pindahKeKampung());
            });
            //eventTemp = eventMinggu3;
        }

        IEnumerator pindahKeKampung()
        {
            UiManager.instance.startChinematic();
            yield return new WaitForSeconds(1f);
            UiManager.instance.panelUtama.SetActive(false);
            Player.GetComponent<Controller>().currentState(state.Interaction);
            UiManager.instance.chinematicDialog(true);
            yield return new WaitForSeconds(2f);
            FindObjectOfType<MiniGame>().pindahKeKampung("Kampung");
            SoundManager.instance.DestroyIntance();
            yield return new WaitForSeconds(1f);
            GameManager.instance.DestroyThisObject();
            // yield return new WaitForSeconds(2f);
            // //FindObjectOfType<Controller>().setPlayerOnFrontDoor();
            // FindObjectOfType<Manager_Ending>().chinematicWithaouCam(false);
            // yield return new WaitForSeconds(1f);
            //playDialogEnd2();

        }

        public void stopEvent(){
            int index = (int)FindObjectOfType<DayManager>().day;

            if(index == 7){
                eventOver[0]?.Invoke();
            }

            else if(index == 14){
                eventOver[1]?.Invoke();
            }

            else if(index == 21){
                eventOver[2]?.Invoke();
            }
            else if (index == 28)
            {
                eventOver[3]?.Invoke();
            }
        }

         public void startDialog(){
            // controller = GetComponent<NPC_Controller>();

            // if(controller){
            //     controller.currentCondition(animasi.Ngobrol);
            // }
            //UiManager.instance.panelUtama.SetActive(false);
           // Player.GetComponent<Controller>().currentState(state.Interaction);
            UiManager.instance.startChinematic();
            //Invoke("chinematic", 1f);
            FindObjectOfType<Player_Interaction>().interactObject = gameObject; 

            Player.GetComponentInChildren<Animator>().SetBool("Ngomong", true);

            FindObjectOfType<DialogManager>().StartDialog(temp, null); 
        }

        public void chinematic(){
            UiManager.instance.Chinematic(true);
            //  UiManager.instance.Chinematic(true);
        }

        

        public void endDialog(){
            UiManager.instance.endChinematic();

            Player.GetComponentInChildren<Animator>().SetBool("Ngomong", false);
            //Player.GetComponent<Controller>().currentState(state.Default);
            //UiManager.instance.panelUtama.SetActive(true);
            //FindObjectOfType<QuestManager>().CheckAction(tempAction);
            FindObjectOfType<DialogManager>().closeDialog();
            Invoke("endDialogAction", 1.5f);
        }

        public void endDialogAction()
        {
            
            eventTemp?.Invoke();
        }
    }
}
