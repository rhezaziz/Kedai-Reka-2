
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGame5_6{

    public class Manager : MonoBehaviour
    {

        public List<Placement> places;
        public List<dataObj> objs = new List<dataObj>();
        public GameObject tempPos;

        int benar, salah;

        public void itemOnDrag(GameObject obj){
            GameObject temp = objPlacement(obj);

            foreach(var place in places){
                place.temp = obj;
                place.obj = temp;                
            }
        }

        GameObject objPlacement(GameObject temp){
            foreach(var obj in objs){
                if(obj.itemDrag == temp){
                    return obj.itemPlacement;
                }

                
            }
            return null;
        }

        public void checkJawaban(bool value){
            if(value)
                benar++;
            else
                salah++;

            Debug.Log($"Jumlah Benar : {benar} Jumlah Salah : {salah}");
        }

    

        public bool testGame = true;
        void balikMainMenu(){
            FindObjectOfType<Terbaru.MainMenu>().PindahScene("New Scene");
        }

        void Update(){
            if(Input.GetKeyDown(KeyCode.Escape) && testGame){
                testGame = false;
                balikMainMenu();
            }
            
        }


        
    }

    [System.Serializable]
        public class dataObj{
            public GameObject itemDrag;
            public GameObject itemPlacement;

            public Takaran takaran;
            
        }

        public enum Takaran{
            Air,
            Minyak,
            PenyedapRasa,
            Tepung
        }
}
