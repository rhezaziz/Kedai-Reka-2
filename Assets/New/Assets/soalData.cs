using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Soal", order = 1)]
    public class soalData : ScriptableObject{
        [TextArea(3,5)]
        public string soal;
        public string pilA, pilB, pilC, pilD;
        public string jawaban;
    }
