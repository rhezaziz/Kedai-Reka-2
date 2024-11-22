using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru
{
    public class Manager_Ending : MonoBehaviour
    {
        public listQuest quest;

        public List<DialogObjectClick> Wargas = new List<DialogObjectClick>();
        

        IEnumerator Start()
        {
            yield return new WaitForSeconds(2f);

            startEnding();
        }
        public void tambahMain(DialogObjectClick thisObj)
        {
            Wargas.Remove(thisObj);

            if (Wargas.Count > 0)
            {
                Debug.Log("Selesai");
            }
        }
        public void startEnding()
        {
            //GameManager.instance.DestroyThisObject();
            //QuestManager.instance.StartQuest(quest);

            foreach(var warga in Wargas)
            {
                warga.interactNPC(true);
            }
        }


    }
}