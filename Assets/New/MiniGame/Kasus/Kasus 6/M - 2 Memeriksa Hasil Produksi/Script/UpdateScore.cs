using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace food
{
    public class UpdateScore : MonoBehaviour
    {
        public TMPro.TMP_Text score;

        int currentScore;

        public void addScore(int value)
        {
            currentScore += value;

            score.text = currentScore.ToString();
        }
    }
}