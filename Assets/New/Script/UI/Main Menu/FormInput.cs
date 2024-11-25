using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Terbaru{

    public class FormInput : MonoBehaviour
    {
        public Button btnOk;
        public TMP_InputField Nama, NIM;
        public PlayVideoAwal video;

        public playerProfil profil;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        bool fieldInputNotNull(){
            if(string.IsNullOrWhiteSpace(Nama.text) || string.IsNullOrWhiteSpace(NIM.text))
                return false;

            return true;
        }

        public void checkInput(){
            btnOk.interactable = fieldInputNotNull();
        }

        public void mulai(){
            resetData();
        }

        void resetData(){
            profil.NamaKarakter = Nama.text;
            foreach(var item in profil.item){
                item.isInventory = false;
                if(!item.isSpawn)
                    item.isShop = true;
            }

            foreach(var npc in profil.character){
                npc.characterLock = true;
                npc.isLockSkill = true;
            }

            profil.jmlHari = 1;

            profil.setEnergy(10);
            profil.Saldo = 0;

            foreach(var quest in profil.quest){
                quest.isDone = false;
                quest.pointBonus = 0;
            }


            klikOk();

        }

        public void klikOk(){
            video.gameObject.SetActive(true);
            video.enabled = true;
            gameObject.SetActive(false);
        }
    }
}
