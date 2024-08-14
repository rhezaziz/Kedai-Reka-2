using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
namespace Terbaru{
    public class Misi_Manager : MonoBehaviour
    {
    // Start is called before the first frame update
        public Transform parent;

        public List<Quest> quests;
        public playerProfil player;
        public GameObject prefabsKarakter;
        public GameObject parentListKarakter;

        public Vector3 PosKiri;
        public Vector3 PosKanan;
        public UnityEngine.UI.Button closeButton;
        private void Start()
        {
            player = FindObjectOfType<Controller>().profil;
            closeButton.onClick.AddListener(() => FindObjectOfType<Controller>().currentState(state.Default));
            initQuestList();
            initKarakter();
        }
        public void initQuestList()
        {
        
            for(int i = 0; i < parent.childCount; i++)
            {
                Container_Quest container = parent.GetChild(i).GetComponent<Container_Quest>();
            
                if (!quests[i].isDone)
                {
                    container.gameObject.SetActive(true);

                    container.initContent(quests[i], i);

                }
                else
                {
                    container.gameObject.SetActive(false);
                }
            }
        }

        List<GameObject> Karakters = new List<GameObject>();
        void initKarakter()
        {
            for (int i = 0; i < player.character.Count; i++)
            {
                GameObject GO_karakter = Instantiate(prefabsKarakter);
                Container_Karakter listKarakter = GO_karakter.GetComponent<Container_Karakter>();
                Karakters.Add(GO_karakter);
                listKarakter.initKarkater(player.character[i], i);
                GO_karakter.transform.SetParent(parentListKarakter.transform);
                GO_karakter.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        public void setUpPositionNPC(List<GameObject> NPC){
            Vector3 player = GameObject.Find("Player").transform.position;
            //Debug.Log($"Posisi Player = {player}");
            Vector3[] posisi = {
                new Vector3(30f, 36f, -122.5f),
                new Vector3(37.5f, 36f, -122.5f),
                
            };
            for(int i = 0; i < NPC.Count; i++){

                bool flip = i == 1 ;
                //Debug.Log($"Posisi {i} = {posisi[i]}");
                NPC[i].transform.position = posisi[i];
                NPC[i].GetComponent<NPC_Controller>().selectedQuest(flip);
            }

            UiManager.instance.mulaiQuest(NPC);
        }

        public void UpdateListKarater(){
            for(int i = 0; i < Karakters.Count; i++){
                var temp = Karakters[i].GetComponent<Container_Karakter>();

                if(temp.isLock != player.character[i].characterLock){
                    temp.initKarkater(player.character[i], i);
                }
            }
        }
    }
}