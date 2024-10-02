using System.Collections;
using System.Collections.Generic;
using MiniGame4_1;
using Unity.VisualScripting;
using UnityEngine;

namespace Terbaru{

    public class DayManager : MonoBehaviour
    {
        public MapsManager maps;
        public List<Day> days = new List<Day>();

        public UnityEngine.Video.VideoClip clipsTidur;

        int day = 0;
        void Start(){
            maps.updateDayKonten(days[0].mapsId);
        }

        public void updateDay(){
            day += 1;
            FindObjectOfType<UiManager>().updateEnergy(-3);

            FindObjectOfType<VideoManager>().action(clipsTidur);
            maps.updateDayKonten(days[day].mapsId);
            if(days[day].itemSpawn.Count >= 1){
                foreach(var item in days[day].itemSpawn){
                    item.item.SetActive(true);
                }
            }
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
