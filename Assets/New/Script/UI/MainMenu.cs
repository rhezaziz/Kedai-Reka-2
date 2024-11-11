using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru{

    public class MainMenu : MonoBehaviour
    {
        [Header("Component Loading")]
        public GameObject Loading;
        public GameObject parentUI;


        public void PindahScene(string namaScene){
            GameObject temp = Instantiate(Loading);
            PlayerPrefs.SetString("Scene", namaScene);
            //Debug.Log("Loading");
            temp.transform.SetParent(parentUI.transform);
            temp.transform.localPosition = Vector3.zero;
            var rectTransform = temp.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0);  // Kiri Bawah
            rectTransform.anchorMax = new Vector2(1, 1);  // Kiri Atas

            // Mengatur Pivot ke sudut kiri atas
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            // Mengatur Left, Top, Right, dan Bottom
            rectTransform.offsetMin = new Vector2(0, 0);  // Left = 10, Bottom = -100
            rectTransform.offsetMax = new Vector2(0, 0);
            temp.transform.localScale = Vector3.one;
            temp.SetActive(true);
            //temp.GetComponent<TestLoading>().loadSceneBtn(namaScene);
        }
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }

}