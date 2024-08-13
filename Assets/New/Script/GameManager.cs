using System.Collections;
using System.Collections.Generic;
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

        public void spawnNPC(int index){
            var NPC = profil.character;
            GameObject parent = GameObject.Find("NPC");

            GameObject NPC_Object = Instantiate(NPC[index].objectNPC);
            NPC_Object.transform.SetParent(parent.transform);
            NPC_Object.transform.localPosition = NPC[index].Posisi;
        }

        public void pindahScene(){
            PlayerPrefs.SetString("Scene", "Asrama");
            SceneManager.LoadScene("Loading");
        }
    }
}