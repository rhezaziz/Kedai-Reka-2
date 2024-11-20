using System.Collections;
using System.Collections.Generic;
using Terbaru;
using UnityEngine;


namespace minigame{
    public class Pot : MonoBehaviour
    {
        public List<Collider2D> col = new List<Collider2D>();

        public string action;

        int currentInt;
        
        public void startGame(){
            foreach(var i in col){
                i.enabled = true;
            }
        }




        public void checkGame(){
            currentInt += 1;

            if(currentInt >= col.Count){
                StartCoroutine(count());
                //QuestManager.instance.currentQuest.quest.pointBonus += 50 * col.Count;
                //QuestManager.instance.CheckActionQuest(action);
            }
        }

        IEnumerator count(){
            yield return new WaitForSeconds(2f);
            FindObjectOfType<MiniGame>().kembaliMiniGame("Tanaman");
        }
    }
}
