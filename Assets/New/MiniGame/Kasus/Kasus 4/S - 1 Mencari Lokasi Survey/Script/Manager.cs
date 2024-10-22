using System.Collections;
using System.Collections.Generic;
using food;
using UnityEngine;


namespace MiniGame4_3
{
    public class Manager : MonoBehaviour
    {
        // Start is called before the first frame update

        public List<lokasi> datas = new List<lokasi>();

        public Color correctWay;
        public Color wrongWay;

        public int sortingLayer;
        void Start()
        {
            
        }

        // Update is called once per frame
        

        public void onClickCar(GameObject mobil){
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

        public void GameOver(){
            if(testGame)
                balikMainMenu();
        }

        [System.Serializable]
        public class lokasi{
            public SpriteRenderer Mobil;
            public SpriteRenderer tempat;
            public SpriteRenderer Jalur;
            public bool thisWay;

        }
    }
}

