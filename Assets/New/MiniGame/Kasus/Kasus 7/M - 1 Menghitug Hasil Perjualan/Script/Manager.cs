using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Terbaru;

namespace MiniGame7_1{
    public class Manager : MonoBehaviour
    {

        public List<UnityEngine.UI.Button> pilihan;

        public UnityEngine.UI.Image pilihanBenar;

        public Color benar, salah;

        public bool test;

        public string action;

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

        

        void hasil(bool value){
            if(value){
                clickedBTN.GetComponent<UnityEngine.UI.Image>().DOColor(benar, 1f);
                 QuestManager.instance.currentQuest.quest.pointBonus += 50;
            }
            else{
                clickedBTN.GetComponent<UnityEngine.UI.Image>().DOColor(salah,1f);
                pilihanBenar.GetComponent<UnityEngine.UI.Button>().interactable = true;
                pilihanBenar.GetComponent<UnityEngine.UI.Button>().enabled = false;
                pilihanBenar.DOColor(new Color(benar.r, benar.g, benar.b, 1f),1f);
            }

            Invoke("Action", 2f);

        }

        void Action(){
            //Debug.Log("Selesai");
             QuestManager.instance.CheckAction(action);
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
