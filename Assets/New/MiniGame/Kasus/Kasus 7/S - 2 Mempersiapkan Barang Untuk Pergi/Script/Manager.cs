using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using Terbaru;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace MiniGame7_4{

    public class Manager : MonoBehaviour
    {
        public string action;
        public List<dataContent> datas = new List<dataContent>();
        List<ui_Component> components = new List<ui_Component>();
        public Color selectedBtn, unSelectedBtn;
        public Sprite Correct, Wrong;
        public Button checkBtn;

        public int jumlah;

        public RectTransform panel;
        public Image background; 
        public Image panelTimer;
        public float timer;
        // void Start()
        // {
        //     initStart();
        // }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void initStart(){
            
            panelTimer.DOFillAmount(0f, timer).OnComplete(() =>{
                panel.DOPivotY(-.1f, .5f).OnComplete(() =>{
                    panel.DOPivotY(1f, .5f);
                    Color color = background.color;
                    background.DOColor(new Color(color.r, color.g, color.b, 0f), .5f).OnComplete(() =>{
                        foreach(var item in datas){
                            item.clickable.transform.DORotate(Vector3.zero, 1f).OnComplete(() =>{
                                item.clickable.interactable = true;
                                background.gameObject.SetActive(false);
                            });
                        }
                    });
                });
            });

        }
        List<dataContent> valueSelected = new List<dataContent>();
        public void SelectItem(Button click){
            var item = data(click);
            // ui_Component component = new ui_Component(click.transform.GetChild(1).GetComponent<TMPro.TMP_Text>(),
            //                 click.transform.GetChild(0).GetComponent<Image>());
            
            ui_Component component = new ui_Component{
                textIndex = click.transform.GetChild(1).GetComponent<TMPro.TMP_Text>(),
                status = click.transform.GetChild(0).GetComponent<Image>()
            };
            //Debug.Log(components.Contains(component));
            if(!components.Contains(component)){
                valueSelected.Add(item);
                components.Add(component);  
                //Debug.Log(components.Count);
               // components[components.Count - 1].textIndex.text = $"{components.Count}";
                click.GetComponent<Image>().color =  selectedBtn;   
            }
            else{
                click.GetComponent<Image>().color =  unSelectedBtn;
                valueSelected.Remove(item);
                components.Remove(component);  
                //component.textIndex.text = $""; 
                int index = 1;
                foreach(var ui in components){
                    //ui.textIndex.text = $"{index}";
                    
                    index += 1;
                }
            }
            checkJumlah();
        }
        bool done;
        void checkJumlah(){
            if(!(jumlah <= valueSelected.Count)){
                if(done){
                    if(!(jumlah <= valueSelected.Count)){
                        foreach(var firstList in datas){
                            firstList.clickable.enabled = true;
                        }
                    }
                    done = false;
                    checkBtn.interactable = false;
                }
                return;
            }

            done = true;
            checkBtn.interactable = true;
            foreach(var firstList in datas){
                firstList.clickable.enabled = false;
                foreach(var secondList in valueSelected){
                    if(firstList == secondList){
                        secondList.clickable.enabled = true;
                        break;
                    }
                }
            }
        }

        dataContent data(Button btn){
            foreach(var _data in datas){
                if(_data.clickable == btn)
                    return _data;
            }
            return null;
        }

        public void checkValue(){
            StartCoroutine(check());
        }

        IEnumerator check(){
            checkBtn.interactable = false;
            foreach(var x in valueSelected){
                //x.clickable.GetComponent<Image>().color = unSelectedBtn;
                x.clickable.enabled = false;
            }

            foreach(var first in datas){
                foreach(var second in valueSelected){
                    if(first == second){
                        Sprite tempValue = second.value ? Correct : Wrong;
                        Image status = second.clickable.transform.GetChild(0).GetComponent<Image>(); 
                        //int index = second.GetHashCode();
                        status.sprite = tempValue;
                        int point = tempValue ? 50 : 0;
                        QuestManager.instance.currentQuest.quest.pointBonus += point;
                        Transform component = status.transform;
                        component.DOScale(Vector3.one * 1.2f, 1.2f).OnComplete(() =>{
                        component.DOScale(Vector3.one, .2f).OnComplete(()=>{
                            
                        
                            });
                        });
                        yield return new WaitForSeconds(1f);
                    }
                }
            }
            yield return new WaitForSeconds(1f);
            QuestManager.instance.CheckAction(action);
            // for(int i = 0; i < valueSelected.Count; i++){
            //     // valueSelected[i].clickable.GetComponent<Image>().color = unSelectedBtn;
            //     Sprite tempValue = valueSelected[i].value ? Correct : Wrong;

            //     // valueSelected[i].clickable.enabled = false;
            //     // components[i].textIndex.transform.DOScale(Vector2.zero, .25f).OnComplete(() => {
            //     //     components[i].textIndex.text = "";
                
            //     // });
            //     components[i].status.sprite = tempValue;

            //     Transform component = components[i].status.transform;
            //     component.DOScale(Vector3.one * .75f, .75f).OnComplete(() =>{
            //         component.DOScale(Vector3.one, .2f).OnComplete(()=>{

                        
            //         });
            //     });

            //     yield return new WaitForSeconds(1.5f);
            //}
            
        }

        [System.Serializable]
        public class dataContent{
            public string Nama;
            public Button clickable;
            public bool value;

            public override bool Equals(object obj)
            {
                if (obj is dataContent other)
                {
                    return this.Nama == other.Nama;
                }
                return false;
                }

                // Override GetHashCode, diperlukan saat mengoverride Equals
                public override int GetHashCode()
                {
                    return Nama.GetHashCode();
                }
            }
        }

        [System.Serializable]
        public class ui_Component{
            public TMPro.TMP_Text textIndex { get; set; }
            public Image status { get; set; }

            // public ui_Component(TMPro.TMP_Text textIndex, Image status){
            //     this.textIndex = textIndex;
            //     this.status = status;
            // }

            public override bool Equals(object obj)
            {
                if (obj is ui_Component other)
                {
                    return this.textIndex == other.textIndex && this.status == other.status;
                }
                return false;
            }

                // Override GetHashCode, diperlukan saat mengoverride Equals
            public override int GetHashCode()
            {
                return textIndex.GetHashCode() ^ status.GetHashCode();
            }
            
        }
    }

