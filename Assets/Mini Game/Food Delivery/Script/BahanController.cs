using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BahanController : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            klik();
        }
    }
    Ray ray;
    Vector2 rayOrigin;
    RaycastHit2D hit;
    void klik()
    {

        if (Input.GetMouseButtonDown(0))
        {
            rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
        }

        if(hit.collider != null)
        {
            Debug.Log(hit.transform.name);
        }

        /*if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }*/
        /*else if (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Moved)
        {
            ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        }*/

        
    }
}
