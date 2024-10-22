using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru{

    [System.Serializable]
    public class Dialog
    {
        public dataDialog[] data;
        public listQuest quest;    
    }
    [System.Serializable]
    public class dataDialog{
        public NarasiData narasi;
        public Sprite GambarKarakter;
        public string Nama;
        [TextArea(5,10)]
        public string sentences;
        public bool Kiri;
    }
}