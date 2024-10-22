using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Terbaru;
using UnityEngine;


namespace MiniGame3_1{
    public class Manager : MonoBehaviour
    {
        public int jumlah;
        int jumlahOrang;
        public Material grey;

        

        float jumlahRating;


        [Header("UI rating"  )]
        public GameObject panelRating;
        //public UnityEngine.UI.Image ratingUi;
        public GameObject btnKembali;
        public void selesai(UnityEngine.UI.Image sprite, float rating){
            //jumlahRating += rating;
            sprite.material = grey;
            jumlahOrang += 1;
            if(jumlah <= jumlahOrang){
                StartCoroutine(GameOver());
            }
        }

        IEnumerator GameOver(){
            yield return new WaitForSeconds(5f);
            float rating = jumlahRating / jumlah;

            panelRating.SetActive(true);

            yield return new WaitForSeconds(1f);
            //ratingUi.DOFillAmount(rating, 1.5f);
            yield return new WaitForSeconds(2f);
            btnKembali.SetActive(true);
        }
        // Start is called before the first frame update
        void Start()
        {
            btnKembali.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>{
                if(testGame)
                    balikMainMenu();
            });
        }

        public bool testGame = true;
        void balikMainMenu(){
            FindObjectOfType<MainMenu>().PindahScene("New Scene");
        }

        void Update(){
            if(Input.GetKeyDown(KeyCode.Escape) && testGame){
                testGame = false;
                balikMainMenu();
            }
            
        }
    }
}