using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Data.Common;

namespace Terbaru{

    public class MapsManager : MonoBehaviour
    {
        public Material shadow;
        [Header("UI")]
        public List<Button> btnMaps = new List<Button>();
        
        public TMP_Text judul;
        public List<infoMaps> maps = new List<infoMaps>();
        int index = 0;

        [Header("Chinematic")]
        public GameObject Chinematic;
        public GameObject panelUtama;
        public RectTransform panelMaps;
        public GameObject objectKeliling;
        public GameObject cameraKeliling;
        public GameObject panelNamaMaps;

        public class actionMaps : UnityEngine.Events.UnityEvent<int>{ }

        public Button NextBtn, PrevBtn;

        void Start(){
            Debug.Log(maps.Count);
            listMaps();
        }

        public void listMaps(){
            judul.text = maps[index].namaMaps;
            NextBtn.interactable = index < maps.Count - 1;
            PrevBtn.interactable = index > 0;

            foreach(var map in btnMaps){
                map.gameObject.SetActive(false);
            }

            for(int i = 0; i < maps[index].maps.Count; i++){
                var btn = btnMaps[i].gameObject;
                btn.SetActive(true);
                btn.GetComponent<PointMaps>().initContent(maps[index].maps[i], shadow);
            }
        }

        public void next(){
            index = index + 1 > maps.Count - 1 ? 0 : index + 1;
            //NextBtn.interactable = index == maps.Count - 1;
            listMaps();
        }

        public void prev(){
            index = index - 1 < 0 ? maps.Count - 1 : index - 1;
            //PrevBtn.interactable = index == 0;
            listMaps();
        }

        public void keliling(Maps temp){
            Debug.Log(temp.mapLokasi.name);
            int indexMaps = 0;
            for(int i = 0; i < maps[index].maps.Count; i++){
                if(maps[index].maps[i] == temp){
                    indexMaps = i;
                    
                    break;
                }
            }
            Debug.Log($"index Maps : {indexMaps}");
            float posY = index * -15f;
            float posX = indexMaps * 25f;

            

            panelMaps.gameObject.SetActive(false);
            panelUtama.gameObject.SetActive(false);
            //Chinematic.gameObject.SetActive(true);

            temp.mapLokasi.gameObject.SetActive(true);
            panelNamaMaps.transform.GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<TMP_Text>().text = temp.nama;


            StartCoroutine(Cutscene(new Vector3(posX, posY, -9.5f)));

        }


        IEnumerator Cutscene(Vector3 posisi){
            Animator anim = Chinematic.GetComponent<Animator>();
            var camera = Camera.main;
            panelUtama.SetActive(false);
            Chinematic.SetActive(true);
            camera.transform.DOLocalMoveZ(-7f, 1f);

            anim.SetTrigger("Mulai"); // Menutup 25%


            yield return new WaitForSeconds(3f);

            anim.SetTrigger("Mulai"); // Menutup 100%
            yield return new WaitForSeconds(2f);
            camera.gameObject.SetActive(false);
            objectKeliling.SetActive(true);
            Debug.Log(posisi);
            cameraKeliling.transform.localPosition = posisi;

            anim.SetTrigger("Mulai"); // Membuka 100%
            //camera.transform.DOLocalMoveZ(-10f, 1f);
            yield return new WaitForSeconds(1f);

            //panelUtama.SetActive(true);
            Chinematic.SetActive(false);
            panelNamaMaps.SetActive(true);
            panelNamaMaps.transform.GetChild(0).DOLocalMoveY(-250f, 1f);

            yield return new WaitForSeconds(1f);
            
            //FindObjectOfType<Movement>().move = true;
        }
    }

    [System.Serializable]
    public class infoMaps{
        public string namaMaps;
        public List<Maps> maps = new List<Maps>();
    }

    [System.Serializable]
    public class Maps{
        public string nama;
        public int index;
        public bool active;
        public bool warning;
        public Sprite gambarMaps;

        public Transform mapLokasi;
    }
}
