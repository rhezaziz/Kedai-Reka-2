using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Terbaru{

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public bool mainMenu;
        public bool Test;

        public List<GameObject> obj = new List<GameObject>();

        public WaktuManager waktu;

        public playerProfil profil;
        public GameObject panelUtama;

        [Header("Tutorial")]
        public bool isTutorial;
        public List<GameObject> interactions = new List<GameObject>();
        public GameObject controllerUI;
        public GameObject infoUI;
        public GameObject UIAwal;
        public GameObject tutorialUI;
        public GameObject tutorialManager;


        void Awake(){
            instance = this;
            
        }
        void Start(){
            DontDestroyOnLoad(this);

            FindObjectOfType<Controller>().GantiPerempuan(false);
            //PlayerPrefs.SetInt("Selesai", 0);
            waktu.currentTime(2);
            checkTutorial();
            checkMainMenu();
        }

        public void updateMainMenu(bool value)
        {
            UiManager.instance.initProfil();
            mainMenu = value;
            checkMainMenu();
            SoundManager.instance.playSoundAsrama();

            if(NPCs.Count != 0)
            {
                for(int i = 0; i < NPCs.Count; i++)
                {
                    bool unlock = profil.character[i].characterLock;

                    NPCs[i].gameObject.SetActive(!unlock);
                }
            }
        }

        public void checkMainMenu()
        {
            bool selesai = PlayerPrefs.GetInt("Selesai") == 1 ? true : false;
            bool temp = !mainMenu || selesai;
            panelUtama.SetActive(temp);
            
            if (mainMenu && !selesai)
            {
                PlayerPrefs.SetInt("Selesai", 0);
                SoundManager.instance.stopAudio();
                FindObjectOfType<MiniGame>().openMainMenu("New Scene");
            }

            PlayerPrefs.SetInt("Selesai", 0);
        }

        public void DestroyThisObject()
        {
            Destroy(gameObject);
        }

        void checkTutorial(){
            foreach(var interact in interactions){
                //interact.GetComponent<Interaction>().isTutorial(!isTutorial);
                interact.GetComponent<Interaction>().changeInteractable(!isTutorial);
            }
            // if(!isTutorial)
            //     return;
            
            
            FindObjectOfType<Movement>().move = !isTutorial;
            controllerUI.SetActive(!isTutorial);
            UIAwal.SetActive(isTutorial);
            infoUI.SetActive(!isTutorial);
            tutorialUI.SetActive(isTutorial);
            tutorialManager.SetActive(isTutorial);
            if(isTutorial)
                tutorialManager.transform.GetChild(0).GetComponent<TutorialManager>().initTutorial();

        }

        


        public void tutorialOver(){
            tutorialUI.SetActive(false);
            tutorialManager.SetActive(false); 
            isTutorial = false;
            foreach(var interact in interactions){
                interact.GetComponent<Interaction>().changeInteractable(true);
            }
            foreach(var item in profil.item)
            {
                item.isShop = !item.isSpawn;
            }
        }
        
        void OnEnable(){
            var player = GameObject.Find("Player").transform.GetChild(0);

            FindObjectOfType<SoundManager>().playSoundAsrama();

            if(!Test) player.localPosition = profil.position;

            spawnAllNPC();
        }

        public void GetItem(Items item)
        {
            item.isInventory = true;
        }
        public void testItem(itemType type)
        {
            foreach (var item in profil.item)
            {
                if(item.itemType == type)
                {
                    item.isInventory = true;
                }
            }
        }

        public bool haveItem(itemType type)
        {
            foreach (var item in profil.item)
            {
                if (item.itemType == type)
                {
                    return item.isInventory;
                }
            }
            return false;
        }

        public bool haveItem(Items item)
        {
            return item.isInventory;
        }


        void spawnAllNPC(){
             
             for(int i = 0; i < profil.character.Count; i++){
                    spawnNPC(i);
             }
        }

        List<GameObject> NPCs = new List<GameObject>();
        public void spawnNPC(int index){
            var NPC_Profil = profil.character;
            GameObject parent = GameObject.Find("NPC");

            GameObject NPC_Object = Instantiate(NPC_Profil[index].objectNPC);
            NPC_Object.transform.SetParent(parent.transform);
            NPC_Object.transform.localPosition = NPC_Profil[index].Posisi;
            NPC_Object.name = NPC_Profil[index].objectNPC.name;
            NPC_Object.GetComponent<NPC_Controller>().Posisi = NPC_Profil[index].Posisi;
            NPCs.Add(NPC_Object);
            
            NPC_Object.SetActive(!NPC_Profil[index].selected);
            NPC_Object.SetActive(!NPC_Profil[index].characterLock);
        }
        public void updateCharacter(int index){
            NPCs[index].SetActive(!profil.character[index].characterLock);
        }

        public void resetNPC()
        {
            for (int i = 0; i < profil.character.Count; i++) {
                updateCharacter(i);
            }
        }

        public void readyMission(List<GameObject> NPC, Quest quest){
            List<GameObject> NPC_Quest = new List<GameObject>();
            foreach(var npc in NPC){
                foreach(var temp in NPCs){
                    if(npc.name == temp.name)
                        NPC_Quest.Add(temp);
                }   
            }
            setUpPositionNPC(NPC_Quest, quest);

        }

        public void QuizPilihKarakter(List<GameObject> NPC)
        {
            List<GameObject> NPC_Quest = new List<GameObject>();
            foreach (var npc in NPC)
            {
                foreach (var temp in NPCs)
                {
                    if (npc.name == temp.name)
                        temp.gameObject.SetActive(false);
                }
            }
        }

        public void disabledInteractNPC()
        {
            foreach (var npc in NPCs)
            {
                npc.GetComponent<Interaction>().changeInteractable(false);
            }
            interactions[0].GetComponent<Interaction>().changeInteractable(false);
        }

        public void enabledInteractNPC()
        {
            foreach (var npc in NPCs)
            {
                npc.GetComponent<Interaction>().changeInteractable(true);
            }
        }

        public void setUpPositionNPC(List<GameObject> NPC, Quest quest){
            Vector3[] posisi = {
                new Vector3(30f, 36f, -122.5f),
                new Vector3(37.5f, 36f, -122.5f),
                new Vector3(30f, 36f, -122.5f),
                
            };
            for(int i = 0; i < NPC.Count; i++){
                Debug.Log(i);
                bool flip = i == 1 ;
                NPC[i].transform.position = posisi[i];
                NPC[i].GetComponent<NPC_Controller>().selectedQuest(flip);
            }

            UiManager.instance.mulaiQuest(NPC, quest);
        }

        public GameObject npcCharacter(GameObject NPC){
            foreach(var npc in NPCs){
                if(NPC.name == npc.name){
                    return npc;
                }
            }

            return null;
        }
        
        public void rekrutAbdan(listCharacters character)
        {
            character.characterLock = false;
            npcCharacter(character.objectNPC).SetActive(!character.characterLock);
        }
        public void pindahScene(){
            PlayerPrefs.SetString("Scene", "Asrama");
            SceneManager.LoadScene("Loading");
        }
    }
}