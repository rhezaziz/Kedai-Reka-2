using System.Collections;
using System.Collections.Generic;
using Terbaru;
using UnityEngine;


namespace Hari10{

    public class Manager : MonoBehaviour
    {
        public GameObject panelGameOver;

        public string action;

        public List<Collider2D> col = new List<Collider2D>();
        // Start is called before the first frame update
        public void GameOver(){
            foreach(var i in col){
                i.enabled = false;
            }
        }

        public void check(){
            QuestManager.instance.CheckActionQuest(action);
        }

        public void StartGame(){
            foreach(var i in col){
                i.enabled = true;
            }
        }
    }
}
