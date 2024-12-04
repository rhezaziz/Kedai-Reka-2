
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

namespace Terbaru{

    public class WaktuManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public Light[] lampu;
        public waktu thisWaktu;
        public I_Pad waktuIpad;
        public UnityEngine.UI.Image ImageTime;

        public List<dataWaktu> waktuData = new List<dataWaktu>();

        public Pintu Kamar;

        public List<GameObject> interaksi = new List<GameObject>();


        dataWaktu _waktu(waktu value){
            foreach(var _time in waktuData){
                if(_time.waktu == value){
                    return _time;
                }
            }

            return null;
        }

        public void changeInteraction(){
            if (GameManager.instance.isTutorial)
                return;

            foreach(var obj in interaksi){
                obj.GetComponent<Interaction>().changeInteractable(true);
            }
        }

        public int indexTime(){
            return (int)thisWaktu;
        }

        public void gantiWaktu(int value){
            int index = (int)thisWaktu;
            index += value;
            if(index >= System.Enum.GetValues(typeof(waktu)).Length)
                index = 0;
                
            currentTime(index);
            
        }

        public void currentTime(int value){
            waktu tempWaktu = (waktu)value;
            thisWaktu = tempWaktu;
            Color color = _waktu(tempWaktu).warnaCahaya;
            ImageTime.sprite = _waktu(tempWaktu).spriteTime; 
            bool valueKamar = thisWaktu == waktu.Pagi;
            Kamar.changeInteractable(!valueKamar && !FindObjectOfType<PapanQuest>().haveCerita());
            //sun.color = color;
            float intent = _waktu(tempWaktu).getIntent();
            waktuIpad.gantiJam(_waktu(tempWaktu).jam);
            foreach(var light in lampu){
                light.DOColor(color, 2f);
            }
            //sun.DOColor(color, 2f);
            //sun.DOIntensity(intent, 2f);

            if(thisWaktu == waktu.Malam && !GameManager.instance.isTutorial){
                Debug.Log("Ganti Interactable");
                foreach(var interact in interaksi){
                    interact.GetComponent<Interaction>().changeInteractable(false);
                }
                if (!FindObjectOfType<PapanQuest>().haveCerita())
                {
                    interaksi[0].GetComponent<Interaction>().changeInteractable(true);
                }
                else
                {
                    interaksi[1].GetComponent<Interaction>().changeInteractable(true);
                }
            }
        }

        public void checkTime(){
            if(thisWaktu == waktu.Malam){
                Debug.Log("Sudah Malam");
            }
        }
    }

    [System.Serializable]
    public class dataWaktu{
        public Color warnaCahaya;
        public waktu waktu;
        public Sprite spriteTime;
        public string jam;
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