using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

namespace Terbaru{

    public class MapsManager : MonoBehaviour
    {
        public AudioSource ambienceSound;
        #region Komponent Value
        public Material shadow;
        [Header("UI")]
        public List<Button> btnMaps = new List<Button>();

        public Pintu pintu;
        public bool onLokasi;
        public TMP_Text judul;
        public bool onAnimation;
        public List<infoMaps> maps = new List<infoMaps>();
        int index = 0;

        public Button kembali;
        public Button mapButton;
        public Button closeMapsPanel;

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

        public float testY;
        #endregion
        void Start(){
            //listMaps();
        }

        public void listMaps(){
            int hari = FindObjectOfType<DayManager>().day;
            //judul.text = maps[index].namaMaps;
            //NextBtn.interactable = index < maps.Count - 1;
            //PrevBtn.interactable = index > 0;

            foreach(var map in btnMaps){
                map.gameObject.SetActive(false);
            }

            for(int i = 0; i < maps[index].maps.Count; i++){
                // var eventMaps = maps[index].maps
                maps[index].maps[i].valueMaps.warning = haveEvent(maps[index].maps[i]);
                //Debug.Log($"{maps[index].maps[i].nama} {haveEvent(maps[index].maps[i])}");
                
                
                var btn = btnMaps[i].gameObject;
                btn.SetActive(true);
                btn.GetComponent<PointMaps>().initContent(maps[index].maps[i], shadow);
            }
        }

        bool haveEvent(Maps action){
            
            int hari = FindObjectOfType<DayManager>().day;
           
            
            foreach(var events in action.events){
                if(events.day == hari){
                   
                    //Debug.Log($"{action.nama} : {events.events.GetPersistentEventCount() > 0 && !events.done}");
                    return events.events.GetPersistentEventCount() > 0 && !events.done;
                }
            }
            
            return false;
        }

        

        public void updateDayKonten(List<id_Maps> IDs){
            int jmlId = IDs.Count;

            for(int i = 0; i < jmlId; i++){
                for(int j = 0; j < maps.Count; j++){
                    for(int x = 0; x < maps[j].maps.Count; x++){
                        if(maps[j].maps[x].valueMaps.id == IDs[i]){
                            //Debug.Log(maps[j].maps[x].nama);
                            maps[j].maps[x].valueMaps.active = true;
                            break;
                        }
                    }
                }
            }
            listMaps();
        }

        List<id_Maps> tempOpenMaps;
        public void openMaps(List<id_Maps> Ids){
            tempOpenMaps = new List<id_Maps>();
            tempOpenMaps = Ids;

            kembali.interactable = false;
            mapButton.GetComponent<Button>().interactable = false;

            updateDayKonten(Ids);
        }

        public void closeMaps(){
            int jmlId = tempOpenMaps.Count;
            for(int i = 0; i < jmlId; i++){
                for(int j = 0; j < maps.Count; j++){
                    for(int x = 0; x < maps[j].maps.Count; x++){
                        if(maps[j].maps[x].valueMaps.id == tempOpenMaps[i]){
                            //Debug.Log(maps[j].maps[x].nama);
                            maps[j].maps[x].valueMaps.active = false;
                            break;
                        }
                    }
                }
            }

            kembali.interactable = true;
            mapButton.GetComponent<Button>().interactable = true;
            listMaps();
            tempOpenMaps.Clear();
        }

        public void afterQuiz(List<id_Maps> IDs){
            IDs.Remove(id_Maps.Lt2_Kelas);
            foreach(var x in maps[2].maps){
                if(id_Maps.Lt2_Kelas == x.valueMaps.id){
                    x.valueMaps.active = false;
                }
            }
            //FindObjectOfType<WaktuManager>().gantiWaktu(1);
            listMaps();
            // for(int i = 0; i < jmlId; i++){
            //     for(int j = 0; j < maps.Count; j++){
            //         for(int x = 0; x < maps[j].maps.Count; x++){
            //             if(maps[j].maps[x].valueMaps.id == IDs[i]){
            //                 //Debug.Log(maps[j].maps[x].nama);
            //                 maps[j].maps[x].valueMaps.active = true;
            //                 break;
            //             }
            //         }
            //     }
            // }
            //listMaps();
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
        public List<GameObject> objGantis;
        public GameObject objTemp;
        public void changeMapsList(int value)
        {
            objTemp.SetActive(false);
            objTemp = objGantis[value];
            objTemp.SetActive(true);

            index = value;
            //NextBtn.interactable = index == maps.Count - 1;
            listMaps();
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
    #endregion
        

    #region  transisi
        GameObject lokasi;
        

        public void Kembali(){
            kembali.interactable = false;
            mapButton.GetComponent<Button>().interactable = false;

            closeMapsPanel.onClick.AddListener(closePanelMaps);

            onAnimation = true;
            onLokasi = false;
            panelNamaMaps.GetComponent<RectTransform>().DOPivotY(0f, 1f).OnComplete(() =>
            StartCoroutine(Cutscene(Vector3.zero, false)));
            closeMapsPanel.onClick.AddListener(() => closeButtonAction(true));
            closeMapsPanel.onClick.RemoveListener(() => closeButtonAction(false));
            
            //pintu.tutupPintu();
        }
        public void panelMuncul()
        {
            panelNamaMaps.GetComponent<RectTransform>().DOPivotY(0f, .1f).OnComplete(() =>
            {
                panelNamaMaps.GetComponent<RectTransform>().DOPivotY(1f, 1f);
            });
        }

        public void closePanelMaps()
        {
            state tempState = !onLokasi ? state.Default : state.Interaction;
            FindObjectOfType<Controller>().currentState(tempState);
        }

        Maps tempMaps;

        public void closeButtonAction(bool value){
            state tempState = value ? state.Default : state.Interaction;
            FindObjectOfType<Controller>().currentState(tempState);
        }
        public void keliling(Maps temp){
            
            onAnimation = true;
            onLokasi = true;

            closeMapsPanel.onClick.RemoveListener(closePanelMaps);

            closeMapsPanel.onClick.AddListener(() => closeButtonAction(false));
            closeMapsPanel.onClick.RemoveListener(() => closeButtonAction(true));
            
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
            tempMaps = temp;
            panelMaps.gameObject.SetActive(false);
            panelUtama.gameObject.SetActive(false);
            Chinematic.gameObject.SetActive(true);
            mapButton.interactable = false;
            kembali.interactable = false;
            panelNamaMaps.GetComponent<RectTransform>().DOPivotY(0, .5f).OnComplete(() => {
                
                mapButton.interactable = true;
                kembali.interactable = true;
            });
            

            //lokasi.gameObject.SetActive(true);
            panelNamaMaps.transform.GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<TMP_Text>().text = temp.nama;
            
            StartCoroutine(Cutscene(new Vector3(posX, posY, -9.5f), true));
            //kembali.onClick.AddListener(() => Kembali());

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

            if(value){
                FindObjectOfType<SoundManager>().stopAudio();
                
            }else if(!value){
                ambienceSound.Stop();
                FindObjectOfType<SoundManager>().playSoundAsrama();
            }
            lokasi.SetActive(value);
            CameraUtama.transform.DOLocalMoveZ(-10f, 1f);
            CameraUtama.gameObject.SetActive(!value);
            objectKeliling.SetActive(value);
            
            panelUtama.SetActive(!value);
            
            
            if(!value)
                pintu.tutupPintu();

            cameraKeliling.transform.localPosition = posisi;
            Debug.Log("Reverse");
            anim.SetTrigger("Reverse"); // Membuka 100%
            //camera.transform.DOLocalMoveZ(-10f, 1f);

            if(tempMaps.valueMaps.ambience != null){
                    //Debug.Log("Play Ambience");
                    ambienceSound.Stop();
                    ambienceSound.clip = tempMaps.valueMaps.ambience;
                    ambienceSound.Play();
            }
            yield return new WaitForSeconds(2f);

            

            //panelUtama.SetActive(true);
            //Chinematic.SetActive(false);
            panelNamaMaps.SetActive(value);
            panelNamaMaps.GetComponent<RectTransform>().DOPivotY(1f, 1f);
            //panelMaps.gameObject.SetActive(true);
            //kembali.interactable = !mapButton.activeInHierarchy;
            //Debug.Log(mapButton.activeInHierarchy);
            //panelMaps.DOPivotY(1f, .5f);
            yield return new WaitForSeconds(1f);
            onAnimation = false;
            
            FindObjectOfType<Controller>().currentState(value ? state.Interaction : state.Default);
            
            if(onLokasi){
                int hari = FindObjectOfType<DayManager>().day;
                foreach(var action in tempMaps.events){
                    if(action.day == hari && !action.done){
                        action.done = true;
                        //Debug.Log($"Ada : {action.events.GetPersistentEventCount()}");
                        action.events?.Invoke();
                        action.events.RemoveAllListeners();
                    } 
                }

                
            }

            QuestManager.instance.CheckActionQuest(tempMaps.nama);
                
            
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

        public List<dataActionInMaps> events = new List<dataActionInMaps>();
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

        public AudioClip ambience;

        public bool active;
        public bool warning;

        

    }

    [System.Serializable]
    public class dataActionInMaps{
        public int day;
        public UnityEvent events;  
        public bool done;
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
