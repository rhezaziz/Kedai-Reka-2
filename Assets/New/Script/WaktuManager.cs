
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

namespace Terbaru{

    public class WaktuManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public Light sun;
        public waktu thisWaktu;

        public List<dataWaktu> waktuData = new List<dataWaktu>();


        dataWaktu _waktu(waktu value){
            foreach(var _time in waktuData){
                if(_time.waktu == value){
                    return _time;
                }
            }

            return null;
        }

        public int indexTime(){
            return (int)thisWaktu;
        }

        public void gantiWaktu(int value){
            int index = (int)thisWaktu;
            index += 1;
            if(index >= System.Enum.GetValues(typeof(waktu)).Length)
                index = 0;
                
            currentTime(index);
        }

        public void currentTime(int value){
            waktu tempWaktu = (waktu)value;
            thisWaktu = tempWaktu;
            Color color = _waktu(tempWaktu).warnaCahaya;
            //sun.color = color;
            float intent = _waktu(tempWaktu).getIntent();

            sun.DOColor(color, 2f);
            sun.DOIntensity(intent, 2f);
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