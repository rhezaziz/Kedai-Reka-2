
using System.Collections;
using System.Collections.Generic;
using Terbaru;
using UnityEngine;


namespace MiniGame5_6{

    public class Manager : MonoBehaviour
    {
        public Takaran temp;

        public Sprite Wrong, Correct;
        public List<Placement> places;
        public List<dataObj> objs = new List<dataObj>();
        public GameObject tempPos;

        public GameObject checkPanel;

        int benar, salah;

        public void itemOnDrag(GameObject obj){
            GameObject temp = objPlacement(obj);

            foreach(var place in places){
                place.temp = obj;
                place.obj = temp;                
            }
        }

        public void gameStart(){
            foreach(var obj in objs){
                obj.itemDrag.GetComponent<Collider2D>().enabled = true;
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
        public int jumlah;
        public void checkJawaban(bool value){
            jumlah += 1;
            if(value)
                benar++;
            else
                salah++;

            if(jumlah >= places.Count){
                checkPanel.SetActive(true);
            }
            //Debug.Log($"Jumlah Benar : {benar} Jumlah Salah : {salah}");
        }

    

        public void Check(){
            checkPanel.SetActive(false);
            StartCoroutine(checkHasil());
        }

        public void Reset(){
            jumlah = 0;
            benar = 0;
            salah = 0;
            checkPanel.SetActive(false);
            foreach(var place in places){
                place.resetGame();
            }
        }
        public string Action;
        IEnumerator checkHasil(){
            foreach(var place in places){
                Sprite temp = place.hasil ? Correct : Wrong;
                place.checkHasil(temp);
                yield return new WaitForSeconds(1.5f);
            }

            yield return new WaitForSeconds(1f);
            QuestManager.instance.currentQuest.quest.pointBonus += (benar * 50);
            QuestManager.instance.CheckAction(Action);
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
            Tepung,
            None
        }
}
