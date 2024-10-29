using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru{
    public class RPG_Manager : MonoBehaviour
    {
        public List<mapsOpen> maps = new List<mapsOpen>();
        public MapsManager map;

        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void initRPG(int index){
            map.openMaps(maps[index].id_maps);

            foreach(var npc in maps[index].npcs){
                npc.interactNPC(true);
            }
        }

        public void endRPG(int index){
            map.closeMaps();
            foreach(var npc in maps[index].npcs){
                npc.interactNPC(false);
            }
        }


        [System.Serializable]
        public class mapsOpen{
            public string nama;
            public List<id_Maps> id_maps= new List<id_Maps>();
            public List<DialogObjectClick> npcs = new List<DialogObjectClick>();
        }
    }

}