using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Terbaru{

    public class Container_Rekrut : MonoBehaviour
    {
        public Image Foto;
        public TMP_Text Nama;
        public listCharacters dataCharacter;
        public Button Rekrut, info;

        public TMP_Text skill;

        public bool isLock;
        public void initData(listCharacters data){
            dataCharacter = data;
            Foto.sprite = data.imageCharacter;
            Nama.text = data.namaCharacter;
            Foto.name = $"Character {data.namaCharacter}";
            skill.text = data.skillCharacter[0];
            info.name = $"Info {data.namaCharacter}";
            isLock = data.characterLock;
            if(!data.characterLock){
                Rekrut.GetComponent<Button>().interactable = false;
                Foto.material = null;
                Foto.transform.parent.GetComponent<Image>().enabled = false;
            }
            else{
                Rekrut.GetComponent<Button>().interactable = true;
                Foto.material = RekrutManager.matTemp;
                Foto.transform.parent.GetComponent<Image>().enabled = true;
            
            }
        }
    }
}