using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Terbaru{

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Dialog", order = 1)] 
    public class Dialog_Object : ScriptableObject
    {
        public dataDialog[] data;
        //public listQuest quest; 
    }
    [System.Serializable]
    public class dataDialog{
        public NarasiData narasi;
        public Sprite GambarKarakter;
        public Nama Nama;
        [TextArea(5,10)]
        public string sentences;
        public bool Kiri;
    }

    public enum Nama{
        Thomas,
        Elena,
        Dosen,
        Alex,
        Andi,
        Anna,
        Ayu,
        Cici,
        Dinda,
        Isy,
        Michael,
        Shinta,
        Yoseph,
        
        Semua,
        Abdan,
    }

    [System.Serializable]
    public class NarasiDialog{
        public bool inAsrama;
        public bool dialogWithPerempuan;
        public Dialog narasi;

    }

    [System.Serializable]
    public class NarasiData{
        
        [TextArea(5,10)]
        public string narasiText;
        public AudioClip VO;
    }

}
