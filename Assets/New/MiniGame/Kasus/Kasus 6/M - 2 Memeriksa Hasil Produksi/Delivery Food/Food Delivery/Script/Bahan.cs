using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bahan : MonoBehaviour
{
    public GameObject[] ingredientsArray;
    [SerializeField] int factoryID;
    //do not need to be processed.
    public string processorTag = "";
    public bool needKatsu, needRice;
    //Private flags
    private float delayTime;                
    private bool canCreate = true;
    //Static flag
    public GameObject bowl;
    public static bool itemIsInHand;        

    void Update()
    {
        managePlayerDrag();

        if (Input.touches.Length < 1 && !Input.GetMouseButton(0) && !bowl)
        {
            itemIsInHand = false;
            
        }

        if(bowl && Input.GetMouseButtonDown(0))
        {
            if (!bowl.activeInHierarchy)
            {
                bowl.SetActive(true);
                bowl.GetComponent<Bowl>().enabled = true;
            }
                
        }
        //Debug.Log(itemIsInHand);
        //debug
        //print ("itemIsInHand: " + itemIsInHand);
    }

    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = new Color(GetComponent<Renderer>().material.color.r,
                                                GetComponent<Renderer>().material.color.g,
                                                GetComponent<Renderer>().material.color.b,
                                                0.5f);
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = new Color(GetComponent<Renderer>().material.color.r,
                                                GetComponent<Renderer>().material.color.g,
                                                GetComponent<Renderer>().material.color.b,
                                                1f);
    }

    //***************************************************************************//
    // If player has dragged on of the ingredients, make an instance of it, then
    // follow players touch/mouse position.
    //***************************************************************************//
    private RaycastHit hitInfo;
    private Ray ray;
    void managePlayerDrag()
    {
        //Mouse of touch?
        if (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Moved)
        {
            ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
        }
        else
        {
            return;
        }

        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject objectHit = hitInfo.transform.gameObject;
            if (objectHit.tag == "ingredient" && objectHit.name == gameObject.name && !itemIsInHand)
            {
                createIngredient();
            }/*
            if (objectHit.tag == "ingredient" && objectHit.name == gameObject.name && !itemIsInHand)
            {
                createIngredient();
            }*/
        }
    }
    void createIngredient()
    {
        if (canCreate)
            create();

    }

    void create()
    {
        canCreate = false;
        itemIsInHand = true;
        GameObject prod = Instantiate(ingredientsArray[factoryID - 1], transform.position + new Vector3(0, 0, -1), Quaternion.Euler(0, 0, 0)) as GameObject;
        prod.name = ingredientsArray[factoryID - 1].name;
        prod.GetComponent<BoxCollider>().enabled = false;
        prod.GetComponent<Bahan_Mover>().needKatsu = needKatsu;
        prod.GetComponent<Bahan_Mover>().needRice = needRice;
        prod.GetComponent<Bahan_Mover>().factoryID = factoryID;
        StartCoroutine(reactivate());

    }

    IEnumerator reactivate()
    {
        yield return new WaitForSeconds(delayTime);
        canCreate = true;
    }


    //***************************************************************************//
    // Play AudioClips
    //***************************************************************************//
    void playSfx(AudioClip _sfx)
    {
        GetComponent<AudioSource>().clip = _sfx;
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
    }
}
