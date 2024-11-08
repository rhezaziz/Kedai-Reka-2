using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using food;
using Terbaru;
using UnityEngine;


namespace MiniGame4_3
{
    public class Manager : MonoBehaviour
    {
        // Start is called before the first frame update

        public Sprite Wrong , Correct;

        public bool DailyQuest;

        public List<lokasi> datas = new List<lokasi>();

        public Color correctWay;
        public Color wrongWay;

        public int sortingLayer;
        public void StartGame()
        {
            foreach(var mobil in datas){
                mobil.Mobil.GetComponent<Collider2D>().enabled = true;
            }
        }

        // Update is called once per frame
        

        public void onClickCar(GameObject mobil){
            Debug.Log(mobil.name);
            foreach(var data in datas){
                data.Mobil.GetComponent<Collider2D>().enabled = false;
                data.Mobil.color = new Color(1f,1f,1f, .5f);
                data.tempat.color = new Color(1f,1f,1f, 0.5f);
                if(data.Mobil.gameObject == mobil){
                    data.Mobil.color = new Color(1f,1f,1f,1f);
                    data.tempat.color = new Color(1f,1f,1f,1f);
                    data.Jalur.sortingOrder = sortingLayer;
                    data.Jalur.color = data.thisWay ? correctWay : wrongWay;
                }
            }
        }

        
        public string action;
        public void GameOver(int value){
            int point = datas[value].thisWay ? 50 : 0;
            Sprite result = datas[value].thisWay ? Correct : Wrong;
            
            datas[value].result.sprite = result;
            Transform resultValue = datas[value].result.transform;

            resultValue.DOScale(Vector2.one * .4f, 1f).OnComplete(() =>{
                if(!DailyQuest){
                    QuestManager.instance.currentQuest.quest.pointBonus += point;
                    QuestManager.instance.CheckAction(action);
                }
                    

                else
                    QuestManager.instance.CheckActionQuest(action);
            });
        }

        

        [System.Serializable]
        public class lokasi{
            public SpriteRenderer Mobil;
            public SpriteRenderer tempat;
            public SpriteRenderer Jalur;

            public SpriteRenderer result;
            public bool thisWay;

        }
    }
}

