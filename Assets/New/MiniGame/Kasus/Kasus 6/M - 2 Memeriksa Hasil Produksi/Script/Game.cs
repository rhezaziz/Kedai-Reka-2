using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace food
{
    public class Game : MonoBehaviour
    {
        public static Game instance;

        public Transform PersonTransform;
        public List<food> foods = new List<food>();
        public List<FoodSelect> selectionFood = new List<FoodSelect>();
        public Person person;
        public UpdateScore score;

        private void Awake()
        {
            instance = this;

            init();
        }

        void init()
        {
            for(int i = 0; i < selectionFood.Count; i++)
            {
                selectionFood[i].data = foods[i];
                selectionFood[i].GetComponent<SpriteRenderer>().sprite = foods[i].foodSprite;
                selectionFood[i].name = foods[i].NamaFood;
            }

            Invoke("pesananDatang", .5f);
        }

        public void pesananDatang()
        {
            
            PersonTransform.gameObject.SetActive(true);

            int randomIndex = Random.Range(0, foods.Count - 1 );
            person.bar.fillAmount = 1;
            person.pesan.sprite = foods[randomIndex].foodSprite;
            person.keyPesanan = foods[randomIndex].keyFood;

            StartCoroutine(startTimer(person.timer, person.bar));
        }

        IEnumerator startTimer(float timer, UnityEngine.UI.Image bar)
        {
            Debug.Log(timer);
            float temp = timer;
            while(bar.fillAmount >= 0)
            {
                temp -= 0.01f;
                bar.fillAmount = temp / timer;
                yield return new WaitForSeconds(0.01f);
            }
            disapearPerson();
        }

        public void checkJawaban(string key)
        {
            if(key == person.keyPesanan)
            {
                score.addScore(10);
                disapearPerson();
                StopAllCoroutines();
            }
        }
        void disapearPerson()
        {
            Debug.Log("Hilang");
            PersonTransform.gameObject.SetActive(false);
            Invoke("pesananDatang", 2f);
        }
    }

    [System.Serializable]
    public class Person
    {
        public UnityEngine.UI.Image bar;
        public SpriteRenderer pesan;
        public string keyPesanan;
        public float timer;
    }


    [System.Serializable]
    public class food
    {
        public string NamaFood;
        public string keyFood;
        public Sprite foodSprite;
    }
}