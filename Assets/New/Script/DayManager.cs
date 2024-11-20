using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Terbaru{

    public class DayManager : MonoBehaviour
    {
        public MapsManager maps;
        public List<Day> days = new List<Day>();

        public UnityEngine.Video.VideoClip clipsTidur;

        public List<int> GantiQuestList = new List<int>();

        public List<dayQuest> quests = new List<dayQuest>();

        public GameObject Dosen;

        public int day = 0;
        public UnityEngine.UI.Button kembaliMaps;

        public int currectDay(){
            return day;
        }
        void Start(){
            maps.updateDayKonten(days[0].mapsId);
        }

        public void gantiHariTest(TMPro.TMP_InputField hari){
            FindObjectOfType<Controller>().currentState(state.Interaction);
            day = int.Parse(hari.text) - 1;
            updateDay();
        }

        public void updateDay(){
            kembaliMaps.enabled = false;
            //FindObjectOfType<WaktuManager>().currentTime(0);
            initContentQuest(false);
            //
            FindObjectOfType<VideoManager>().action(clipsTidur);
            Invoke("changeTime",4);
            
            //StartCoroutine(delayExecute());
            FindObjectOfType<WaktuManager>().changeInteraction();
            FindObjectOfType<Mingguan>().stopEvent();
        }


        IEnumerator delayExecute(){
            yield return new WaitForSeconds(4f);
            changeTime();
        }

        void changeTime(){
            day += 1;
            FindObjectOfType<Mingguan>().startEvent();
            FindObjectOfType<UiManager>().updateEnergy(-3);
            FindObjectOfType<QuizManager>().setSoal(day);

            FindObjectOfType<WaktuManager>().currentTime(0);
            // Dosen.GetComponent<DialogObjectClick>().interactNPC(true);
            maps.updateDayKonten(days[day].mapsId);
            if(days[day].itemSpawn.Count >= 1){
                foreach(var item in days[day].itemSpawn){
                    item.item.SetActive(true);
                }

                foreach(var person in days[day].orang){
                    person.interactNPC(true);
                }
            }

            if(days[Mathf.Clamp(day - 1, 0, 30)].itemSpawn.Count >= 1){
                foreach(var item in days[day].itemSpawn){
                    item.item.SetActive(false);
                }
            }

            initContentQuest(true);
            for(int i = 0; i < GantiQuestList.Count; i++){
                if(GantiQuestList[i] == day){
                    QuestManager.instance.StateQuest = i + 1;
                    return;
                }
            }
            // foreach(var time in GantiQuestList){
            //     if(time == day){
            //         QuestManager.instance.CurrentQuest = 1;
            //         return;
            //     }
            // }
        }

        public void updateMaps(){
            maps.afterQuiz(days[day].mapsId);
        }

        public void initContentQuest(bool thisDay){
            foreach(var quest in quests){
                
                int tempDay = thisDay ? day : day - 1;

                if(quest.day == Mathf.Clamp(tempDay, 0, 30)){
                    foreach(var item in quest.interact){
                        
                        item.GetComponent<Interaction>().changeInteractable(thisDay);
                    }
                    //Debug.Log(quest.nama +" : " + quest.day);
                    if (thisDay) quest.events?.Invoke();
                }
            }
        }
    }

    [System.Serializable]
    public class Day{
        public List<id_Maps> mapsId;
        public List<spawnItem> itemSpawn;
        public List<DialogObjectClick> orang;
    }

    [System.Serializable]
    public class spawnItem{
        public GameObject item;
        //public Transform posisiItem;
    }

    [System.Serializable]
    public class dayQuest{
        public string nama;
        public int day;
        public UnityEvent events;

        public List<GameObject> interact = new List<GameObject>();
    }
}
