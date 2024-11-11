using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Terbaru;
using UnityEngine;
using UnityEngine.UI;


namespace MiniGame2_1{

    public class Manager : MonoBehaviour
    {
        public string action;

        public Sprite Wrong, Correct;

        public List<Button> buttons = new List<Button>();
        public void checkAction(){
            
            FindObjectOfType<QuestManager>().CheckAction(action);

        }
        Button temp;
        public void clicked(Button btn){
            temp = btn;
        }

        public void checkJawaban(bool jawaban){
            int poins = jawaban ? 50 : 0;
            QuestManager.instance.currentQuest.quest.pointBonus += poins;

            foreach(var btn in buttons){
                btn.interactable = false;
            }

            Transform result = temp.transform;
            result.DOScale(Vector2.one * .85f, 1f).OnComplete(() =>{
                result.GetComponentInChildren<Image>().DOColor(new Color(1f,1f,1f,1f), 1f).OnComplete(() => {
                    checkAction();
                });
            });
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