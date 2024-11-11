using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Terbaru;


namespace MiniGame1_3{
    
    public class Manager : MonoBehaviour
    {

        public bool DailyQuest;
        public List<UnityEngine.UI.Button> pilihan;

        public Sprite Wrong, Correct;

        public Quest quest;

        public bool test;

        public string action;
        

        public List<dataAnimatedButton> animated = new List<dataAnimatedButton>();

        UnityEngine.UI.Image valueResult;
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
            FindObjectOfType<MainMenu>().PindahScene("New Scene");
        }

        void Update(){
            // if(Input.GetKeyDown(KeyCode.Escape) && testGame){
            //     testGame = false;
            //     balikMainMenu();
            // }
            
        }

        void hasil(bool value){
            test = false;
            clickedBTN.interactable = false;
            Sprite tempSprite = value ? Correct : Wrong;
            Transform result = clickedBTN.transform.GetChild(0);
            result.GetComponent<UnityEngine.UI.Image>().sprite = tempSprite;
            result.DOScale(Vector2.one * 2.2f, 1f).OnComplete(() => {
                result.DOScale(Vector2.one * 2f, .5f).OnComplete(() =>{
                    if(!DailyQuest){
                        QuestManager.instance.CheckAction(action);
                        quest.pointBonus += value ? 50 : 0;
                    }else{
                        QuestManager.instance.CheckActionQuest(action);
                    }
                });
            });
             

            // if(testGame)
            //     balikMainMenu();

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
