
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEnum : MonoBehaviour
{
    public Kemampuan[] Misi;

    public Kemampuan[] character;

    public Transform parent;
    public List<Kemampuan> indexSkill;
    public List<int> listInt; 
    private void Start()
    {
        initial();
    }
    public void check()
    {
        for(int i = 0; i <  Misi.Length; i++)
        {
          
            for(int j = 0; j <  character.Length; j++)
            {
       
                if (Misi[i] == character[j])
                {
                    indexSkill.Add(character[j]);
                    updateList(i);
                }
            }
        }
    }

    void updateList(int index)
    {
        
        parent.transform.GetChild(index).GetChild(0).GetChild(0).gameObject.SetActive(true);
    }

    void initial()
    {
        for(int i = 0; i < parent.childCount; i++)
        {
            parent.transform.GetChild(i).gameObject.SetActive(false);

            if(i < Misi.Length)
            {
                parent.transform.GetChild(i).gameObject.SetActive(true);
                parent.transform.GetChild(i).GetChild(1).GetComponent<TMPro.TMP_Text>().text = Misi[i].ToString();
            }
        }
    }
}
