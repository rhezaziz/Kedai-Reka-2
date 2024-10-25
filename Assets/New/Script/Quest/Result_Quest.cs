using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terbaru{
    public class Result_Quest : MonoBehaviour
    {
        public List<listCharacters> karakter = new List<listCharacters>();
        public Sprite karakterSesuai, karakterTidakSesuai;
        public GameObject Point;
        public TMPro.TMP_Text pointText;

        public GameObject PointBonus;
        public TMPro.TMP_Text pointBonusText;

        public Container_InfoQuest quest;

        public UnityEngine.UI.Button klikResult;

        public Controller control;

        public GameObject objectPanel;

        public GameObject panelItem;
        public UnityEngine.UI.Image gambarItem;
        public List<UiCharacter> uiS = new List<UiCharacter>();

        Sprite getSprite(Nama nama){
            foreach(var sprite in karakter){
                if(nama == sprite.nama){
                    return sprite.icon;
                }
                
            }
            return null;
        }
        public void result(listQuest currentQuest){
            objectPanel.SetActive(true);
            klikResult.onClick.RemoveAllListeners();
            //Debug.Log($"{quest.select.namaPilihan[0]} -- {quest.select.namaPilihan[1]} -- {quest.select.itemOn}");
            for(int i = 0; i  < quest.select.namaPilihan.Length; i++){
                Nama temp = quest.select.namaPilihan[i];
                uiS[i].karakter.sprite = getSprite(temp);
                uiS[i].namaKarakter.text = temp.ToString();
                uiS[i].border.sprite = karakterTidakSesuai;
                currentQuest.quest.pointBonus += 25;
                
                if(temp == currentQuest.quest.nama[i]){
                    uiS[i].border.sprite = karakterSesuai;
                    currentQuest.quest.pointBonus += 25;
                    break;
                }
            }
            klikResult.onClick.AddListener(() => tambahPoint(currentQuest));
        }



        public void tambahPoint(listQuest currentQuest){
            var playerProfil = GameManager.instance.profil;

            SoundManager.instance.uiSFX(2);
            playerProfil.Saldo += currentQuest.quest.Reward;

            UiManager.instance.UpdateSaldo(playerProfil.Saldo);
            UiManager.instance.Chinematic(true);
            objectPanel.SetActive(false);

            FindObjectOfType<UiManager>().panelUtama.SetActive(true);
            control.currentState(state.Default);
            control.playerMove.move = true;
        }
    }

    [System.Serializable]
    public class UiCharacter{
        public UnityEngine.UI.Image karakter;
        public UnityEngine.UI.Image border;
        public TMPro.TMP_Text namaKarakter;
    }
}