using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WaterCan : MonoBehaviour
{
    public GameObject Air;
    public Transform position;
    private void OnMouseDrag()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector2(mousePos.x, mousePos.y);
    }

    private void OnMouseUp()
    {
        transform.position = position.position;
    }

    public void EnableSiram()
    {
        transform.DORotate(new Vector3(0f, 0f, 30f), 0.5f);
        Air.SetActive(true);
    }

    public void DisableSiram()
    {
        Air.SetActive(false);
        transform.DORotate(new Vector3(0f, 0f, 0f), 0.5f);
        
    }
}
