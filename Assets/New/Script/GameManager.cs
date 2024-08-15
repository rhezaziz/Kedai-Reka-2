using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Terbaru{

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public playerProfil profil;


        void Awake(){
            instance = this;
            
        }
        void Start(){
            DontDestroyOnLoad(this);
        }
        
        void OnEnable(){
            var player = GameObject.Find("Player").transform.GetChild(0);

            player.localPosition = profil.position;

            spawnAllNPC();
        }
        

        void spawnAllNPC(){
             
             for(int i = 0; i < profil.character.Count; i++){
                if(!profil.character[i].characterLock){
                    spawnNPC(i);
                }
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

            NPCs.Add(NPC_Object);
            NPC_Object.SetActive(!NPC_Profil[index].selected);
        }

        public void readyMission(GameObject[] NPC){
            List<GameObject> NPC_Quest = new List<GameObject>();
            foreach(var npc in NPC){
                foreach(var temp in NPCs){
                    if(npc.name == temp.name)
                        NPC_Quest.Add(temp);
                }   
            }
            FindObjectOfType<Misi_Manager>().setUpPositionNPC(NPC_Quest);

        }
        public void pindahScene(){
            PlayerPrefs.SetString("Scene", "Asrama");
            SceneManager.LoadScene("Loading");
        }
    }
}