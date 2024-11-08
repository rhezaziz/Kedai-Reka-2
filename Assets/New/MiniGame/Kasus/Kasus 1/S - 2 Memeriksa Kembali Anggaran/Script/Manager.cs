using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Terbaru;

namespace MiniGame1_4{
    public class Manager : MonoBehaviour
    {
        public Button check;

        [System.Serializable]
        public class dataButton{
            public Button onSeletect;
            public TMP_Text txt;
            public bool value;
            public Image result;
        }

        public Sprite wrong, correct;


        public List<dataButton> buttons = new List<dataButton>();

        dataButton onSelected;
        public void select(int value){
            int temp = value == 0 ? 1 : 0;
            onSelected = buttons[value];
            if(!buttons[temp].onSeletect.interactable){
                buttons[temp].txt.DOColor(new Color(1,0f,0f, 0f), .25f).OnComplete(() =>{
                    buttons[temp].txt.transform.localScale = Vector2.zero;
                    buttons[temp].txt.color = new Color(1f,0f,0f,1f);
                    buttons[temp].onSeletect.interactable = true;

                });
            }else if(!buttons[value].onSeletect.interactable){
                temp = value;
                buttons[temp].txt.DOColor(new Color(1,0f,0f, 0f), .25f).OnComplete(() =>{
                    buttons[temp].txt.transform.localScale = Vector2.zero;
                    buttons[temp].txt.color = new Color(1f,0f,0f,1f);
                    buttons[temp].onSeletect.interactable = true;

                });
                check.interactable = false;
                return;
            }


            buttons[value].onSeletect.interactable = false;
            buttons[value].txt.transform.localScale = Vector2.one * 1.5f;
            buttons[value].txt.transform.DOScale(Vector2.one, .55f);

            check.interactable = true;

            
        }
        public string action;
        public void checkResult(){
            check.interactable = false;
            foreach(var i in buttons)
                i.onSeletect.interactable = false;
            
            int point = onSelected.value ? 50 : 0;
            var result = onSelected.result.transform;
            onSelected.result.sprite = onSelected.value ? correct : wrong;

            Sequence anim = DOTween.Sequence();
            
            result.DOScale(Vector2.one * 1.2f, 1f).OnComplete(() =>{
                result.DOScale(Vector2.one , .35f).OnComplete(() =>{
                    result.DOScale(.85f, .25f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
                    QuestManager.instance.currentQuest.quest.pointBonus += point;
                    QuestManager.instance.CheckAction(action);
                    // anim.Append(result.DOScale(Vector2.one * .9f, .1f));
                    // anim.Append(result.DOScale(Vector2.one, .1f));
                    // anim.SetLoops(-1);
                });
            });

        }
    }
}
