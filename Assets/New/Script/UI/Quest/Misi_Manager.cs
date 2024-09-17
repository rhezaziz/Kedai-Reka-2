using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Terbaru{
    public class Misi_Manager : MonoBehaviour
    {
        public Transform parent;

        public bool Tutorial;
        //public List<Quest> quests;
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

        #region  Tutorial
        void initQuestTutorial(){
            for(int i = 0; i < parent.childCount; i++)
            {
                Container_Quest container = parent.GetChild(i).GetComponent<Container_Quest>();
                Quest temp = QuestManager.instance.getQuest(0);
                 container.gameObject.SetActive(false);

                 container.initContent(temp, 0);
                // if (!temp.isDone)
                // {
                //     container.gameObject.SetActive(true);

                //     container.initContent(temp, i);
                // }
                // else
                // {
                    
                // }
            }
            initKarakter();
        }

        public void initKontenQuest(){

            if(Tutorial){
                initQuestTutorial();
                return;
            }

            initQuestList();
            UpdateListKarater();
        }

        #endregion
        void initQuestList()
        {
            for(int i = 0; i < parent.childCount; i++)
            {
                Container_Quest container = parent.GetChild(i).GetComponent<Container_Quest>();
                Quest temp = QuestManager.instance.getQuest(i);
                if (!temp.isDone)
                {
                    container.gameObject.SetActive(true);

                    container.initContent(temp, i);
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
                GO_karakter.name = $"Karakter {player.character[i].objectNPC.name}";
                listKarakter.initKarkater(player.character[i], i);
                GO_karakter.transform.SetParent(parentListKarakter.transform);
                GO_karakter.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        

        void UpdateListKarater(){
            Debug.Log("Quest ListKarakter");
            for(int i = 0; i < Karakters.Count; i++){
                var temp = Karakters[i].GetComponent<Container_Karakter>();

                if(temp.isLock != player.character[i].characterLock){
                    temp.initKarkater(player.character[i], i);
                }
            }
        }
    }
}