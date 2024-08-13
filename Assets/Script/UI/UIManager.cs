using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TMP_Text textCoint;
    public GameObject[] energy;

    public playerProfil profil;

    [Header("Dialog")]
    public TMP_Text text_Dialog;
    public TMP_Text text_Nama;
    public Image spriteKarakterDialog;
    public Image spritePlayer;
    public GameObject parentDialog;
    public Button nextDialogButton;
    public Color DefaultColor;
    public Color DisableColor;
    private void Awake()
    {
        Instance = this;
    }

    public void initDialog(string Nama = "", Sprite Ekpresi = null, bool isPlayer = false)
    {
        Image temp = isPlayer ? spritePlayer : spriteKarakterDialog;
        spriteKarakterDialog.color = isPlayer ? DisableColor : DefaultColor;
        spritePlayer.color = isPlayer ? DefaultColor : DisableColor;
        //text_Dialog.text = dialog.sentence;
        text_Nama.text = Nama;
        temp.sprite = Ekpresi;
    }



    private void Start()
    {
        updateCoint();
        updateEnergy();
    }

    public void updateCoint()
    {
        NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
        //textCoint.text = "Saldo : " + "Rp" + profil.Saldo.ToString("n0", info);
    }

    public void updateEnergy()
    {
        for(int i = 0; i < energy.Length; i++)
        {
            if ((i + 1)> profil.Energy)
            {
                energy[i].SetActive(false);
            }
        }
    }
}
