
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru{

    public class WaktuManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public Light sun;
        public waktu thisWaktu;

        public List<dataWaktu> waktuData = new List<dataWaktu>();
        void Start()
        {
            
        }

        dataWaktu _waktu(waktu value){
            foreach(var _time in waktuData){
                if(_time.waktu == value){
                    return _time;
                }
            }

            return null;
        }

        public void gantiWaktu(int value){
            int index = (int)thisWaktu;
            index += 1;
            currentTime(index);
        }

        public void currentTime(int value){
            waktu tempWaktu = (waktu)value;
            thisWaktu = tempWaktu;
            Color color = _waktu(tempWaktu).warnaCahaya;
            sun.color = color;
            sun.intensity = _waktu(tempWaktu).getIntent();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }

    [System.Serializable]
    public class dataWaktu{
        public Color warnaCahaya;
        public waktu waktu;

        public float maxIntent;
        public float minIntent;

        public float getIntent(){
            return Random.Range(minIntent, maxIntent);
        }

    }

    public enum waktu{
        Pagi,
        Siang,
        Sore,
        Malam
    }
}