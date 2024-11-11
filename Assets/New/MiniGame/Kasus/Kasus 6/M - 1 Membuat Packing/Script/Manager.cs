using System.Collections;
using System.Collections.Generic;
using Terbaru;
using UnityEngine;


namespace MiniGame6_1{
    public class Manager : MonoBehaviour
    {

        public string action;
        public List<GameObject> itemInList;


        public void itemInPlace(GameObject item){
            itemInList.Remove(item);

            if(itemInList.Count <= 0){
                StartCoroutine(gameOver());
            }
        }


        IEnumerator gameOver(){
            yield return new WaitForSeconds(2f);
            int point = 50 * itemInList.Count;

            QuestManager.instance.currentQuest.quest.pointBonus += point;

            QuestManager.instance.CheckAction(action);
        }

        public void StartGame(){
            foreach(var item in itemInList){
                item.GetComponent<Collider2D>().enabled = true;
            }
        }
        
    }
}
