using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Nyapu : MonoBehaviour
{
    public Transform[] noda;
    public Transform pos;

    public float value;

    public Animator anim;
    public GameObject Debu;
    public bool isNyapu;
    private void Start()
    {
       // anim = GetComponent<Animator>();
       // StartCoroutine(NyapuNoda(0));
    }

    

    IEnumerator NyapuNoda(GameObject debu)
    {
        SpriteRenderer nodaSprite = debu.GetComponent<SpriteRenderer>();
        float temp = nodaSprite.color.a;
        Color currecntColor = nodaSprite.color;
        //Sequence anim = DOTween.Sequence();

        //anim.SetLoops(1, LoopType.Yoyo);
        while (nodaSprite.color.a > 0 && isNyapu)
        {
            anim.SetBool("Nyapu", true);
            Debu.SetActive(true);
            temp -= value;
            currecntColor.a = temp;

            nodaSprite.color = currecntColor;

            yield return new WaitForSeconds(0.1f);

        }
        if(!(nodaSprite.color.a > 0))
        {
            NyapuManager.instance.changeSkor(debu);
            Debu.SetActive(false);
            anim.SetBool("Nyapu", false);
            transform.DORotate(Vector3.zero, 0.1f);

        }
        StopCoroutine(NyapuNoda(debu));
    }


    private void OnMouseDrag()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector2(mousePos.x, mousePos.y);
    }

    private void OnMouseUp()
    {
        transform.position = pos.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Noda"))
        {
            
            isNyapu = true;
            //_debu = collision.gameObject;
            StartCoroutine(NyapuNoda(collision.gameObject));
        
            //Debug.Log("Enter");
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Noda"))
        {
            //StopAllCoroutines();
            
            StopCoroutine(NyapuNoda(collision.gameObject));
            //_debu = null;
            // Debu.SetActive(false);
            isNyapu = false;
            Debu.SetActive(false);

            anim.SetBool("Nyapu", false);
            transform.DORotate(Vector3.zero, 0.1f);
        }
    }
}
