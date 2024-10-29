using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Terbaru{

    public class PointMaps : MonoBehaviour
    {
        public Image Gambar;
        public TMPro.TMP_Text namaMaps;
        public GameObject panelInfo;
        public GameObject warning, Lock;
        public infoMaps info;
        public Button btnMap;

        public MapsManager manager;

        public void initContent(Maps info, Material shadow){
            btnMap.onClick.RemoveAllListeners();
            Gambar.sprite = info.gambarMaps;
            namaMaps.text = info.nama;

            panelInfo.SetActive(!info.valueMaps.active || info.valueMaps.warning);

            //tnMap.onClick.AddListener((x) => {action(x);});

            Lock.SetActive(!info.valueMaps.active);
            warning.SetActive(info.valueMaps.warning);
            namaMaps.color = info.valueMaps.active ? Color.black : Color.white;
            GetComponent<Button>().interactable = info.valueMaps.active;

            Gambar.material = info.valueMaps.active ? null : shadow;

            btnMap.onClick.AddListener(() => {
                manager.keliling(info);
                QuestManager.instance.CheckActionQuest("Pindah");
                });
        }
    }
}
