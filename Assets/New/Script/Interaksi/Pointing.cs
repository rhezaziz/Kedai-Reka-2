using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Pointing : MonoBehaviour
{
    public bool isX, isY, isZ;

    public float jarak; 
    public float timer1, timer2;
    public Vector3 posAwal;

    public Animator anim;
    private void Awake()
    {
        posAwal = transform.localPosition;
        anim = GetComponent<Animator>();
    }
    void OnEnable(){

        anim.SetBool("Pointing", true);

        //Vector3 tempPos = posAwal;

        //transform.DOLocalMoveY(tempPos.y + jarak, timer1).OnComplete(() =>
        //{
        //    transform.DOLocalMoveY(tempPos.y, timer2)
        //        .SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        //});
        
        //transform.DOMove(new Vector3(tempJarak(posAwal.x, isX), 
        //                            tempJarak(posAwal.y, isY), 
        //                            tempJarak(posAwal.z, isZ)), timer1).OnComplete(() => {
        //                                transform.DOMove(posAwal, timer2);
        //                            }).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        // Sequence anim = DOTween.Sequence();
        // anim.Append(transform.DOMove(new Vector3(tempJarak(posAwal.x, isX), 
        //                             tempJarak(posAwal.y, isY), 
        //                             tempJarak(posAwal.z, isZ)), timer1));
        // anim.Append(transform.DOMove(posAwal, timer2));
        // anim.Append(transform.DOMove(posAwal, .5f).SetLoops(-1));
        
        
    }

    private void OnDisable()
    {
        anim.SetBool("Pointing", false);
    }

    float tempJarak(float value, bool temp){
        if(temp){
            return value + jarak;
        }
        return value;
    }
}
