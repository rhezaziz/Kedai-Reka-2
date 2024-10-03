using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace food
{
    public class FoodSelect : MonoBehaviour
    {
        public food data;
        private void OnMouseDown()
        {
            Game.instance.checkJawaban(data.keyFood);
        }
    }
}