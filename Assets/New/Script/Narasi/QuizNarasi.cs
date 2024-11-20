using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru{
    public class QuizNarasi : MonoBehaviour
    {
        // public NarasiDialog[]
        public Dialog[] dialogs;
        public NarasiDialog[] narasi;
        public void jawab(int index){
            QuestManager.instance.CheckActionQuest("Narasi");
            FindObjectOfType<Narasi>().nextDialog(dialogs[index]);
        }

        public void jawabInNarasi(int index){
            QuestManager.instance.CheckActionQuest("Narasi");
            FindObjectOfType<Narasi>().nextDialog(narasi[index]);
        }
    }
}
