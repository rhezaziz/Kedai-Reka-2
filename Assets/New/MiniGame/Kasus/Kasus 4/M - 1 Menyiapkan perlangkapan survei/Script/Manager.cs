using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Terbaru;

namespace MiniGame4_1{
    public class Manager : MonoBehaviour
    {
        public List<items> listItem = new List<items>();
        public Image parent;
        public RectTransform itemUI;
        public TMPro.TMP_Text jumlahText;

        public string action;

        public void startKonten(){
            var min = itemUI.anchorMin;
            var max = itemUI.anchorMax;

            foreach(var item in listItem){
                item.loct.DOColor(new Color(1f,1f,1f,0f),1f);
            }
            itemUI.DOAnchorMin(new Vector2(min.x, 1f), 1f);
            itemUI.DOAnchorMax(new Vector2(max.x, 1f), 1f);
            itemUI.DOScale(Vector3.one * 3 / 4, 1f).OnComplete(() =>{
                Invoke("initKonten", 1f);
            }); 

            // foreach(var item in listItem){
            //     GameObject temp = Instantiate(prefabText);
            //     temp.SetActive(true);
            //     Vector3 rotation = prefabText.transform.localEulerAngles;
            //     temp.transform.SetParent(parent);
            //     temp.transform.localScale = Vector3.one;
            //     temp.transform.localEulerAngles = rotation;
            //     temp.GetComponent<Text>().text = item.nama;
            //     item.loct = temp.transform;
            //     item.ObjectItem.GetComponent<SelectedObject>().setItemsValue(item.loct);
            // }
        }

        // Start is called before the first frame update
        void Start()
        {
           // Invoke("startKonten", 2f);
        }

        void jumlahTerpilih(){
            //Debug.Log(jumlahText.text);
            string[] text = jumlahText.text.Split('/');
            //Debug.Log(text[0]);
            int jml = int.Parse(text[0]);
            jumlahText.text = $"{jml + 1}/{listItem.Count}";

            if(jml >= listItem.Count - 1){
                QuestManager.instance.CheckAction(action);
                Debug.Log("Selesai");
            }
        }

        public void itemOnClick(SpriteRenderer item){

            QuestManager.instance.currentQuest.quest.pointBonus += 50;
            var itemData = getItem(item);
            //Debug.Log(itemData.nama);
            Vector2 dir = itemData.loct.transform.position;
            item.sortingOrder = 11;
            item.transform.DOMove(dir, 1f);
            item.transform.DOScale(Vector3.one * itemData.size, 1f);
            item.transform.DORotate(itemData.rot,1f).OnComplete(() => {
                itemData.loct.DOColor(new Color(1f,1f,1f,1f), .5f);
                item.gameObject.SetActive(false);
                jumlahTerpilih();

                
            });
        }

        items getItem(SpriteRenderer itemSelected){
            foreach(var item in listItem){
                if(item.ObjectItem == itemSelected){
                    return item;
                }
            }
            return null;
        }


        void initKonten(){
            parent.enabled = false;
            foreach(var item in listItem){
                item.ObjectItem.GetComponent<Collider2D>().enabled = true;
            }
            jumlahText.text = $"0/{listItem.Count}";
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }

    [System.Serializable]
    public class items
    {
        public string nama;
        public SpriteRenderer ObjectItem;
        public Image loct;
        public float size;
        public Vector3 rot;
    }
}
