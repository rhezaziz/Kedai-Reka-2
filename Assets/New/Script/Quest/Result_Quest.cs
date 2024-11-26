using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        public Transform panelResutl;

        public GameObject panelItem;
        public UnityEngine.UI.Image gambarItem;

        public Transform Camera;

        public bool isRPG;
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
            panelResutl.localScale = Vector2.zero;
            objectPanel.SetActive(true);
            panelResutl.DOScale(Vector2.one * .9f, 1f);
            tempQuest = currentQuest;
            //klikResult.onClick.RemoveAllListeners();
            //Debug.Log($"{quest.select.namaPilihan[0]} -- {quest.select.namaPilihan[1]} -- {quest.select.itemOn}");
            for(int i = 0; i  < quest.select.namaPilihan.Length; i++){
                Nama temp = quest.select.namaPilihan[i];
                uiS[i].karakter.sprite = getSprite(temp);
                uiS[i].namaKarakter.text = temp.ToString();
                uiS[i].border.sprite = karakterTidakSesuai;
                currentQuest.quest.pointBonus += 25;

                
                foreach(var nama in currentQuest.quest.nama){
                    
                    if(temp == nama){
                        
                        uiS[i].border.sprite = karakterSesuai;
                        currentQuest.quest.pointBonus += 25;
                        break;
                    }
                }
            }

            if(currentQuest.quest.item != null){
                gambarItem.sprite = currentQuest.quest.item.gambarItem;
                gambarItem.color = Color.black;
                if(quest.select.itemOn){
                    gambarItem.color = Color.white;
                }
            }else{
                panelItem.SetActive(false);
            }

            pointBonusText.text = currentQuest.quest.pointBonus.ToString();
            pointText.text = currentQuest.quest.Reward.ToString();
            isRPG = currentQuest.isRPG;
            //klikResult.onClick.AddListener(() => tambahPoint(currentQuest));
        }

        void closePanel(){
            panelResutl.DOScale(Vector2.one * 1.1f, .25f).OnComplete(() => {
                panelResutl.DOScale(Vector2.zero, .5f).OnComplete(() => {
                    
                });

                //var camera = Camera.main;
            // float zoom = isActive ? -7f : -10f;
                UiManager.instance.endChinematic();
                objectPanel.SetActive(false);
                Debug.Log("Zoom Out");
                if(!isRPG){
                    FindObjectOfType<UiManager>().panelUtama.SetActive(true);
                    control.currentState(state.Default);
                    control.playerMove.move = true;
                    Camera.transform.DOLocalMoveZ(-10f, 1f);
                }
                
                
            });
            UiManager.instance.ChinematicPanel.endChinematic();
            UiManager.instance.updateEnergy(2);
            FindObjectOfType<WaktuManager>().gantiWaktu(1);
        }
        listQuest tempQuest;
        public void tambahPoint(){
            var playerProfil = GameManager.instance.profil;

            SoundManager.instance.uiSFX(2);
            playerProfil.Saldo += tempQuest.quest.Reward;

            UiManager.instance.UpdateSaldo(playerProfil.Saldo);
            tempQuest = null;
            closePanel();
            
        }
    }

    [System.Serializable]
    public class UiCharacter{
        public UnityEngine.UI.Image karakter;
        public UnityEngine.UI.Image border;
        public TMPro.TMP_Text namaKarakter;
    }
}