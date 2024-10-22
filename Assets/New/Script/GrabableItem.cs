using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Terbaru{

    public class GrabableItem : MonoBehaviour, Interaction
    {
        public bool clickAbleObject;
        public bool paket;
        public List<Items> item;
        public string actionQuest;

        public void isTutorial(bool value){
            
        }
        public void action(Transform Player){
            //var profil = GameManager.instance.profil;
            Player.GetComponent<Controller>().currentState(state.Default);
            //var temp = profil.item.Contains(item);
            foreach(var i in item){
                i.isInventory = true;
                string action = $"{actionQuest} {i.namaItem}";
                QuestManager.instance.CheckAction(action);
            }

            AudioClip clip = paket ? SoundManager.instance.InvorenmentClip[5] : SoundManager.instance.sfxUIClip[7];
            SoundManager.instance.sfx(clip);
            //
            gameObject.SetActive(false);
            //Destroy(this);
        }

        public void btnActive(GameObject btn, bool interactable){
            btn.SetActive(interactable);
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Ambil Barang";
        }
        
        void OnDisable(){
            FindObjectOfType<Player_Interaction>().onInteraction(false, null);
        }

        void OnMouseDown(){
            if(clickAbleObject)
                action(null);
        }
    }

}