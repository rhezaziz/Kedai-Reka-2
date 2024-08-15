using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Terbaru{
    public class QuestManager : MonoBehaviour
    {
        public List<listQuest> quests = new List<listQuest>();

        public questProses currentQuest;

        public int index;

        public void StartProcessQuest(){
            Debug.Log($"Start Process Quest {index}");
            int processIndex = quests[index].Index;
            currentQuest = quests[index].proses[processIndex];

            StartProcess(processIndex);

            HelperProcess(processIndex);
        }

        void StartProcess(int noIndex) => quests[index].proses[noIndex].ProcessEventStart?.Invoke();
        
        void CloseProses(int noIndex) => quests[index].proses[noIndex].ProcessEnd?.Invoke();
        
        void HelperProcess(int noIndex) => quests[index].proses[noIndex].ProcessEventHelper?.Invoke();
        

        public void CheckAction(string Action){
            if(currentQuest.Action != Action){
                return;
            }

            CloseProses(quests[index].Index);
            NextProcess();
        }

        void NextProcess(){
            int tempIndex = quests[index].Index;
            int jumlahProses = quests[index].proses.Length - 1;
            
            if(tempIndex < jumlahProses){
                
                quests[index].Index += 1;
                StartProcess(quests[index].Index);
            }
            else{
                EndProcess();
            }
        }

        void EndProcess(){
            var playerProfil = GameManager.instance.profil;

            playerProfil.Saldo += quests[index].quest.Reward;
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
            // public string ProcessTittle;
            // public int Index;
            // public Kemampuan[] skills;
            // public Items item;
            // [TextArea(5,10)]
            // public string Deskripsi;
            // public int jumlahEnergy;
            // public int level;
            // public bool isDone;
            // public int hadiah;
            public Quest quest;
            public int Index;
            public questProses[] proses;
        }
}
