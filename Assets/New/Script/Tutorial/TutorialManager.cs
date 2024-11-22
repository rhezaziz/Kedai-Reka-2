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
        [Header("UI Maps")]
        public Button kembali;
        public GameObject parentNPC;
        #region Component Tutorial Day 1
        [Header("Panel Utama")]
        public Button Interaksi;
        public GameObject MoveController;
        public GameObject panelValuePlayer;
        public Button IntearctButton;
        public Button ipad;
        public QuizManager qm;
        public Button resultButton;

        [Header("Interact Object")]
        public GameObject kucing;
        public Interaksi_Animasi iKucing;
        public GameObject TriggerAnimasiTrigger;
        public PapanQuest panelQuest;
        public Pintu PintuKamar;
        public Pintu KamarMandi;
        public Pintu PintuUtama;

        public Komputer komputer;
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
        public Button closePanelQuest;
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
        public GameObject objDay3_7;

        [Header("Dialog")]
        public TutorialDialog dialogDay2_10;

        [Header("")]
        public listCharacters[] dataCharacter;
        public Items[] item;
        public Quest[] quest;

        #endregion
        
        #region  Tutorial Hari Pertama Fase 1

        //Pengenalan UI Panel Nama, Energy, dan Controller;

        public void initTutorial(){
            foreach(var i in item){
                i.isInventory = false;
                i.isShop = false;
            }

            item[0].isShop = true;
                
            
            foreach(var i in quest)
                i.isDone = false;
            
            foreach(var i in dataCharacter){
                i.characterLock = true;
                i.selected = false;
            }

            ipad.interactable = false;
        }
        public void playDay1_1()
        {
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day1_1");
            kucing.SetActive(true);
            //item.isInventory = false;
            
                
            MoveController.SetActive(true);
            //TutorialController.instance.hiddenBtn.onClick.AddListener()
            panelValuePlayer.SetActive(true);
            
            FindObjectOfType<Movement>().move = true;
        }
        
        //Memberitahu ada interaksi
        public void playDay1_2(){
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day1_2");
            // iKucing.enabled = true;
            iKucing.changeInteractable(true);
            FindObjectOfType<Controller>().currentState(state.Interaction);
            UiManager.instance.panelUtama.SetActive(false);
            
            TutorialEvents.OnTutorialComplete += endDay1_2;
            
        }

        void endDay1_2(){
            TutorialEvents.OnTutorialComplete -= endDay1_2;
            UiManager.instance.panelUtama.SetActive(true);
            FindObjectOfType<Controller>().currentState(state.Default);
             UiManager.instance.bantuanText("Dekati Kucing");
            // iKucing.enabled = true;
        
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
            UiManager.instance.bantuanText("");
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
            UiManager.instance.bantuanText("Pergi Ke papan Kasus");
            TutorialEvents.OnTutorialComplete -= endDay1_5;
            var camera = Camera.main;
            camera.transform.localPosition = new Vector3(0, 1.75f, -10f);
            UiManager.instance.panelUtama.SetActive(true);
            FindObjectOfType<Controller>().currentState(state.Default);
            GameObject dindaSpawn = GameManager.instance.npcCharacter(dindaData.objectNPC);
            dindaSpawn.SetActive(true);
            dinda.GetComponent<Interaction>().changeInteractable(true);
            
            dindaSpawn.transform.SetParent(dinda.transform);
            dindaSpawn.transform.localPosition = Vector3.zero;
            dindaSpawn.GetComponent<Interaction>().changeInteractable(false);
            dindaSpawn.GetComponent<Collider>().enabled = false;
            colDapur.SetActive(false);
            //dindaData.characterLock = false;
            var action = dinda.GetComponent<TutorialDialog>();
            var NpcControl = dindaSpawn.GetComponent<NPC_Controller>();
            action.animasiObj = NpcControl.gameObject;
            action.OnAction.AddListener(() =>NpcControl.currentCondition(animasi.Idle));
            //Vector3 posDinda = dindaData.Posisi;
            action.OnAction.AddListener(() =>
            {
                dinda.GetComponent<Interaction>().changeInteractable(false);
                actionEnd(dindaSpawn);
                dinda.SetActive(false);
                
                dindaData.characterLock = false;
                panelQuest.changeInteractable(true);
            });
            //dinda.SetActive(true);
        }
        GameObject tempDinda;
        void actionEnd(GameObject dinda){
            tempDinda = dinda;
            Transform parent = GameObject.Find("NPC").transform;
            //dinda.SetActive(false);
            dinda.transform.SetParent(parent); 
            dinda.transform.localPosition = dindaData.Posisi;
            FindObjectOfType<Controller>().currentState(state.Interaction);
            panelQuest.action(FindObjectOfType<Controller>().transform);
            Day1_6();
            dindaData.characterLock = false;
            UiManager.instance.bantuanText("");
        }
        

        public void initDay1_6(){
            UiManager.instance.panelUtama.SetActive(false);
            FindObjectOfType<Controller>().currentState(state.Interaction);

        }

        void Day1_6(){
            
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day1_6");
            colDeketKamar.SetActive(true);
            //UiManager.instance.bantuanText("Pergi Ke depan pintu");
        }

        public void Day1_7(){
            UiManager.instance.panelUtama.SetActive(false);
            FindObjectOfType<Controller>().currentState(state.Interaction);
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day1_7");
            UiManager.instance.bantuanText("Masuk ke kamar");
            TutorialEvents.OnTutorialComplete += endDay1_7;
        }

        void endDay1_7(){
            UiManager.instance.bantuanText("");
            QuestManager.instance.CheckAction("Tidur");
            TutorialEvents.OnTutorialComplete -= endDay1_7;
            UiManager.instance.panelUtama.SetActive(true);
            FindObjectOfType<Controller>().currentState(state.Default);
            PintuKamar.ExtraAction.AddListener(initDay2_1);
            tempDinda.SetActive(false);
        }

        #endregion

        #region Day 2
        
        void initDay2_1(){
            Debug.Log("tes 1 1");
            Invoke("Day2_1", 8f);
            colDeketKamar.SetActive(false);
            //Day2_1();
            
        }

        void Day2_1(){
            Debug.Log("tes");
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_1");
            objDay2_2.SetActive(true);
            PintuKamar.ExtraAction.RemoveAllListeners();
            // PintuKamar.enabled = false;
            PintuKamar.changeInteractable(false);
            dindaData.selected = true;
            UiManager.instance.bantuanText("Pergi ke kamar mandi");
        }

        public void Day2_2(){
            objDay2_2.SetActive(false);
            // KamarMandi.enabled = true;
            FindObjectOfType<Controller>().currentState(state.Interaction);
            KamarMandi.changeInteractable(true);
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_2");
            TutorialEvents.OnTutorialComplete += endDay2_2;
            
        }

        void endDay2_2(){
            TutorialEvents.OnTutorialComplete -= endDay2_2;
            KamarMandi.ExtraAction.AddListener(initDay2_3);
            FindObjectOfType<Controller>().currentState(state.Default);
        }

        void initDay2_3(){
            
            
            KamarMandi.ExtraAction.RemoveAllListeners();
            // KamarMandi.enabled = false;
            KamarMandi.changeInteractable(false);
            Invoke("Day2_3", 5f);
        }

        void Day2_3(){
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_3");
            objDay2_4.SetActive(true);
            UiManager.instance.bantuanText("Pergi ke dapur");
        }

        public void Day2_4(){
            objDay2_4.SetActive(false);
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_4");
            dapur.changeInteractable(true);
            dapur.extendAction.AddListener(Day2_5);
            FindObjectOfType<Controller>().currentState(state.Interaction);
            TutorialEvents.OnTutorialComplete += initDay2_5;
        }

        void initDay2_5(){
            Debug.Log("dapur");
            //Invoke("Day2_5", 2f);
            TutorialEvents.OnTutorialComplete -= initDay2_5;
            FindObjectOfType<Controller>().currentState(state.Default);
            //IntearctButton.onClick.RemoveListener(initDay2_5);
            //PintuUtama.enabled = true;
            
        }

        void Day2_5(){
            dapur.changeInteractable(false);
            UiManager.instance.bantuanText("Pergi ke pintu Utama");
            //FindObjectOfType<Controller>().currentState(state.Default);
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_5");
            dapur.extendAction.RemoveListener(Day2_5);
            //PintuUtama.enabled = true;
            PintuUtama.changeInteractable(true);
            PintuUtama.ExtraAction.AddListener(Day2_6);

        }

        void Day2_6(){
            // PintuUtama.enabled = false;
            TutorialController.IsSkippable = false;
            UiManager.instance.bantuanText("");
            TutorialLoader.instance.Load("Day2_6");
            PintuUtama.changeInteractable(false);
            PintuUtama.ExtraAction.RemoveAllListeners();
            TutorialEvents.OnTutorialComplete += endDay2_6;

        }

        void endDay2_6(){
            Invoke("Day2_7", 4f);
        }

        void Day2_7(){
            UiManager.instance.bantuanText("Ajak Ngobrol Dosen");
            dosen.onDialog = false;
            TutorialEvents.OnTutorialComplete -= endDay2_6;
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_7");
            TutorialEvents.OnTutorialComplete += initDay2_8;
            // TutorialController.instance.hiddenBtn.onClick.AddListener(()=>
            //     );
        }
        

        void initDay2_8(){
            dosen.onDialog = true;
            TutorialEvents.OnTutorialComplete -= initDay2_8;
            qm.pilihanA.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Day2_8);
            qm.pilihanB.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Day2_8);
            qm.pilihanC.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Day2_8);
            qm.pilihanD.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Day2_8);
        }

        public void Day2_8(){
            Debug.Log("Day 2 - 8");
            qm.pilihanA.transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(Day2_8);
            qm.pilihanB.transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(Day2_8);
            qm.pilihanC.transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(Day2_8);
            qm.pilihanD.transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(Day2_8);
            TutorialController.IsSkippable = false;
            objDay2_8.SetActive(true);
            TutorialLoader.instance.Load("Day2_8");
            colDeketKamarPintu.SetActive(false);
            colDeketKamar.SetActive(false);
            UiManager.instance.bantuanText("Kembali Ke Asrama");
        }

        public void Day2_9(){
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_9");
            colDeketKamarPintu.SetActive(false);
            colDeketKamar.SetActive(false);
            UiManager.instance.bantuanText("Pergi ke papan Kasus");
        }

        public void initDay2_10(){

            IntearctButton.onClick.AddListener(Day2_10);
                
            dialogDay2_10.startDialog();
        }

        public void Day2_10(){
            UiManager.instance.bantuanText("Pergi Ke komputer");
            IntearctButton.onClick.RemoveListener(Day2_10);
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_10");
            komputer.changeInteractable(true);
            //Komputer.changeInteractable(true);
        }

        public void Day2_11(){
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_11");
            ipad.gameObject.SetActive(true);
            objPaket.SetActive(true);
            UiManager.instance.bantuanText("Pergi ambil paket");
//            objPaket.GetComponent<Interaction>().changeInteractable(true);
        }

        public void Day2_12(){
            TutorialController.IsSkippable = false;
            komputer.changeInteractable(false);
            ipad.interactable = false;
            TutorialLoader.instance.Load("Day2_13");
            UiManager.instance.bantuanText("Kembali Ke papan Kasus");
            iQuest.changeInteractable(true);
            objDay2_12.SetActive(true);
            resultButton.onClick.AddListener(() => {
                Day2_14();
            });
        }

        public void Day2_13(){
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_12");
            closePanelQuest.interactable = false;
            
            //iQuest.enabled = true;
            //objDay2_12.SetActive(true);
            
        }

        void Day2_14(){
            
            Debug.Log("Day2_14");
            PintuKamar.ExtraAction.AddListener(initDay3_1);
            UiManager.instance.bantuanText("Pergi ke kamar");
            closePanelQuest.interactable = true;
            PintuKamar.changeInteractable(true);
            iQuest.changeInteractable(false);
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_14");
            resultButton.onClick.RemoveListener(() => {
                Day2_14();
            });
        }

        // void Day2_15(){
        //     TutorialController.IsSkippable = false;
        //     TutorialLoader.instance.Load("Day2_15");
        //     resultButton.onClick.RemoveListener(() => {
        //         Day2_15();
        //     });
        //     PintuKamar.extendAction.RemoveListener(Day3_1);
        // }






        #endregion


        #region Day3

        void initDay3_1(){
            Debug.Log("init 3 _ 1");
            //PintuKamar.extendAction.RemoveListener(initDay3_1);
            Invoke("Day3_1", 8f);
            colDeketKamar.SetActive(false);
            //Day2_1();
            
        }
        void Day3_1(){
            //Debug.Log("tes");
            TutorialController.IsSkippable = false;

            // TutorialController.IsSkippable = false;
            // TutorialLoader.instance.Load("Day2_1");
            // objDay2_2.SetActive(true);
            // PintuKamar.extendAction.RemoveListener(initDay2_1);
           
            TutorialLoader.instance.Load("Day3_1");
            PintuKamar.ExtraAction.RemoveAllListeners();
            UiManager.instance.bantuanText("Pergi ke kamar mandi");
            //objDay2_2.SetActive(true);
            Debug.Log("3_1");
            //PintuKamar.enabled = false;
            PintuKamar.changeInteractable(false);
            dindaData.selected = false;
            KamarMandi.changeInteractable(true);
            KamarMandi.ExtraAction.AddListener(initDay3_2);
        }

         void initDay3_2(){
            KamarMandi.ExtraAction.RemoveAllListeners();
            // KamarMandi.enabled = false;
            KamarMandi.changeInteractable(false);
            Invoke("Day3_2", 5f);
        }

        public void Day3_2(){
            objDay2_2.SetActive(false);
            Debug.Log("3_2");
            //KamarMandi.enabled = true;
            UiManager.instance.bantuanText("Pergi ke dapur");
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day3_2");
            KamarMandi.ExtraAction.RemoveAllListeners();
            KamarMandi.changeInteractable(false);
            dapur.changeInteractable(true);
            dapur.extendAction.AddListener(Day3_3);
        }

        // void initDay3_3(){
        //     KamarMandi.extendAction.RemoveListener(initDay3_3);
        //     KamarMandi.enabled = false;
        //     KamarMandi.changeInteractable(false);
        //     Invoke("Day2_3", 5f);
        // }

        void Day3_3(){
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day3_3");
            Debug.Log("3_3");
            UiManager.instance.bantuanText("Pergi ke kelas");
            
            dapur.changeInteractable(false);
            dapur.extendAction.RemoveListener(Day3_3);
            //PintuUtama.enabled = true;
            PintuUtama.changeInteractable(true);
            PintuUtama.ExtraAction.AddListener(Day3_4);
            //objDay2_4.SetActive(true);
        }

         public void Day3_4(){
            
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day3_4");
            PintuUtama.changeInteractable(false);
            PintuUtama.ExtraAction.RemoveAllListeners();
            //TutorialEvents.OnTutorialComplete += endDay3_4;
        }

        void endDay3_4(){
            Invoke("Day3_5", 6f);
        }

        public void Day3_5(){
            dosen.onDialog = false;
            Debug.Log("Day3_5");
            TutorialEvents.OnTutorialComplete -= endDay3_4;
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day2_7");
            TutorialEvents.OnTutorialComplete += initDay3_6;
            
        }

        // void Day3_5(){
            
        //     TutorialEvents.OnTutorialComplete -= endDay3_6;
        //     TutorialController.IsSkippable = false;
        //     TutorialLoader.instance.Load("Day2_7");
        //     TutorialEvents.OnTutorialComplete += initDay2_8;
            
            

        // }

        public void initDay3_6(){
            kembali.interactable = false;
            UiManager.instance.bantuanText("Ajak Ngobrol Dosen");
            dosen.onDialog = true;
            TutorialEvents.OnTutorialComplete -= initDay3_6;
            qm.pilihanA.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Day3_6);
            qm.pilihanB.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Day3_6);
            qm.pilihanC.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Day3_6);
            qm.pilihanD.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Day3_6);
        }

        void Day3_6(){
            Debug.Log("Day3_6");
            qm.pilihanA.transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(Day2_6);
            qm.pilihanB.transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(Day2_6);
            qm.pilihanC.transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(Day2_6);
            qm.pilihanD.transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(Day2_6);
            TutorialController.IsSkippable = false;
            //objDay3_7.SetActive(true);
            TutorialLoader.instance.Load("Day3_6");
            kembali.interactable = true;
            kembali.onClick.AddListener(initDay3_7);
            
        }

        void initDay3_7(){
            kembali.onClick.RemoveListener(initDay3_7);
            Invoke("Day3_7", 5);
        }

        void Day3_7(){
            Debug.Log("Day3_7");
            UiManager.instance.bantuanText("Kembali Ke papan Kasus");
            // TutorialEvents.OnTutorialComplete += endDay3_7;
            //FindObjectOfType<Controller>().currentState(state.Interaction);
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day3_7");
            iQuest.changeInteractable(true);
            resultButton.onClick.AddListener(Day3_8);
            objDay2_2.SetActive(false);
            objDay2_4.gameObject.SetActive(false);
            objDay2_8.SetActive(false);

        }

        // void endDay3_7(){
        //     TutorialEvents.OnTutorialComplete -= endDay3_7;
        //     FindObjectOfType<Controller>().currentState(state.Default);
            
        // }
        public GameObject dindaQuest;
        void Day3_8(){
            Debug.Log("Day3_8");
            PintuKamar.changeInteractable(false);
            UiManager.instance.bantuanText("Cari Dinda");
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day3_8");
            GameObject dindaNPC = null;


            for(int i = 0; i < parentNPC.transform.childCount; i++){
                if(parentNPC.transform.GetChild(i).name == "Dinda"){
                    dindaNPC = parentNPC.transform.GetChild(i).gameObject;
                    dindaNPC.SetActive(true);
                    dindaNPC.GetComponent<Interaction>().changeInteractable(true);
                    dindaNPC.GetComponent<Collider>().enabled = true;
                    break;
                }
            }
            dindaQuest = dindaNPC;
            dindaNPC.GetComponent<NPC_Dialog>().SetindexDialog(1);
            dindaNPC.GetComponent<Interaction>().changeInteractable(true);
            resultButton.onClick.RemoveListener(Day3_8);
        }

        public void Day3_9(){
            Debug.Log("Day3_9");
            UiManager.instance.bantuanText("Pergi cari barang");
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day3_9");
        }

        public Transform NPC;

        public void Day3_10(){
            // Debug.Log("Day3_9");
            UiManager.instance.bantuanText("Kembali ke asrama");
            dindaQuest.GetComponent<NPC_Dialog>().SetindexDialog(0);
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day3_10");
            kembali.onClick.AddListener(() => {
                initDay3_11();
            });
        }
        void initDay3_11(){
            Invoke("Day3_11", 4);   
            kembali.onClick.RemoveListener(() => {
                initDay3_11();
            });
        }   
        public void Day3_11(){
            UiManager.instance.bantuanText("Pergi ke kamar");
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Day3_11");
            PintuKamar.changeInteractable(true);
            PintuKamar.ExtraAction.AddListener(() => {
                initDayOver();
            });
        }

        void initDayOver(){
            Debug.Log($"Sebelum {PintuKamar.ExtraAction.GetPersistentEventCount()}");
            Debug.Log("init Day Over");
            PintuKamar.ExtraAction.RemoveAllListeners();
            Debug.Log($"Sesudah {PintuKamar.ExtraAction.GetPersistentEventCount()}");
            //PintuKamar.extendAction.RemoveListener(initDay3_1);
            // UiManager.instance.bantuanText("Pergi ke kamar mandi");

            colDeketKamar.SetActive(false);
            PintuKamar.changeInteractable(false);
            StartCoroutine(Over());
            //Day2_1();
            
        }

        IEnumerator Over(){
            yield return new WaitForSeconds(8f);
            Debug.Log("Over");
            TutorialController.IsSkippable = false;
            TutorialLoader.instance.Load("Over");
            TutorialEvents.OnTutorialComplete += overTutorial;
            //GameManager.instance.tutorialOver();
        }

        public void overTutorial(){
            PintuKamar.ExtraAction.RemoveAllListeners();
            TutorialEvents.OnTutorialComplete -= overTutorial;
            GameManager.instance.tutorialOver();
            UiManager.instance.bantuanText("");
            ipad.interactable = true;
            dindaData.selected = false;
            Destroy(this.gameObject);
        }

        // void Day3_6(){
        //     //PintuUtama.enabled = false;
        //     TutorialController.IsSkippable = false;
        //     TutorialLoader.instance.Load("Day2_6");
            
        //     TutorialEvents.OnTutorialComplete += endDay2_6;

        // }

        

        // void Day3_7(){
        //     TutorialEvents.OnTutorialComplete -= endDay3_6;
        //     TutorialController.IsSkippable = false;
        //     TutorialLoader.instance.Load("Day2_7");
        //     TutorialEvents.OnTutorialComplete += initDay2_8;
        //     TutorialController.instance.hiddenBtn.onClick.AddListener(()=>
        //         dosen.onDialog = true);
        // }

       

        // void initDay3_5(){
        //     Debug.Log("dapur");
        //     //Invoke("Day2_5", 2f);
        //     //IntearctButton.onClick.RemoveListener(initDay2_5);
        //     PintuUtama.enabled = true;
            
        // }

        

        

        
        

        

        // public void Day3_9(){
        //     TutorialController.IsSkippable = false;
        //     TutorialLoader.instance.Load("Day2_9");
        //     colDeketKamarPintu.SetActive(false);
            
        // }

        #endregion
    }
}
