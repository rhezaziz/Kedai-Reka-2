using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Exoa.TutorialEngine;
using DG.Tweening;
using food;


namespace Terbaru{
    public class TutorialManager : MonoBehaviour
    {
        #region Component Tutorial Day 1
        [Header("Panel Utama")]
        public Button Interaksi;
        public GameObject MoveController;
        public GameObject panelValuePlayer;
        public Button IntearctButton;

        [Header("Interact Object")]
        public GameObject kucing;
        public Interaksi_Animasi iKucing;
        public GameObject TriggerAnimasiTrigger;
        public PapanQuest panelQuest;
        public Pintu PintuKamar;
        public Pintu KamarMandi;
        public Pintu PintuUtama;
        public DialogObjectClick dosen;

        public Interaksi_CG dapur;

        [Header("Panel Quest")]
        public GameObject dinda;
        public listCharacters dindaData;
        public Button QuestList;
        public PapanQuest iQuest;
        public Button mulaiQuest;
        public GameObject skills;
        public GameObject itemQuest;
        public GameObject karakterList;
        public Button pilihKarakter;

        [Header("Collider")]
        public GameObject colDapur;
        public GameObject colDeketKamar;
        public GameObject colDeketKamarPintu;

        [Header("Collider Interaksi")]
        public GameObject objDay2_2;
        public GameObject objDay2_4;
        public GameObject objDay2_8;
        public GameObject objPaket;
        public GameObject objDay2_12;

        [Header("Dialog")]
        public TutorialDialog dialogDay2_10;



        #endregion
        
        #region  Tutorial Hari Pertama Fase 1

        //Pengenalan UI Panel Nama, Energy, dan Controller;
        public void playDay1_1()
        {
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day1_1");
            kucing.SetActive(true);
            MoveController.SetActive(true);
            //TutorialController.instance.hiddenBtn.onClick.AddListener()
            panelValuePlayer.SetActive(true);
            
            FindObjectOfType<Movement>().move = true;
        }
        
        //Memberitahu ada interaksi
        public void playDay1_2(){
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day1_2");
            iKucing.enabled = true;
            
            FindObjectOfType<Controller>().currentState(state.Interaction);
            UiManager.instance.panelUtama.SetActive(false);
            
            TutorialEvents.OnTutorialComplete += endDay1_2;
            
        }

        void endDay1_2(){
            TutorialEvents.OnTutorialComplete -= endDay1_2;
            UiManager.instance.panelUtama.SetActive(true);
            FindObjectOfType<Controller>().currentState(state.Default);
            iKucing.enabled = true;
        
        }

        // Interaksi dengan kucing
        public void playDay1_3(){
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day1_3");
            
            FindObjectOfType<Movement>().move = false;
            //TutorialEvents.OnTutorialComplete += endDay1_3;
            // TutorialController.instance.hiddenBtn.onClick.AddListener(()=>
            //     iKucing.action(player));
        }

        public void EndAnimation(){
            //TutorialEvents.OnTutorialComplete -= endDay1_3;
            //FindObjectOfType<Movement>().move = true;

            Invoke("playDay1_4", 1f);
        }

        void playDay1_4(){
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day1_4");
            FindObjectOfType<Movement>().move = false;
            UiManager.instance.panelUtama.SetActive(false);

            TutorialEvents.OnTutorialComplete += endDay1_4;
            

        }

        void endDay1_4(){
            TutorialEvents.OnTutorialComplete -= endDay1_4;
            
            initPlayDay1_5();
        }

        //Informasi menuju papan misi
        public void initPlayDay1_5(){
            var camera = Camera.main;
            Vector3 papanLocation = panelQuest.transform.position;
            Vector3 PosAwal = camera.transform.localPosition;    
            camera.transform.DOMoveX(papanLocation.x, 2f).OnComplete(()
                => camera.transform.DOMoveZ(papanLocation.z, 2f)).OnComplete(()
                    => Invoke("playDay1_5",1f));
        }

        public void playDay1_5(){
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day1_5");
            TutorialEvents.OnTutorialComplete += endDay1_5;
            
        }

        void endDay1_5(){
            TutorialEvents.OnTutorialComplete -= endDay1_5;
            var camera = Camera.main;
            camera.transform.localPosition = new Vector3(0, 1.75f, -10f);
            UiManager.instance.panelUtama.SetActive(true);
            FindObjectOfType<Controller>().currentState(state.Default);
            GameObject dindaSpawn = GameManager.instance.npcCharacter(dindaData.objectNPC);
            dindaSpawn.SetActive(true);
            
            dindaSpawn.transform.SetParent(dinda.transform);
            dindaSpawn.transform.localPosition = Vector3.zero;
            colDapur.SetActive(false);
            //dindaData.characterLock = false;
            var action = dinda.GetComponent<TutorialDialog>();
            var NpcControl = dindaSpawn.GetComponent<NPC_Controller>();
            action.animasiObj = NpcControl.gameObject;
            action.OnAction.AddListener(() =>NpcControl.currentCondition(animasi.Idle));
            //Vector3 posDinda = dindaData.Posisi;
            action.OnAction.AddListener(() => actionEnd(dindaSpawn));

            //dinda.SetActive(true);
        }

        void actionEnd(GameObject dinda){
            Transform parent = GameObject.Find("NPC").transform;
            dinda.transform.SetParent(parent); 
            dinda.transform.localPosition = dindaData.Posisi;
            FindObjectOfType<Controller>().currentState(state.Interaction);
            panelQuest.action(FindObjectOfType<Controller>().transform);
            Day1_6();
            dindaData.characterLock = false;
        }

        public void initDay1_6(){
            UiManager.instance.panelUtama.SetActive(false);
            FindObjectOfType<Controller>().currentState(state.Interaction);

        }

        void Day1_6(){
            
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day1_6");
            colDeketKamar.SetActive(true);
        }

        public void Day1_7(){
            UiManager.instance.panelUtama.SetActive(false);
            FindObjectOfType<Controller>().currentState(state.Interaction);
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day1_7");
            TutorialEvents.OnTutorialComplete += endDay1_7;
        }

        void endDay1_7(){
            TutorialEvents.OnTutorialComplete -= endDay1_7;
            UiManager.instance.panelUtama.SetActive(true);
            FindObjectOfType<Controller>().currentState(state.Default);
            PintuKamar.extendAction.AddListener(initDay2_1);
        }

        #endregion

        #region Day 2
        
        void initDay2_1(){
            Debug.Log("tes 1 1");
            Invoke("Day2_1", 8f);
            colDeketKamarPintu.SetActive(true);
            //Day2_1();
            
        }

        void Day2_1(){
            Debug.Log("tes");
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_1");
            objDay2_2.SetActive(true);
            PintuKamar.extendAction.RemoveListener(initDay2_1);
            PintuKamar.enabled = false;
        }

        public void Day2_2(){
            objDay2_2.SetActive(false);
            KamarMandi.enabled = true;
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_2");
            KamarMandi.extendAction.AddListener(initDay2_3);
        }

        void initDay2_3(){
            KamarMandi.extendAction.RemoveListener(initDay2_3);
            KamarMandi.enabled = false;
            Invoke("Day2_3", 5f);
        }

        void Day2_3(){
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_3");
            objDay2_4.SetActive(true);
        }

        public void Day2_4(){
            objDay2_4.SetActive(false);
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_4");
            
            dapur.extendAction.AddListener(Day2_5);
        }

        void initDay2_5(){
            Debug.Log("dapur");
            //Invoke("Day2_5", 2f);
            //IntearctButton.onClick.RemoveListener(initDay2_5);
            PintuUtama.enabled = true;
            
        }

        void Day2_5(){
            Debug.Log("Dapur aa");
            
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_5");
            dapur.extendAction.RemoveListener(Day2_5);
            PintuUtama.enabled = true;
            PintuUtama.extendAction.AddListener(Day2_6);

        }

        void Day2_6(){
            PintuUtama.enabled = false;
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_6");
            PintuUtama.extendAction.RemoveListener(Day2_6);
            TutorialEvents.OnTutorialComplete += endDay2_6;

        }

        void endDay2_6(){
            Invoke("Day2_7", 4f);
        }

        void Day2_7(){
            TutorialEvents.OnTutorialComplete -= endDay2_6;
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_7");
            TutorialEvents.OnTutorialComplete += initDay2_8;
            TutorialController.instance.hiddenBtn.onClick.AddListener(()=>
                dosen.isQuiz = true);
        }
        

        void initDay2_8(){
            TutorialEvents.OnTutorialComplete -= initDay2_8;
            QuizManager.instance.pilihanA.GetComponentInChildren<Button>().onClick.AddListener(Day2_8);
            QuizManager.instance.pilihanB.GetComponentInChildren<Button>().onClick.AddListener(Day2_8);
            QuizManager.instance.pilihanC.GetComponentInChildren<Button>().onClick.AddListener(Day2_8);
            QuizManager.instance.pilihanD.GetComponentInChildren<Button>().onClick.AddListener(Day2_8);
        }

        void Day2_8(){
            QuizManager.instance.pilihanA.GetComponentInChildren<Button>().onClick.RemoveListener(Day2_8);
            QuizManager.instance.pilihanB.GetComponentInChildren<Button>().onClick.RemoveListener(Day2_8);
            QuizManager.instance.pilihanC.GetComponentInChildren<Button>().onClick.RemoveListener(Day2_8);
            QuizManager.instance.pilihanD.GetComponentInChildren<Button>().onClick.RemoveListener(Day2_8);
            TutorialController.IsSkippable = false;
            objDay2_8.SetActive(true);
            TutorialLoader.instance.Load("Day2_8");
        }

        public void Day2_9(){
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_9");
            
        }

        public void initDay2_10(){
            IntearctButton.onClick.AddListener(Day2_10);
                
            dialogDay2_10.startDialog();
        }

        public void Day2_10(){
            IntearctButton.onClick.RemoveListener(Day2_10);
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_10");
        }

        public void Day2_11(){
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_11");
            objPaket.SetActive(true);
        }

        public void Day2_12(){
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_13");
            iQuest.enabled = true;
            objDay2_12.SetActive(true);
        }

        public void Day2_13(){
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_12");
            //iQuest.enabled = true;
            //objDay2_12.SetActive(true);
        }






        #endregion
    }
}
