using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMaterial : MonoBehaviour
{

    Manager manager;
    public float minDistance;

    public bool closet;
    private void Start()
    {
        manager = FindObjectOfType<Manager>(); 
    }
    private void Update()
    {
        closeMaterial();
    }


    void closeMaterial()
    {
        GameObject[] materials = GameObject.FindGameObjectsWithTag("Material");
        
        foreach(var mat in materials)
        {
            if (mat.GetComponent<MaterialObject>().isMoving)
            {
                float distance = Vector2.Distance(transform.position, mat.transform.position);

                if(distance < minDistance)
                {
                    GetComponent<SpriteRenderer>().color = manager.close;
                    closet = true;
                }
                else
                {
                    closet = false;
                    GetComponent<SpriteRenderer>().color = manager.basic;
                }
            }
        }
    }
}
