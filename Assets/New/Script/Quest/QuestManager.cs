using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Terbaru{
    public class QuestManager : MonoBehaviour
    {

        public static QuestManager instance ;
        //public List<listQuest> quests = new List<listQuest>();
        public List<QuestHarian> daily = new List<QuestHarian>();

        public listQuest currentQuest;
        public bool isActive = false;
        public List<GameObject> NPCs = new List<GameObject>();

        public Result_Quest result;
        //public int index;

        void Awake(){
            instance = this;
        }


        public int StateQuest;


        public int CurrentQuest{
            get {
                return StateQuest;
            }
            set{
                
                StateQuest += value;    
            }
        }

#region  List Quest
        public void StartProcessQuest(Quest quest){
            int index = 0;
            isActive = true;
            Debug.Log("Start Quest");
            FindObjectOfType<SoundManager>().playSoundQuest();
//            int hari = FindObjectOfType<Controller>().profil.GetHari() /  7;
            foreach(var _quest in daily[CurrentQuest].quest){
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

        public Quest[] initListQuest(){
            Quest[] temp = {
                null,
                null,
                null
            };

            List<Quest> lisTQuest = new List<Quest>();
            int hari = CurrentQuest;
            for(int a = 0; a < temp.Length; a++){
                foreach(var _daily in daily[hari].quest){
                    if(!_daily.quest.isDone && !lisTQuest.Contains(_daily.quest)){
                        lisTQuest.Add(_daily.quest);
                        temp[a] = _daily.quest;
                        break;
                    }
                }
            }
            //}
            // for(int i = 0; i < temp.Length; i++){
            //     foreach(var quest in quests){ 
            //         foreach(var daily in indexs[hari].id){
            //             if(quest.id == daily){
            //                 if(!quest.quest.isDone){
            //                     temp[i] = quest.quest;
            //                     break;
            //                 }
            //                 break;
            //             }
            //         }
            //     }
            //}
            //Debug.Log($"1 {temp[0].iD_Quiz} - 1 {temp[1].iD_Quiz} - 1 {temp[2].iD_Quiz}");
            return temp;
            
        }

        public Quest getQuest(int index){
            //Debug.Log(index);
            int hari = CurrentQuest;
            //int temp = indexs[hari].indexQuest[index];
            //Debug.Log(quests[temp].quest.judulMisi);
            return daily[hari].quest[index].quest; 
        }

        public int lenghtQuest(){
            int hari = CurrentQuest;
            return daily[hari].quest.Length; 
        }


        void StartProcess(int noIndex) => currentQuest.proses[noIndex].ProcessEventStart?.Invoke();
        
        void CloseProses(int noIndex) => currentQuest.proses[noIndex].ProcessEnd?.Invoke();
        
        void HelperProcess(int noIndex) => currentQuest.proses[noIndex].ProcessEventHelper?.Invoke();
        

        public void CheckAction(string Action){
            
            if(!isActive)
                return;

            int currentIndex = currentQuest.Index;
            if(currentQuest.proses[currentIndex].Action != Action){
                Debug.Log("Tidak Sesuai");
                return;
            }

            Debug.Log("Sesuai "+Action);

            CloseProses(currentIndex);
            NextProcess();
        }

        void NextProcess(){
            int tempIndex = currentQuest.Index;
            int jumlahProses = currentQuest.proses.Length - 1;
            //Debug.Log($"tempIndex {tempIndex} == jumlahProses {jumlahProses}");
            
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

            //Debug.Log("Quest Selesai");
            foreach(var lists in NPCs){
                lists.gameObject.SetActive(true);
                lists.GetComponent<NPC_Controller>().setPosition();
                
                foreach(var npc in playerProfil.character){
                    if(npc.objectNPC.name == lists.name){
                        npc.selected = false;
                    }
                }
            }
            //Debug.Log("Selesai Quest");
            currentQuest.quest.isDone = true;

            //playerProfil.Saldo += currentQuest.quest.Reward;

            //UiManager.instance.UpdateSaldo(playerProfil.Saldo);
            result.result(currentQuest);
            isActive = false;
            FindObjectOfType<SoundManager>().playSoundAsrama();
        }

#endregion
     
#region Quest
        public void StartQuest(listQuest quest){
            //int index = 0;
            isActive = true;
            // foreach(var _quest in quests){
            //     if(_quest.quest == quest){
            //         index = _quest.Index;
            //         currentQuest = _quest;
            //         break;
            //     }
            // }
            //Debug.Log($"Start Process Quest {index}"); 
            currentQuest = quest;
            StartProcessQuest(0);

            HelperProcessQuest(0);
        }

        // public Quest getQuest(int index){
        //     Debug.Log(index);
        //     int hari = FindObjectOfType<Controller>().profil.GetHari();
        //     int temp = indexs[hari].indexQuest[index];
        //     Debug.Log(quests[temp].quest.judulMisi);
        //     return quests[temp].quest; 
        // }


        void StartProcessQuest(int noIndex) => currentQuest.proses[noIndex].ProcessEventStart?.Invoke();
        
        void CloseProsesQuest(int noIndex) => currentQuest.proses[noIndex].ProcessEnd?.Invoke();
        
        void HelperProcessQuest(int noIndex) => currentQuest.proses[noIndex].ProcessEventHelper?.Invoke();
        

        public void CheckActionQuest(string Action){
            Debug.Log(Action);
            Debug.Log(isActive);
            if(!isActive)
                return;

            int currentIndex = currentQuest.Index;
            if(currentQuest.proses[currentIndex].Action != Action){
                return;
            }
            NextProcessQuest();
            CloseProsesQuest(currentIndex);
            
        }

        void NextProcessQuest(){
            Debug.Log("Next Quest");
            int tempIndex = currentQuest.Index;
            int jumlahProses = currentQuest.proses.Length - 1;
            //Debug.Log($"tempIndex {tempIndex} == jumlahProses {jumlahProses}");
            
            if(tempIndex < jumlahProses){
                
                currentQuest.Index += 1;
                StartProcessQuest(currentQuest.Index);
                Debug.Log("Start QUest");
                // StartProcess(currentQuest.Index);
                // HelperProcess(currentQuest.Index);
            }
            else{
                EndProcessQuest();
            }
        }

        void EndProcessQuest(){
            var playerProfil = GameManager.instance.profil;

            //Debug.Log("Quest Selesai");
            // foreach(var lists in NPCs){
            //     lists.gameObject.SetActive(true);
            //     lists.GetComponent<NPC_Controller>().setPosition();
                
            //     foreach(var npc in playerProfil.character){
            //         if(npc.objectNPC.name == lists.name){
            //             npc.selected = false;
            //         }
            //     }
            // }
            //currentQuest.quest.isDone = true;

            //playerProfil.Saldo += currentQuest.quest.Reward;
            Debug.Log("End");
            UiManager.instance.UpdateSaldo(playerProfil.Saldo);
            isActive = false;
        }
#endregion
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
            public ID_Quiz id;
            public Quest quest;
            public int Index;
            public questProses[] proses;
        }

        [System.Serializable]
        public class QuestHarian{
            public listQuest[] quest;
        }
}
