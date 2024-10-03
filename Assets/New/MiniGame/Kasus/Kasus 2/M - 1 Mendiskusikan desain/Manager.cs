using System.Collections;
using System.Collections.Generic;
using Terbaru;
using UnityEngine;


namespace MiniGame2_1{

    public class Manager : MonoBehaviour
    {
        public string action;
        public void checkAction(){
            FindObjectOfType<QuestManager>().CheckAction(action);
        }

        public void checkJawaban(bool jawaban){
            if(jawaban)
                Debug.Log("Benar");
                //Benar

            checkAction();
        }
    }
}