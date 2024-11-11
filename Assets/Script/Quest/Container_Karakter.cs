using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Container_Karakter : MonoBehaviour
{
    [Header("List Karakter")]
    public Image fotoKarakter;
    public Button btnKarakter;
    public TMP_Text namaKarakter;
    public Transform parent;

    [Header("Info Karakter")]
    public GameObject panelInfoKarakter;
    public Image fotoInfo;
    public TMP_Text skillKarakter;
    public Button pilihBtn;
    public Button kembaliBtn;
    public TMP_Text NamaInfoKarakter;
    string skill;

    public Container_InfoQuest info;
    int index;

    bool selected = false;

    listCharacters characterInfo;
    [SerializeField] private Material greyMat;

     

    public bool setSelected(bool value)
    {
        return selected = value;
    }
    public void initKarkater(listCharacters karakter, int value)
    {
        characterInfo = karakter;
        fotoKarakter.sprite = karakter.imageCharacter;
        namaKarakter.text = karakter.namaCharacter;
        index = value;
        if (karakter.characterLock)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            btnKarakter.onClick.AddListener(() => infoKarakter(karakter));
        }
    }

    void infoKarakter(listCharacters karakter)
    {
        
        pilihBtn.onClick.RemoveAllListeners();
        panelInfoKarakter.SetActive(true);
        skill = null;
        fotoInfo.sprite = karakter.imageInfoCharacter;
        NamaInfoKarakter.text = karakter.namaCharacter;
        pilihBtn.onClick.AddListener(() => pilih());
        kembaliBtn.onClick.AddListener(() => closeInfo());
        for(int i = 0; i < karakter.skillCharacter.Length; i++)
        {
            Kemampuan _skill = karakter.skills[i];
            skill += " - " + _skill.ToString() + "<br>";
        }
        skillKarakter.text = skill;
    }

    public void UnInteract()
    {
        fotoKarakter.material = greyMat;
        btnKarakter.interactable = false;
    }

    public void InteractKarakter()
    {
        if (!selected)
        {
            fotoKarakter.material = null;
            btnKarakter.interactable = true;

        }
    }

    public void pilih()
    {

        closeInfo();
        selected = true;
        btnKarakter.interactable = false;
        fotoKarakter.material = greyMat;
        info.pilihKarakter(characterInfo, GetComponent<Container_Karakter>());
        
        //Debug.Log(value);
    }

    void closeInfo()
    {
        panelInfoKarakter.SetActive(false);
        kembaliBtn.onClick.RemoveAllListeners();
        pilihBtn.onClick.RemoveAllListeners();
    }
}
