using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Terbaru{
    public class QuestManager : MonoBehaviour
    {

        public static QuestManager instance ;
        public List<listQuest> quests = new List<listQuest>();
        public List<QuestHarian> indexs = new List<QuestHarian>();

        public listQuest currentQuest;
        public bool isActive = false;
        public List<GameObject> NPCs = new List<GameObject>();
        //public int index;

        void Awake(){
            instance = this;
        }

        public void StartProcessQuest(Quest quest){
            int index = 0;
            isActive = true;
            foreach(var _quest in quests){
                if(_quest.quest == quest){
                    index = _quest.Index;
                    currentQuest = _quest;
                    break;
                }
            }
            Debug.Log($"Start Process Quest {index}"); 
            StartProcess(index);

            HelperProcess(index);
        }

        public Quest getQuest(int index){
            Debug.Log(index);
            int hari = FindObjectOfType<Controller>().profil.GetHari();
            int temp = indexs[hari].indexQuest[index];
            Debug.Log(quests[temp].quest.judulMisi);
            return quests[temp].quest;
            

            
        }


        void StartProcess(int noIndex) => currentQuest.proses[noIndex].ProcessEventStart?.Invoke();
        
        void CloseProses(int noIndex) => currentQuest.proses[noIndex].ProcessEnd?.Invoke();
        
        void HelperProcess(int noIndex) => currentQuest.proses[noIndex].ProcessEventHelper?.Invoke();
        

        public void CheckAction(string Action){
            Debug.Log(Action);
            if(!isActive)
                return;

            int currentIndex = currentQuest.Index;
            if(currentQuest.proses[currentIndex].Action != Action){
                return;
            }

            CloseProses(currentIndex);
            NextProcess();
        }

        void NextProcess(){
            int tempIndex = currentQuest.Index;
            int jumlahProses = currentQuest.proses.Length - 1;
            
            if(tempIndex < jumlahProses){
                
                currentQuest.Index += 1;
                StartProcessQuest(currentQuest.quest);
                // StartProcess(currentQuest.Index);
                // HelperProcess(currentQuest.Index);
            }
            else{
                EndProcess();
            }
        }

        void EndProcess(){
            var playerProfil = GameManager.instance.profil;


            foreach(var lists in NPCs){
                lists.gameObject.SetActive(true);
                lists.GetComponent<NPC_Controller>().setPosition();
                
                foreach(var npc in playerProfil.character){
                    if(npc.objectNPC.name == lists.name){
                        npc.selected = false;
                    }
                }
            }
            currentQuest.quest.isDone = true;

            playerProfil.Saldo += currentQuest.quest.Reward;

            UiManager.instance.UpdateSaldo(playerProfil.Saldo);
            isActive = false;
        }
    }

    [System.Serializable]
    public class questProses{
        public string Action;
        public UnityEvent ProcessEventStart;
        public UnityEvent ProcessEventHelper;
        public UnityEvent ProcessEnd;
            
        }

        [System.Serializable]
        public class listQuest{
            public Quest quest;
            public int Index;
            public questProses[] proses;
        }

        [System.Serializable]
        public class QuestHarian{
            public int[] indexQuest;
        }
}
