using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Terbaru;
using UnityEngine;

namespace MiniGame6_4{
    
    public class Manager : MonoBehaviour
    {
        //public Player player;

        public string action;
        public List<items> item = new List<items>();
        public List<Barang> barang;

        public float waktuTunggu;

        public RectTransform panelBarang1, panelBarang2;
        //public listQuest quest;

        public GameObject panelText;
        public GameObject textHiddenObject;
        HashSet<string> textHidden = new HashSet<string>();

        public List<dataItem> data = new List<dataItem>();

        int hiddenObject;

        int Score;

        public void startGame(){
            foreach(var item in data[0].items){
                item.GetComponent<Collider2D>().enabled = true;
            }
        }

        void Start(){
            hiddenObject = data[index].items.Count;
        }

        int index;
        public void checkGame()
        {
            hiddenObject -= 1;
            Score += 50;

            if(hiddenObject <= 0)
            {
                if(index <= data.Count - 1){
                    
                    index += 1;
                    StartCoroutine(gantiBarang());
                    hiddenObject = data[index].items.Count;

                }
                else{
                    Debug.Log("Selesai");
                    QuestManager.instance.currentQuest.quest.pointBonus += Score;
                    QuestManager.instance.CheckAction(action);
                    
                }
                
                // FindObjectOfType<GameOver>().initialGameOver(Score.ToString());
                // quest.miniGame = true;
                // quest.onProses = true;
            }
        }


        IEnumerator gantiBarang(){
            yield return new WaitForSeconds(waktuTunggu);
            panelBarang1.DOPivot(new Vector2(-.1f, .5f), .5f).OnComplete(() =>{
                panelBarang1.DOPivot(new Vector2(1f, .5f), 2f);
                
            });
            yield return new WaitForSeconds(1.5f);
            panelBarang2.DOPivotX(1f, 2f).OnComplete(() =>{
                foreach(var item in data[index].items){
                    item.active = true;
                    panelBarang1.gameObject.SetActive(false);
                }
            });
        }
        // Start is called before the first frame update
        
        // Update is called once per frame
       

        public bool isHiddenObject(GameObject objectKlik)
        {
            //bool isCorrect;
            //// if(textHidden.Contains(objectKlik.name)
            return textHidden.Contains(objectKlik.name);
        }
    }

    [System.Serializable]
    public class dataItem{
        public int Index;
        public List<Barang> items = new List<Barang>();
    }

    [System.Serializable]
    public class items
    {
        public string nama;
        public Sprite gambar;
        public bool isHidden;
        public Transform loct;
        public Barang barang;
    }
}
