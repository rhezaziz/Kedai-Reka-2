using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

namespace Terbaru
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager instance;
        public RekrutManager rekrut;

        public Misi_Manager quest;
        [Header("Profil")]
        public Image[] bar;
        public TMPro.TMP_Text Saldo;
        public TMPro.TMP_Text Nama;
        void Start(){
            var profil = GameManager.instance.profil; 

            Nama.text = profil.NamaKarakter;

            UpdateSaldo(profil.Saldo);
        }

        public void UpdateSaldo(int saldo){
            NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
            Saldo.text = "Rp" + saldo.ToString("n0", info);
        }

        void Awake(){
            instance = this;
        }

        public void displayRekrut(playerProfil profil){
            rekrut.gameObject.SetActive(true);

            rekrut.display(profil);
        }

        public void displayQuest(){
            quest.gameObject.SetActive(true);

            quest.UpdateListKarater();
        }
    }
}
