using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGame4_4{
    public class Manager : MonoBehaviour
    {

        public List<dataSoal> soals = new List<dataSoal>();

        public GameObject prefabPertanyaan;
        public Transform parent;

        public void nextPertanyaan(){
            if(soals.Count > 0){
                prefabPertanyaan.GetComponent<DragObject>().spawn(soals[0]);
                soals.RemoveAt(0);
            }else{
                if(testGame)
                    balikMainMenu();
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            nextPertanyaan();
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

    [System.Serializable]
    public class dataSoal{
        public bool value;
        public string Pertanyaan;
    }
}