using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru{

    public class DayManager : MonoBehaviour
    {
        public MapsManager maps;
        public List<Day> days = new List<Day>();

        public UnityEngine.Video.VideoClip clipsTidur;

        public List<int> GantiQuestList = new List<int>();

        public int day = 0;
        void Start(){
            maps.updateDayKonten(days[0].mapsId);
        }

        public void updateDay(){
            //FindObjectOfType<WaktuManager>().currentTime(0);
            FindObjectOfType<VideoManager>().action(clipsTidur);
            StartCoroutine(delayExecute());
        }


        IEnumerator delayExecute(){
            yield return new WaitForSeconds(4f);
            changeTime();
        }

        void changeTime(){
            day += 1;
            FindObjectOfType<UiManager>().updateEnergy(-3);
            
            FindObjectOfType<WaktuManager>().currentTime(0);
            maps.updateDayKonten(days[day].mapsId);
            if(days[day].itemSpawn.Count >= 1){
                foreach(var item in days[day].itemSpawn){
                    item.item.SetActive(true);
                }
            }

            foreach(var time in GantiQuestList){
                if(time == day){
                    QuestManager.instance.CurrentQuest = 1;
                    return;
                }
            }
        }

        public void updateMaps(){
            maps.afterQuiz(days[day].mapsId);
        }
    }

    [System.Serializable]
    public class Day{
        public List<id_Maps> mapsId;
        public List<spawnItem> itemSpawn;
    }

    [System.Serializable]
    public class spawnItem{
        public GameObject item;
        //public Transform posisiItem;
    }
}
