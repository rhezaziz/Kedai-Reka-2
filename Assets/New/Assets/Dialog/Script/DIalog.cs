using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Check{

    [System.Serializable]
    public class Dialog
    {
        //public Dialog_Object dialogObject;
        public listQuest quest;
        public UnityEvent events;    
    }
    // [System.Serializable]
    // public class dataDialog{
    //     public NarasiData narasi;
    //     public Sprite GambarKarakter;
    //     public string Nama;
    //     [TextArea(5,10)]
    //     public string sentences;
    //     public bool Kiri;
    // }
}