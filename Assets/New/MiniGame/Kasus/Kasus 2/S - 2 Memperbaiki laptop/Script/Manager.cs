using System.Collections;
using System.Collections.Generic;
using Terbaru;
using UnityEngine;

namespace MiniGame2_4{
    public class Manager : MonoBehaviour
    {
        public string jawaban;

        public void checkJawaban(string value){
            if(value == jawaban){
                Debug.Log("Benar");
            }

            else{
                Debug.Log("Salah");
            }

            if(testGame)
                balikMainMenu();
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