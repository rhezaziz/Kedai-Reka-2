using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace Terbaru
{
    public class movementMap : MonoBehaviour
    {
        public Button Kanan, Kiri;
        bool isKanan, isKiri;
        public Rigidbody2D rg;

        public float tambah;

        public float max, min;
        public void Interactable(bool value){
            Kanan.interactable = value;
            Kiri.interactable = value;
        }

        void FixedUpdate(){
            if(isKanan){
                MovePaddleRight();
                //Kiri.interactable = false;
            }

            if(isKiri){
                MovePaddleLeft();
                //Kanan.interactable = false;
            }
        }

        public void KananDown(){
            isKanan = true;
        }

        public void KananUp(){
            isKanan = false;
        }

        public void KiriDown(){
            isKiri = true;
        }

        public void KiriUp(){
            isKiri = false;
        }



        public void MovePaddleRight()
        {
            if(rg.position.x <= max)
                rg.position = (rg.position + new Vector2(tambah, 0) * Time.deltaTime);
        }

        public void MovePaddleLeft()
        {
            if(rg.position.x >= min)
                rg.position = (rg.position + new Vector2(-tambah, 0) * Time.deltaTime);
        }    
    }    
}

