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
        public Button check, resetBtn;

        [System.Serializable]
        public class dataButton{
            public Button onSeletect;
            public TMP_Text txt;
            public bool value;
            public Image result;
        }

        public List<Clicked> clicks = new List<Clicked>();
        public List<Clicked> clicked = new List<Clicked>();

        public Sprite wrong, correct;


        public List<dataButton> buttons = new List<dataButton>();

        dataButton onSelected;
        bool checkJml = true;

        void Update()
        {
            if (clicked.Count > 0 && checkJml)
            {
                resetBtn.interactable = true;
                checkJml = false;
            }
        }

        public void startGame()
        {
            resetGame();
        }
        public void checkHasil()
        {
            check.interactable = false;
            resetBtn.interactable = false;
            foreach (var click in clicks)
            {
                click.thisObj.interactable = false;

            }
            StartCoroutine(animCheckHasil());
        }

        IEnumerator animCheckHasil()
        {
            foreach (var click in clicks)
            {
                bool value = click.value;
                bool onSelected = clicked.Contains(click);

                bool hasil = value && !onSelected || !value && onSelected;
                Sprite temp = hasil ? correct : wrong;
                click.checkObj(temp);

                int point = hasil ? 50 : 0;

                QuestManager.instance.currentQuest.quest.pointBonus += point;
                

                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(1f);
            QuestManager.instance.CheckAction(action);
        }


        public void resetGame()
        {
            checkJml = true;
            check.interactable = true;
            resetBtn.interactable = false;
            foreach (var click in clicks)
            {
                click.resetObj();
            }
            clicked.Clear();
        }

        #region old Version
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

        #endregion
    }
}
