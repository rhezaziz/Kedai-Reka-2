using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialObject : MonoBehaviour
{
    private string namaMaterial = null;
    Manager manager;
    GameObject place;
    Vector2 posAwal;
    public bool isMoving;
    List<GameObject> objects = new List<GameObject>();

    //Directory<GameObject, float> test = new Directory<GameObject, float>();
    bool canMoving = true;
    private void Start()
    {
        namaMaterial = gameObject.name;
        manager = FindObjectOfType<Manager>();
        posAwal = transform.position;
        GetComponent<SpriteRenderer>().material = manager._data(namaMaterial).mat;
    }

    private void OnMouseDrag()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePos.x, mousePos.y);
        GetComponent<BoxCollider2D>().size = Vector2.one / 2f; 
    }

    private void OnMouseUp()
    {
        if (!place)
        {
            GetComponent<BoxCollider2D>().size = Vector2.one;
            transform.position = posAwal;
        }
        else
        {
            place.GetComponentInParent<SpriteRenderer>().enabled = true;
            place.GetComponent<SpriteRenderer>().material = manager._data(namaMaterial).mat;
            place.GetComponent<Collider2D>().enabled = false;
            gameObject.SetActive(false);
            manager.placement.Add(namaMaterial);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision && collision.CompareTag("Place"))
        {
            objects.Add(collision.gameObject);
            place = closedObject();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision && collision.CompareTag("Place"))
        {
            collision.GetComponent<SpriteRenderer>().color = manager.basic;
            objects.Remove(collision.gameObject);

            place = closedObject();
            //place = closedObject();
        }
    }
    List<float> distances = new List<float>();
    GameObject closedObject()
    {
        if(objects.Count > 1)
        {
            distances = new List<float>();
            foreach(var obj in objects)
            {
                float distance = Vector2.Distance(transform.position, obj.transform.position);
                obj.GetComponent<SpriteRenderer>().color = manager.basic;
                distances.Add(distance);
            }
            for(int i = 0; i < objects.Count; i++)
            {
                for(int x = 0; x < objects.Count; x++)
                {
                    if (distances[i] > distances[x])
                    {
                        var tempObject = objects[i];
                        objects[i] = objects[x];
                        objects[x] = tempObject;

                        var tempDistance = distances[i];
                        distances[i] = distances[x];
                        distances[x] = tempDistance;

                    }
                }
            }
            
        }
        if(objects.Count != 0)
        {

            objects[0].GetComponent<SpriteRenderer>().color = manager.close;
            return objects[0];
        }

        else
        {
            return null;
        }
    }
}
