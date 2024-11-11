using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
public class BukaPintu : MonoBehaviour
{
    public GameObject item;

    public Transform engselPintu;

    //public state states;

    bool isClosed = true;

    Vector3 Rotate;


    public float rotateBuka;
    private void Start()
    {
        
    }

    public void buka()
    {
        if(item != null)
        {
            item.GetComponent<Collider>().enabled = true;
        }

        Rotate = new Vector3(0f, rotateBuka, 0f);
        //Rotate = isClosed ? new Vector3(0f, 0f, 0f) : new Vector3(0f, rotateBuka, 0f);

        engselPintu.DOLocalRotate(Rotate, 1.5f);
    }
}
