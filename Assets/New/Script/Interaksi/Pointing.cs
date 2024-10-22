using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Pointing : MonoBehaviour
{
    public bool isX, isY, isZ;

    public float jarak; 
    public float timer1, timer2;

    void OnEnable(){
        Vector3 posAwal = transform.position;
        transform.DOMove(new Vector3(tempJarak(posAwal.x, isX), 
                                    tempJarak(posAwal.y, isY), 
                                    tempJarak(posAwal.z, isZ)), timer1).OnComplete(() => {
                                        transform.DOMove(posAwal, timer2);
                                    }).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        // Sequence anim = DOTween.Sequence();
        // anim.Append(transform.DOMove(new Vector3(tempJarak(posAwal.x, isX), 
        //                             tempJarak(posAwal.y, isY), 
        //                             tempJarak(posAwal.z, isZ)), timer1));
        // anim.Append(transform.DOMove(posAwal, timer2));
        // anim.Append(transform.DOMove(posAwal, .5f).SetLoops(-1));
        
        
    }

    float tempJarak(float value, bool temp){
        if(temp){
            return value + jarak;
        }
        return value;
    }
}
