using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Terbaru{

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public WaktuManager waktu;

        public playerProfil profil;


        void Awake(){
            instance = this;
            
        }
        void Start(){
            DontDestroyOnLoad(this);

            waktu.currentTime(0);
        }
        
        void OnEnable(){
            var player = GameObject.Find("Player").transform.GetChild(0);

            player.localPosition = profil.position;

            spawnAllNPC();
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
        
        public void pindahScene(){
            PlayerPrefs.SetString("Scene", "Asrama");
            SceneManager.LoadScene("Loading");
        }
    }
}