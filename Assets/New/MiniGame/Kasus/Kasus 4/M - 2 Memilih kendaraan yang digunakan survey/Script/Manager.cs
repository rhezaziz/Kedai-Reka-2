using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGame4_2{
    public class Manager : MonoBehaviour
    {
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

        public void gameOver(){
            if(testGame)
                balikMainMenu();
        }
    }
}