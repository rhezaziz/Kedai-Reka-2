using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Terbaru{

    public class DayManager : MonoBehaviour
    {
        public MapsManager maps;
        public List<Day> days = new List<Day>();
        void Start(){
            maps.updateDayKonten(days[0].mapsId);
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
        public Transform posisiItem;
    }
}
