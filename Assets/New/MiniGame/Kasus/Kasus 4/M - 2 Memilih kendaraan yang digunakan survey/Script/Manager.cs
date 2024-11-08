using System.Collections;
using System.Collections.Generic;
using Terbaru;
using UnityEngine;


namespace MiniGame4_2{
    public class Manager : MonoBehaviour
    {
        public bool testGame = true;

        public List<Collider2D> cols = new List<Collider2D>();
        
        public string action;

        public void startGame(){
            foreach(var col in cols){
                col.enabled = true;
            }
        }
        public void gameOver(){
            QuestManager.instance.currentQuest.quest.pointBonus += 50;
            QuestManager.instance.CheckAction(action);
        }
    }
}