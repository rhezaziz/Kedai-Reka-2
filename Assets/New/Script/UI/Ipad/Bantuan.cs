using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terbaru
{

    public class Bantuan : MonoBehaviour
    {
        public List<GameObject> gambarBantuan = new List<GameObject>();

        public int Index;


        public void currentContent(int value)
        {
            gambarBantuan[Index].SetActive(false);
            gambarBantuan[value].SetActive(true);

            Index = value;

        }

        public void Next()
        {
            int check = Index + 1;
            int temp = check > gambarBantuan.Count - 1 ? 0 : check;

            currentContent(temp);
        }

        public void Previous()
        {
            int check = Index - 1;
            int temp = check < 0 ? gambarBantuan.Count - 1 : check;

            currentContent(temp);
        }
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
