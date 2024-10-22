using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MiniGame7_1{
    public class Manager : MonoBehaviour
    {

        public List<UnityEngine.UI.Button> pilihan;

        public bool test;

        public List<dataAnimatedButton> animated = new List<dataAnimatedButton>();
        public void checkJawaban(bool value){
            // if(!clickedBTN.GetComponent<UnityEngine.UI.Button>().interactable)
            //     return;

            // foreach(var btn in pilihan){
            //     btn.interactable = false;
            // }
            
            if(!clickedBTN.interactable || !clickedBTN.IsActive() && !test)
                return;

            

            foreach(var btn in pilihan){
                btn.interactable = false;
            }
            Debug.Log($"Before : {clickedBTN.IsActive()}");
            clickedBTN.enabled = false;
            Debug.Log($"After : {clickedBTN.IsActive()}");

            int index = 0;
            
            clickedBTN.transform.DOScale(animated[index].size, animated[index].timer).OnComplete(() =>{
                index += 1;
                clickedBTN.transform.DOScale(animated[index].size, animated[index].timer).OnComplete(() =>{
                    index += 1;
                    clickedBTN.transform.DOScale(animated[index].size, animated[index].timer).OnComplete(() =>{
                        hasil(value);
                    });
                });
            });
            //clickedBTN
        }

        UnityEngine.UI.Button clickedBTN;
        public void mouseClick(UnityEngine.UI.Button temp){
            
            


            clickedBTN = temp;

            


            if(temp.GetComponent<UnityEngine.UI.Button>().interactable && temp.IsActive() || test){
                         
                temp.transform.DOScale(new Vector2(.75f, .75f), .5f);
                pilihan.Remove(temp.GetComponent<UnityEngine.UI.Button>());

            }   
            
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

        void hasil(bool value){
            if(testGame)
                balikMainMenu();

        }

        // public void mouseUp(){
        //     foreach
        // }

        [System.Serializable]
        public class dataAnimatedButton{
            public float timer;
            public Vector2 size;
        }
    }
}
