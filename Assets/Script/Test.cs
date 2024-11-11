using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject[] testObject;
    // Start is called before the first frame update
    void Start()
    {
        changePositionObject();    
    }

    private void Awake()
    {

        testObject = GameObject.FindGameObjectsWithTag("Changes");

    }

    public void changePositionObject()
    {
        int length = Coba.instance.data.Count;
        if(length > 0)
        {
            for (int i = 0; i < testObject.Length; i++)
            {
                testObject[i].transform.position = Coba.instance.data[i].position;
            }
        }
        
    }


    public void saveData()
    {
        for(int i = 0; i < testObject.Length; i++)
        {
            Coba.instance.setListData(testObject[i]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
