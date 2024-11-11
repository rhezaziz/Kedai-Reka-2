using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


namespace Tapping
{
    public class Manager : MonoBehaviour
    {

        [Header("UI Component")]
        public GameObject ConditionUI;
        public GameObject StartUI;
        public TMPro.TMP_Text Skor, Timer;


        [Header("Value Component")]
        [SerializeField] private List<Noda> noda;
        private float startTime = 30f;


        [Header("Global")]
        bool Playing;
        int currentSkor;
        HashSet<Noda> currentNoda = new HashSet<Noda>();
        private float currentTime;


        public void StartGame(GameObject buttonStart)
        {
            buttonStart.SetActive(false);
            StartUI.SetActive(false);
            ConditionUI.SetActive(true);

            for(int i =  0; i < noda.Count; i++)
            {
                noda[i].setIndex(i);
                noda[i].hideNoda();
            }
            currentNoda.Clear();
            currentTime = startTime;
            Playing = true;
            currentSkor = 0;
            Skor.text = $"{currentSkor}";
        }

        private void Update()
        {
            if (Playing)
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 0)
                {
                    currentTime = 0;
                    GameOver();
                }
                Timer.text = $"{(int)currentTime / 60}:{(int)currentTime % 60:D2}";
                // Check if we need to start any more moles.
                if (currentNoda.Count <= (currentSkor / 10))
                {
                    // Choose a random mole.
                    int index = Random.Range(0, noda.Count);
                    // Doesn't matter if it's already doing something, we'll just try again next frame.
                    if (!currentNoda.Contains(noda[index]))
                    {
                        currentNoda.Add(noda[index]);
                        noda[index].Active(currentSkor/ 10);
                    }
                }
            }

        }


        void GameOver()
        {
            Debug.Log("Permainan Selesai");
            Playing = false;

            foreach(var temp in noda)
            {
                temp.hideNoda();
            }
        }
        public void descreaseTime()
        {
            currentTime -= 0.5f;
        }
        public void addScore(int index)
        {
            currentSkor += 1;
            Skor.text = $"{currentSkor}";
            currentTime += 2f;
            currentNoda.Remove(noda[index]);
        }
    }
    
}
