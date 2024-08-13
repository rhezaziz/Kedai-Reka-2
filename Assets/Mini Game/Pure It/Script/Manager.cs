using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public List<data> datas = new List<data>();

    public Color basic, close;

    public List<string> Correct = new List<string>();
    public List<string> placement = new List<string>();

    public data _data(string value)
    {
        foreach(var data in datas)
        {
            if(data.name == value)
            {
                return data;
            }
        }
        return null;
    }


    public void StartGame()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

}

[System.Serializable]
public class data
{
    public Material mat;
    public string name;
}
