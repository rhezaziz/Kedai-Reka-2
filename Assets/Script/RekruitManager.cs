using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using System;

public class RekruitManager : MonoBehaviour
{
    public playerProfil profil;

    public GameObject prefabs;

    public Transform parent;


    public GameObject panelInfo;
    public GameObject panelListCharacter;
    public Image fotoInfo;
    public Material mat;
    public Button Beli, Kembali;
    public TMP_Text SkillText,NamaText, TitleText, Deskripsi;
    public TMP_Text saldoText;

    public GameObject popUp;

    private void Start()
    {
        // initial();
        mulai();
    }

    public void mulai()
    {
        initial();
        NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
        saldoText.text = "Saldo : "+"Rp" + profil.Saldo.ToString("n0", info);
    }
    void initial()
    {
        GameObject character;
        for(int i = 0; i < profil.character.Count; i++) {
            character = Instantiate(prefabs);
            Image fotoCharacter = character.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
            TMP_Text namaCharacter = character.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            TMP_Text SkillCharacter = character.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMP_Text>();
            GameObject rekrutButton = character.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(0).gameObject;
            Button infoButton = character.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetComponent<Button>();
            fotoCharacter.sprite = profil.character[i].imageCharacter;
            namaCharacter.text = profil.character[i].namaCharacter;

            rekrutButton.name = i.ToString();

            character.name = i.ToString();
            SkillCharacter.text = "";
            infoButton.onClick.RemoveAllListeners();
            infoButton.onClick.AddListener(() => lihatProfil(int.Parse(rekrutButton.name)));

            for(int j = 0; j < profil.character[i].skillCharacter.Length; j++) {

                SkillCharacter.text += profil.character[i].skillCharacter[j] + " | ";
            }

            if (!profil.character[i].characterLock)
            {
                rekrutButton.GetComponent<Button>().interactable = false;
                fotoCharacter.material = null;
                fotoCharacter.transform.parent.GetComponent<Image>().enabled = false;
            }
            else
            {
                rekrutButton.GetComponent<Button>().onClick.RemoveAllListeners();
                rekrutButton.GetComponent<Button>().onClick.AddListener(() => popUpMuncul(int.Parse(rekrutButton.name)));
            }
            
            character.SetActive(true);
            character.transform.SetParent(parent);

            character.transform.localScale = new Vector2(1f, 1f);

        }
    }

    void updateListCharacter(int index)
    {
        NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
        saldoText.text = "Saldo : " + "Rp" + profil.Saldo.ToString("n0", info);
        GameObject character =  parent.transform.GetChild(index + 1).gameObject;
        character.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().material = null;
        character.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetComponent<Button>().interactable = false;
    }

    void lihatProfil(int index)
    {
        //int index = int.Parse(button.name);
        Debug.Log(index);
        panelInfo.SetActive(true);
        panelListCharacter.SetActive(false);
        fotoInfo.sprite = profil.character[index].imageInfoCharacter;
        NamaText.text = profil.character[index].namaCharacter;
        TitleText.text = profil.character[index].titleCharacter;
        SkillText.text = "";
        Deskripsi.text = profil.character[index].deskripsiKarakter;
        Kembali.onClick.AddListener(() => closeInfo());
        for (int i = 0; i < profil.character[index].skillCharacter.Length; i++)
        {
            SkillText.text += "<br>" + "-" + profil.character[index].skills[i].ToString();
        }

        if (!profil.character[index].characterLock)
        {
            Beli.gameObject.SetActive(false);
            fotoInfo.material = null;
        }
        else
        {
            Beli.gameObject.SetActive(true);
            Beli.onClick.AddListener(() => rekrut(index));
            NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
            Beli.transform.GetChild(0).GetComponent<TMP_Text>().text = "Rp" + profil.character[index].cost.ToString("n0", info);
            fotoInfo.material = mat;
        }
    }

    void rekrut(int index)
    {
        //int index = int.Parse(button.name);
        int saldo = profil.Saldo;
        int harga = profil.character[index].cost;

        popUpMuncul(saldo >= harga);
        if (saldo >= harga)
        {
            Beli.gameObject.SetActive(false);
            profil.Saldo -= harga;
            fotoInfo.material = null;
            profil.character[index].characterLock = false;
            updateListCharacter(index);
            Beli.gameObject.SetActive(false);
            //Debug.Log("Berhasil");
            FindObjectOfType<UIManager>().updateCoint();
        }
        else
        {
            Debug.Log("Saldo Kurang");
        }
        
    }

    void popUpMuncul(bool succes)
    {
        popUp.SetActive(true);
        GameObject button = popUp.transform.GetChild(0).GetChild(1).GetChild(1).gameObject;
        Button btnOK = popUp.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>();
        TMP_Text textPopUp = popUp.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        button.SetActive(false);
        btnOK.transform.GetChild(0).GetComponent<TMP_Text>().text = "OK";
        btnOK.onClick.RemoveAllListeners();
        btnOK.onClick.AddListener(() => popUp.SetActive(false));

        btnOK.onClick.AddListener(() => btnOK.onClick.RemoveAllListeners());
        textPopUp.text = succes ? "Berhasil Rekrut Karakater" : "Saldo Kurang Untuk Rekrut Karakter";
    }

    void popUpMuncul(int index)
    {
        popUp.SetActive(true);
        Button btnKembali = popUp.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Button>();
        Button btnOK = popUp.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>();
        TMP_Text textPopUp = popUp.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
        string hargaText = "Rp" + profil.character[index].cost.ToString("n0", info);

        textPopUp.text = "Apa anda yakin rekrut karakter ?";
        btnKembali.gameObject.SetActive(true);
        btnKembali.onClick.AddListener(() => btnOK.onClick.RemoveAllListeners());
        btnKembali.onClick.AddListener(() => popUp.SetActive(false));
        btnKembali.onClick.AddListener(() => btnKembali.onClick.RemoveAllListeners());
        btnOK.transform.GetChild(0).GetComponent<TMP_Text>().text = hargaText;
        btnOK.onClick.AddListener(() => rekrut(index));
        //btnOK.
    }

    void closeInfo()
    {
        Beli.GetComponent<Button>().onClick.RemoveAllListeners();

        Kembali.GetComponent<Button>().onClick.RemoveAllListeners();

    }
}
