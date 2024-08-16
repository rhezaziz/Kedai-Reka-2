using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru{

    public class PapanQuest : MonoBehaviour, Interaction
    {
        public Transform Berdiri;

        public GameObject quest;
        public void action(Transform player){
            player.position = new Vector3(Berdiri.position.x, player.position.y, Berdiri.position.z);
            FindObjectOfType<Misi_Manager>().initQuestList();
            quest.SetActive(true);
        }
    }
}