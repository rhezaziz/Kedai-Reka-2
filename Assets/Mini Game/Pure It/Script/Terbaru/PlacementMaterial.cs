using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terbaru
{
    public class PlacementMaterial : MonoBehaviour
    {
        public Transform temp;

        public bool isEmpty;

        public Sprite[] materials;

        SpriteRenderer area, sprite;

        public int valueCorrect;

        private void Start()
        {
            sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
            area = GetComponent<SpriteRenderer>();
        }

        public void closesDistance()
        {
            if (isEmpty)
            {
                Color color = Color.white;
                area.color = new Color(color.a, color.g, color.b, 0.75f);
            }
        }

        public void LongDistance()
        {
            Color color = Color.white;
            area.color = new Color(color.a, color.g, color.b, 0f);
        }


        public void place(int value)
        {
            if (!isEmpty || !isCorrect(value))
                return;

            sprite.sprite = materials[value - 1];
            isEmpty = false;
            Placement.correct.Add(isCorrect(value));
            Placement.jumlahTerisi += 1;
        }

        bool isCorrect(int temp)
        {
            return temp == valueCorrect;
        }
    }
}