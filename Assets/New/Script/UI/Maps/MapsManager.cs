using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Terbaru{

    public class MapsManager : MonoBehaviour
    {
        #region Komponent Value
        public Material shadow;
        [Header("UI")]
        public List<Button> btnMaps = new List<Button>();

        public Pintu pintu;
        
        public TMP_Text judul;
        public List<infoMaps> maps = new List<infoMaps>();
        int index = 0;

        public Button kembali;

        [Header("Chinematic")]
        public GameObject Chinematic;
        public GameObject panelUtama;
        public RectTransform panelMaps;
        public GameObject objectKeliling;
        public GameObject cameraKeliling;
        public GameObject CameraUtama;
        public GameObject panelNamaMaps;

        public class actionMaps : UnityEngine.Events.UnityEvent<int>{ }

        public Button NextBtn, PrevBtn;
        #endregion
        void Start(){
            //listMaps();
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

        public void updateDayKonten(List<id_Maps> IDs){
            int jmlId = IDs.Count;

            for(int i = 0; i < jmlId; i++){
                for(int j = 0; j < maps.Count; j++){
                    for(int x = 0; x < maps[j].maps.Count; x++){
                        if(maps[j].maps[x].valueMaps.id == IDs[i]){
                            Debug.Log(maps[j].maps[x].nama);
                            maps[j].maps[x].valueMaps.active = true;
                            break;
                        }
                    }
                }
            }
            listMaps();
        }

        public void afterQuiz(List<id_Maps> IDs){
            IDs.Remove(id_Maps.Lt2_Kelas);
            int jmlId = IDs.Count;
            
            for(int i = 0; i < jmlId; i++){
                for(int j = 0; j < maps.Count; j++){
                    for(int x = 0; x < maps[j].maps.Count; x++){
                        if(maps[j].maps[x].valueMaps.id == IDs[i]){
                            Debug.Log(maps[j].maps[x].nama);
                            maps[j].maps[x].valueMaps.active = true;
                            break;
                        }
                    }
                }
            }
            listMaps();
        }
        public void updateDayKonten(ListMapsValue updateMaps){
            List<mapsValue> tempValue = new List<mapsValue>();
            tempValue = updateMaps.listValueMaps;

            for(int i = 0; i < maps.Count; i++){
                foreach(var _maps in maps[i].maps){
                    if(_maps.valueMaps.id == tempValue[0].id){
                        _maps.valueMaps = tempValue[0];
                        tempValue.RemoveAt(0);
                        
                    }
                    
                }   
            }

            listMaps();
        }
    #region UI
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
    #endregion
        

    #region  transisi
        GameObject lokasi;
        

        public void Kembali(){
    
            panelNamaMaps.transform.GetChild(0).DOLocalMoveY(-500f, 1f).OnComplete(() =>
            StartCoroutine(Cutscene(Vector3.zero, false)));
            //pintu.tutupPintu();
        }
        public void keliling(Maps temp){
            kembali.onClick.RemoveAllListeners();
            //Debug.Log(temp.mapLokasi.name);
            int indexMaps = 0;
            for(int i = 0; i < maps[index].maps.Count; i++){
                if(maps[index].maps[i] == temp){
                    indexMaps = i;
                    
                    break;
                }
            }
            float posY = index * -15f;
            float posX = indexMaps * 25f;
            lokasi = temp.mapLokasi.gameObject;
            

            panelMaps.gameObject.SetActive(false);
            panelUtama.gameObject.SetActive(false);
            Chinematic.gameObject.SetActive(true);

            //lokasi.gameObject.SetActive(true);
            panelNamaMaps.transform.GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<TMP_Text>().text = temp.nama;


            StartCoroutine(Cutscene(new Vector3(posX, posY, -9.5f), true));
            kembali.onClick.AddListener(() => Kembali());

        }


        IEnumerator Cutscene(Vector3 posisi, bool value){
            Debug.Log(value);
            Animator anim = Chinematic.GetComponent<Animator>();
            //var camera = Camera.main;
            panelUtama.SetActive(false);
            Chinematic.SetActive(true);
            CameraUtama.transform.DOLocalMoveZ(-7f, 1f);

            anim.SetTrigger("Mulai"); // Menutup 25%


            yield return new WaitForSeconds(3f);

            anim.SetTrigger("Mulai"); // Menutup 100%
            yield return new WaitForSeconds(2f);
            lokasi.SetActive(value);
            CameraUtama.transform.DOLocalMoveZ(-10f, 1f);
            CameraUtama.gameObject.SetActive(!value);
            objectKeliling.SetActive(value);
            
            panelUtama.SetActive(!value);
            
            if(!value)
                pintu.tutupPintu();

            cameraKeliling.transform.localPosition = posisi;

            anim.SetTrigger("Mulai"); // Membuka 100%
            //camera.transform.DOLocalMoveZ(-10f, 1f);
            yield return new WaitForSeconds(2f);

            //panelUtama.SetActive(true);
            Chinematic.SetActive(false);
            panelNamaMaps.SetActive(value);
            panelNamaMaps.transform.GetChild(0).DOLocalMoveY(-250f, 1f);

            yield return new WaitForSeconds(1f);
            
            //FindObjectOfType<Controller>().currentState(value ? state.Interaction : state.Default);
            
            
            //FindObjectOfType<Movement>().move = true;
        }
    }
    #endregion

    [System.Serializable]
    public class infoMaps{
        public string namaMaps;
        public List<Maps> maps = new List<Maps>();
    }

    [System.Serializable]
    public class Maps{
        
        public string nama;
        public mapsValue valueMaps;

        [Header("Maps Value")]
        public int index;
        
        public Sprite gambarMaps; 
        public Transform mapLokasi;
    }

    [System.Serializable]
    public class ListMapsValue{
        public int  hari;
        public List<mapsValue> listValueMaps;
    }

    [System.Serializable]
    public class mapsValue{

        public string namaTempat;
        public id_Maps id;

        public bool active;
        public bool warning;

    }

    public enum id_Maps{
        //Bebas
        B_Apotek,
        B_jlnKota,
        B_kantorRW,
        B_kantorRT,
        B_Mall,
        B_Parkiran,
        B_Komplek,

        //Lantai 1
        Lt1_Gudang,
        Lt1_Kantin,
        Lt1_Kelas,
        Lt1_Lift,
        LT1_Lobby,
        Lt1_Pekarangan,
        Lt1_Perpus,
        Lt1_Toilet,

        //Lantai 2
        Lt2_Aula,
        Lt2_Kelas,
        Lt2_Lorong,
        Lt2_Lift,
        Lt2_R_Dosen,
        Lt2_Toilet,

        //Lantai 3

        Lt3_Audio,
        Lt3_Kelas,
        Lt3_Labkom,
        Lt3_Tangga,
        LT3_Lorong,
        Lt3_Toilet
    }
}
