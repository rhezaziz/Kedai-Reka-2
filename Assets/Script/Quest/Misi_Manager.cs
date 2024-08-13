using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Misi_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform parent;

    public List<Quest> quests;
    public playerProfil player;
    public GameObject prefabsKarakter;
    public GameObject parentListKarakter;
    private void Start()
    {
        initQuestList();
        initKarakter();
    }
    public void initQuestList()
    {
        
        for(int i = 0; i < parent.childCount - 2; i++)
        {
            Container_Quest container = parent.GetChild(i+1).GetComponent<Container_Quest>();
            
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

    void UpdateListKarater(){
        for(int i = 0; i < Karakters.Count; i++){

        }
    }
}
