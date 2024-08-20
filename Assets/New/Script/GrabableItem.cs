using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Terbaru{

    public class GrabableItem : MonoBehaviour, Interaction
    {
        public List<Items> item;
        public string actionQuest;
        public void action(Transform Player){
            //var profil = GameManager.instance.profil;

            //var temp = profil.item.Contains(item);
            foreach(var i in item){
                i.isInventory = true;
                QuestManager.instance.CheckAction($"{actionQuest} {i.namaItem}");
               
            }
             
            Player.GetComponent<Controller>().currentState(state.Default);
            gameObject.SetActive(false);
            //Destroy(this);
        }
        
        void OnDisable(){
            FindObjectOfType<Player_Interaction>().onInteraction(false, this.gameObject);
        }
    }

}