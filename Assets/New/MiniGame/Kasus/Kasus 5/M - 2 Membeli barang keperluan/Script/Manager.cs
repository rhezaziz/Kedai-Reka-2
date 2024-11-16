using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Terbaru;
using UnityEngine;


namespace MiniGame5_2{    
    public class Manager : MonoBehaviour
    {
        // Start is called before the first frame update
        public List<itemShop> items = new List<itemShop>();
        public List<GameObject> itemsOnRak = new List<GameObject>();
        public GameObject prefabsText;
        public Transform parent;
        public Transform trolli;
        public string action;


        public bool isDaily;
        void Start()
        {
            //initKonten();
        }

        

        public void initKonten(){
            for(int i = 0; i < itemsOnRak.Count; i++){
                itemsOnRak[i].GetComponent<Collider2D>().enabled = true;
                itemsOnRak[i].GetComponent<SelectedItem>().initialItem(trolli.gameObject);
            }

            foreach(var item in items){
                GameObject text = Instantiate(prefabsText);
                text.transform.SetParent(parent);
                text.SetActive(true);
                text.transform.localScale = Vector2.one;
                Vector3 rot = prefabsText.transform.localEulerAngles;
                //Debug.Log($"rot Prefabs : {rot}");
                //Debug.Log($"rot Sebelum {text.transform.localEulerAngles}");
                
                text.transform.localEulerAngles = rot;
                //Debug.Log($"rot Setelah {text.transform.localEulerAngles}");
                text.GetComponent<UnityEngine.UI.Text>().text = item.namaItem;
                item.itemOnTrolli.SetActive(false);
                item.itemOnRak.GetComponent<SelectedItem>().extendAction.AddListener(() =>
                    check(item, text)    
                );
            }
        }
        int jumlah = 0;
        public void check(itemShop item, GameObject text){
            item.itemOnTrolli.SetActive(true);
            item.itemOnRak.SetActive(false);
            Transform garis = text.transform.GetChild(0);

            garis.DOScaleX(1f, 1f);
            items.Remove(item);
            jumlah += 1;

            gameOver();

        }

        void gameOver(){
            if(items.Count <= 0){
                Invoke("checkAction", 2f);
            }
        }


        public void checkAction(){
            if (!isDaily)
            {
                QuestManager.instance.currentQuest.quest.pointBonus += jumlah * 50;
                QuestManager.instance.CheckAction(action);
            }

            else
            {
                QuestManager.instance.CheckActionQuest(action);
            }
        }

       
    }

    [System.Serializable]
    public class itemShop{
        public string namaItem;
        public type _type;
        public GameObject itemOnTrolli;

        public GameObject itemOnRak;
        
    }

    public enum type{
        Ayam,
        Minyak,
        Bubuk,
        Royco,
        Tepung
    }
}