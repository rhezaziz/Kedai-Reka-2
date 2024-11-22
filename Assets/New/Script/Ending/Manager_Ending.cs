using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru
{
    public class Manager_Ending : MonoBehaviour
    {
        public listQuest quest;
        public void startEnding()
        {
            GameManager.instance.DestroyThisObject();
            QuestManager.instance.StartQuest(quest);
        }


    }
}