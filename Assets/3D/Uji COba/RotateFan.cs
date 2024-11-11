using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class RotateFan : MonoBehaviour
{
    public float speedRotate;
    public Animator anim;
    public Transform Baling;
    bool rotate = true;

    private void Start(){
        anim = GetComponent<Animator>();
        //anim.speed = 0; 

    }
    private void Update(){
        if(rotate)
            Baling.Rotate(new Vector3(0f, 1f, 0f), speedRotate * Time.deltaTime);
    
    }

    void OnBecameInvisible(){
        //enabled = false;
        stopAction();
    }

    void OnBecameVisible(){
        StartAction();
    }

    void stopAction(){
        rotate = false;
        //var state = anim.GetCurrentAnimatorStateInfo(0);
        anim.speed = 0;
        //anim.Play(state.nameHash, 0, state.normalizedTime)
    }

    void StartAction(){
        
        rotate = true;
        anim.speed = 1;
    }

}
