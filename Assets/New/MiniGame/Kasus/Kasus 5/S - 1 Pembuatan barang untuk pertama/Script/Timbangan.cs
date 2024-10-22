using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MiniGame5_4{
    public class Timbangan : MonoBehaviour
    {
        float value;
        public List <Isi> isiValue = new List<Isi>();
        public SpriteRenderer isiTakaran;

        public Transform jarum;

        // Start is called before the first frame update
        
        public void tambahTakaran(float isi){
            int endIndex = isiValue.Count - 1;

            if(value >= isiValue[endIndex].takaran){
                return;
            }

            value += isi;

            foreach(var check in isiValue){
                if(check.takaran == value){
                    isiTakaran.sprite = check.gambar;
                    jarum.DORotate(new Vector3(0f, 0, check.rot), 1f);
                    return;
                }
            }

            
        }
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        
        [System.Serializable]
        public class Isi{
            public Sprite gambar;
            public float takaran;
            public float rot;
        }
    }
}
