using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coba : MonoBehaviour
{
    //public GameObject[] objectChanges;

    public static Coba instance;

    public List<dataObject> data;
    private void Start()
    {
       // objectChanges = GameObject.FindGameObjectsWithTag("Changes");
    }
    private void Update()
    {
        //Debug.Log(data.Count);

    }

    public void loadScene()
    {
     //   objectChanges = GameObject.FindGameObjectsWithTag("Changes");
      //  if (data.Count > 0)
       // {
            // Debug.Log("Masuk");
       // /    for (int i = 0; i < objectChanges.Length; i++)
          ///  {
        //        objectChanges[i].transform.position = data[i].position;
        //    }
      //  }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        DontDestroyOnLoad(instance);
    }

    public void setListData(GameObject objek)
    {
        if (data.Count > 0)
            data.Clear();

        data.Add(
                new dataObject
                {
                    nameObject = objek.name,
                    position = objek.transform.position
                }
            );
    }
}

[System.Serializable]
public class dataObject
{
    public string nameObject;
    public Vector3 position;
}
