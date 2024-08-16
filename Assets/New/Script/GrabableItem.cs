using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Terbaru{

    public class GrabableItem : MonoBehaviour, Interaction
    {
        public Items item;
        public string actionQuest;
        public void action(Transform Player){
            //var profil = GameManager.instance.profil;

            //var temp = profil.item.Contains(item);
            item.isInventory = true;
            Player.GetComponent<Controller>().currentState(state.Default);
            QuestManager.instance.CheckAction(actionQuest);
            gameObject.SetActive(false);
            //Destroy(this);
        }
        
        void OnDisable(){
            FindObjectOfType<Player_Interaction>().onInteraction(false, this.gameObject);
        }
    }

}