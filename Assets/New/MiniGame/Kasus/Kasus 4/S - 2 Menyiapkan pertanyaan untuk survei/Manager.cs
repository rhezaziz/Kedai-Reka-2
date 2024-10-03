using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGame4_4{
    public class Manager : MonoBehaviour
    {

        public List<dataSoal> soals = new List<dataSoal>();

        public GameObject prefabPertanyaan;
        public Transform parent;

        public void nextPertanyaan(){
            if(soals.Count > 0){
                prefabPertanyaan.GetComponent<DragObject>().spawn(soals[0]);
                soals.RemoveAt(0);
            }else{
                Debug.Log("Selesai");
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            nextPertanyaan();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }

    [System.Serializable]
    public class dataSoal{
        public bool value;
        public string Pertanyaan;
    }
}