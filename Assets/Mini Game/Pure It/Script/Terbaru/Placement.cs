using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru
{
    public class Placement : MonoBehaviour
    {
        static public List<bool> correct;
        static public int jumlahTerisi;

        int jumlahMaterial;
        ManagerPurify manager;

        private void Start()
        {
            manager = FindObjectOfType<ManagerPurify>();
            jumlahMaterial = FindObjectsOfType<PlacementMaterial>().Length;
            correct = new List<bool>();
            StartCoroutine(checkGame());
        }


        IEnumerator checkGame()
        {
            while (jumlahTerisi != jumlahMaterial)
            {
                yield return new WaitForSeconds(1f);
                //Debug.Log("Jalan");
                //Jalan
            }
            manager.GameEnd(correctPlace());
            yield return null;
        }

        bool correctPlace()
        {
            for (int i = 0; i < jumlahMaterial; i++)
            {
                if (!correct[i])
                    return false;
            }
            return true;
        }

        void checkCorrect()
        {

        }
    }
}