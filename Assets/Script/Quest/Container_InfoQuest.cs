using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Container_InfoQuest : MonoBehaviour
{

    public Material grey;
    public GameObject itemPanel;
    public GameObject parentListSkill;
    public GameObject parentListKarakter;
    public GameObject characterSelected;
    public listCharacters[] karakter = 
    {
        null,
        null
    };
    public Container_Karakter[] cKarakter =
    {
        null,
        null
    };

    int selected;

    public Button Mulai;

    List<string> allSkill = new List<string>();

    public playerProfil player;

    Quest quest;

    //listCharacters[2] chara; 
    public void initKonten(Quest _quest)
    {
        this.quest = _quest;
        //Debug.Log(quest);
        initBarang();
        spawnListKarakter();
        initListSkill();
    }

    void initBarang()
    {
        Items item = quest.item;
        //Debug.Log(item);
        if (item == null)
        {
            itemPanel.SetActive(false);
        }
        else
        {
            itemPanel.SetActive(true);
            Image barang = itemPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();

            if (item.isInventory)
            {
                barang.enabled = true;
                barang.sprite = item.gambarItem;
                barang.material = null;
            }
            else
            {
                barang.enabled = true;
                barang.material = grey;
                barang.sprite = item.gambarItem;

            }
        }
    }
    void initListSkill()
    {
        if(quest.skills != null)
        {
            parentListSkill.SetActive(true);
            for (int i = 0; i < parentListSkill.transform.childCount; i++)
            {
                parentListSkill.transform.GetChild(i).gameObject.SetActive(false);

                if(i < quest.skills.Length)
                {
                    parentListSkill.transform.GetChild(i).gameObject.SetActive(true);

                    parentListSkill.transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = quest.skills[i].ToString();
                }
            }
        }
        else
        {
            parentListSkill.SetActive(false);
        }
    }

    public void spawnListKarakter()
    {
        for(int i = 1; i < parentListKarakter.transform.childCount; i++)
        {
            Debug.Log(i);
            Container_Karakter karakterList = parentListKarakter.transform.GetChild(i).GetComponent<Container_Karakter>();
            karakterList.initKarkater(player.character[i - 1], i - 1);
        }
    }
    
    public void pilihKarakter(listCharacters characters, Container_Karakter _Karakter)
    {
        selected += 1;
        for (int i = 0; i < quest.skills.Length; i++)
        {
            for (int j = 0; j < characters.skills.Length; j++)
            {
                if (quest.skills[i] == characters.skills[j])
                {
                    string _skill = characters.skills[j];
                    allSkill.Add(_skill);
                }
            }
        }
        karakter[karakter[0] == null ? 0 : 1] = characters;
        cKarakter[cKarakter[0] == null ? 0 : 1] = _Karakter;
        selectedCharacter(selected - 1);
        updateListSkill();
        updateProfil();

        if (selected >= 2)
        {
            Mulai.gameObject.SetActive(true);
            
            Mulai.onClick.AddListener(() => mulaiGame());
            for (int x = 1; x < parentListKarakter.transform.childCount; x++)
            {
                Container_Karakter karakterList = parentListKarakter.transform.GetChild(x).GetComponent<Container_Karakter>();
                karakterList.UnInteract();
            }
        }
    }


    public void mulaiGame()
    {
        int indexQuest = quest.jmlEnergy;
        if(player.Energy >= indexQuest)
        {
            
            player.Energy -= indexQuest;
            FindObjectOfType<DemoLoadScene>().LoadScene(quest.sceneGame);
            quest.isDone = true;
            player.Saldo += quest.Reward; 
        }
        else
        {
            Debug.Log("Energy Kurang");
            //Mulai.enabled(false);
        }
    }

    void updateProfil()
    {
        for(int i = 0; i < karakter.Length; i++)
        {
            GameObject panelKarakter = characterSelected.transform.GetChild(i).gameObject;
            Image imageKarakter = panelKarakter.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            GameObject cancelSelect = panelKarakter.transform.GetChild(1).gameObject;

            if (karakter[i] != null)
            {
                imageKarakter.gameObject.SetActive(true);
                imageKarakter.sprite = karakter[i].imageCharacter;
                cancelSelect.SetActive(true);
            }
            else
            {
                imageKarakter.gameObject.SetActive(false);
                //imageKarakter.sprite = karakter[i].imageCharacter;
                cancelSelect.SetActive(false); ;

            }
        }
    }

    public void unSelectedCharacter(int index)
    { 
        cKarakter[index].setSelected(false);
        cKarakter[index] = null;
        for (int i = 0; i < karakter[index].skills.Length; i++)
        {
            allSkill.Remove(karakter[index].skills[i]);
        }
        for(int x = 1; x < parentListKarakter.transform.childCount; x++)
        {
            Container_Karakter karakterList = parentListKarakter.transform.GetChild(x).GetComponent<Container_Karakter>();
            karakterList.InteractKarakter();
        }
        selected -= 1;
        Mulai.gameObject.SetActive(false);
        karakter[index] = null;
        updateListSkill();
        selectedCharacter(index);
    }

    void selectedCharacter(int index)
    {

        if (karakter[index] != null)
        {
         //   Debug.Log(index);
            index = 0;

           // Debug.Log(index);
        }
        Image imageCharacter = characterSelected.transform.GetChild(index).GetChild(0).GetChild(0).GetComponent<Image>();
        GameObject btnClose = characterSelected.transform.GetChild(index).GetChild(1).gameObject;

        btnClose.gameObject.SetActive(karakter[index] != null);
        imageCharacter.gameObject.SetActive(karakter[index] != null);
        imageCharacter.sprite = karakter[index] != null ?  karakter[index].imageCharacter : null;

    }

    void updateListSkill()
    {
        for (int i = 0; i < quest.skills.Length; i++)
        {
            if (allSkill.Count != 0)
            {
                for (int j = 0; j < allSkill.Count; j++)
                {
                    if (quest.skills[i] != allSkill[j])
                    {
                        parentListSkill.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);
                    }
                    else
                    {
                        parentListSkill.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(true);
                        break;
                    }
                }
            }
            else
            {
                parentListSkill.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    void resetInfoQuest(int index)
    {
        if (cKarakter[index] != null)
        {
            cKarakter[index].setSelected(false);
            cKarakter[index].InteractKarakter();
        }
        selectedCharacter(index);
        cKarakter[index] = null;
        karakter[index] = null;
    }
    public void cloaseInfoQuest()
    {
        for(int i = 0; i < cKarakter.Length; i++)
        {
            resetInfoQuest(i);
        //    unSelectedCharacter(i);
        }
        for (int x = 1; x < parentListKarakter.transform.childCount; x++)
        {
            Container_Karakter karakterList = parentListKarakter.transform.GetChild(x).GetComponent<Container_Karakter>();
            karakterList.InteractKarakter();
        }
        Mulai.gameObject.SetActive(false);
        allSkill.Clear();
        updateListSkill();
        updateProfil();
        Mulai.onClick.RemoveAllListeners();
        selected = 0;
    }
}

