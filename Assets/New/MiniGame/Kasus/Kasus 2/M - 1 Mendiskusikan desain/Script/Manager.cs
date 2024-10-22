using System.Collections;
using System.Collections.Generic;
using Terbaru;
using UnityEngine;


namespace MiniGame2_1{

    public class Manager : MonoBehaviour
    {
        public string action;
        public void checkAction(){
            if(testGame)
                FindObjectOfType<QuestManager>().CheckAction(action);
            else
                balikMainMenu();
        }

        public void checkJawaban(bool jawaban){
            if(jawaban)
                Debug.Log("Benar");
                //Benar

            checkAction();
        }

        public bool testGame = true;
        void balikMainMenu(){
            FindObjectOfType<MainMenu>().PindahScene("New Scene");
        }

        void Update(){
            if(Input.GetKeyDown(KeyCode.Escape) && testGame){
                testGame = false;
                balikMainMenu();
            }
            
        }
    }
}