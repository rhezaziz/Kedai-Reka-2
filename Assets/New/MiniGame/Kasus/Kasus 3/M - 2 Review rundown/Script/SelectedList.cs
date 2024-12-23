using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGame3_2{
    public class SelectedList : MonoBehaviour
    {
        //public Manager manager;
        public int jumlahGaris;

        public float timer;
        public bool selected;
        public bool value;
        public UnityEngine.UI.Image panelValue;

        public UnityEngine.UI.Text garisText;

        public void pilih(){
            selected = true;
            GetComponent<UnityEngine.UI.Button>().enabled = false;
            StartCoroutine(animasiGaris());
            panelValue.transform.localScale = new Vector3(.8f, .8f, .8f);
            panelValue.sprite = null;
            panelValue.color = Color.black;
        }

        IEnumerator animasiGaris(){

            float time = timer / jumlahGaris;
            for(int i = 0; i < jumlahGaris; i++){
                garisText.text += "_";
                yield return new WaitForSeconds(time);
            }
        }


        // Start is called before the first frame update
        
    }
}