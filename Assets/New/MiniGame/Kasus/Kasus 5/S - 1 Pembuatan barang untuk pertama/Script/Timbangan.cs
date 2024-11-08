using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace MiniGame5_4{
    public class Timbangan : MonoBehaviour
    {
        float value;
        public List <Isi> isiValue = new List<Isi>();
        public SpriteRenderer isiTakaran;

        public Transform jarum;

        public TMP_Text textTakaran;

        // Start is called before the first frame update
        
        public void tambahTakaran(float isi){
            int endIndex = isiValue.Count - 1;
            StartCoroutine(munculText(isi));
            if(value >= isiValue[endIndex].takaran){
                return;
            }

            value += isi;
            FindObjectOfType<Manager>().setCurrentTakaran((int)isi);
            foreach(var check in isiValue){
                if(check.takaran == value){
                    isiTakaran.sprite = check.gambar;
                    jarum.DORotate(new Vector3(0f, 0, check.rot), 1f);
                    return;
                }
            }
        }

        IEnumerator munculText(float isi){
            textTakaran.text = $"+{(int)isi} Gram";
            textTakaran.DOColor(new Color(1f,1f,1f,1f),.5f);

            yield return new WaitForSeconds(1.5f);
            textTakaran.DOColor(new Color(1f,1f,1f,0f),.5f);

        }


        public void resetIsi(){
            isiTakaran.sprite = null;
            value = 0;
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
