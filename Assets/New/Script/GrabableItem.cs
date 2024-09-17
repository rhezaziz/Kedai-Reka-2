using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Terbaru{

    public class GrabableItem : MonoBehaviour, Interaction
    {
        public List<Items> item;
        public string actionQuest;

        public bool isTutorial(){
            return true;
        }
        public void action(Transform Player){
            //var profil = GameManager.instance.profil;

            //var temp = profil.item.Contains(item);
            foreach(var i in item){
                i.isInventory = true;
                string action = $"{actionQuest} {i.namaItem}";
                QuestManager.instance.CheckAction(action);
            }
             
            Player.GetComponent<Controller>().currentState(state.Default);
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
    }

}