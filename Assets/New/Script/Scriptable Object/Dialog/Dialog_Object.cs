using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Dialog", order = 1)] 
public class Dialog_Object : ScriptableObject
    {
        public dataDialog[] data;
        //public listQuest quest; 
    }
    public class dataDialog{
        //public NarasiData narasi;
        public Sprite GambarKarakter;
        public string Nama;
        [TextArea(5,10)]
        public string sentences;
        public bool Kiri;
    }
