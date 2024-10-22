using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGame6_1{
    public class Manager : MonoBehaviour
    {
        public List<GameObject> itemInList;


        public void itemInPlace(GameObject item){
            itemInList.Remove(item);

            if(itemInList.Count <= 0){
                if(testGame)
                    balikMainMenu();
            }
        }

        public bool testGame = true;
        void balikMainMenu(){
            FindObjectOfType<Terbaru.MainMenu>().PindahScene("New Scene");
        }

        void Update(){
            if(Input.GetKeyDown(KeyCode.Escape) && testGame){
                testGame = false;
                balikMainMenu();
            }
            
        }
    }
}
