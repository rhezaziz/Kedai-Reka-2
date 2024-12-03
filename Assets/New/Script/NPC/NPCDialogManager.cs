using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terbaru{

    public class NPCDialogManager : MonoBehaviour
    {

        public List<Npc_Dialog_Data> dialogData = new List<Npc_Dialog_Data>();
        public Transform NPCparent;
        public List<Transform> npcs = new List<Transform>();

        // public void setNPCDialog(int index){
        //     for(int i = 0; i < NPCparent.childCount; i++){
        //         NPCparent
        //     }
        // }
        public void initAddList(){

        }


        public Dialog tempDialog(int index, Nama nama){
            foreach(var data in dialogData){
                if(data.nama == nama){
                    return data.dialog[index];
                }
            }

            return null;
        }

        public void getAlex(int index){
            for(int i = 0; i < NPCparent.childCount; i++){
                if(NPCparent.GetChild(i).name == "Alex"){
                    NPCparent.GetChild(i).GetComponent<NPC_Dialog>().SetindexDialog(index);
                    return;
                }
            }
        }

        public void getDinda(int index)
        {
            for (int i = 0; i < NPCparent.childCount; i++)
            {
                if (NPCparent.GetChild(i).name == "Dinda")
                {
                    NPCparent.GetChild(i).GetComponent<NPC_Dialog>().SetindexDialog(index);
                    return;
                }
            }
        }
    }


    [System.Serializable]
    public class Npc_Dialog_Data{
        public Nama nama;
        public List<Dialog> dialog = new List<Dialog>();
    }
}
