using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame2_4{
    public class Manager : MonoBehaviour
    {
        public string jawaban;

        public void checkJawaban(string value){
            if(value == jawaban){
                Debug.Log("Benar");
            }

            else{
                Debug.Log("Salah");
            }
        }
    }

}