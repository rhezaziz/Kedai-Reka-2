using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGame4_1{
    public class Manager : MonoBehaviour
    {
        public List<items> listItem = new List<items>();
        public Transform parent;
        public GameObject prefabText;


        public void startKonten(){

            foreach(var item in listItem){
                GameObject temp = Instantiate(prefabText);
                temp.SetActive(true);
                Vector3 rotation = prefabText.transform.localEulerAngles;
                temp.transform.SetParent(parent);
                temp.transform.localScale = Vector3.one;
                temp.transform.localEulerAngles = rotation;
                temp.GetComponent<Text>().text = item.nama;
                item.loct = temp.transform;
                item.ObjectItem.GetComponent<SelectedObject>().setItemsValue(item.loct);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            startKonten();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }

    [System.Serializable]
    public class items
    {
        public string nama;
        public Transform ObjectItem;
        public Transform loct;
    }
}
