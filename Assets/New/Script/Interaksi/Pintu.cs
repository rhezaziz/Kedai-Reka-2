using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terbaru
{

    public class Pintu : MonoBehaviour, Interaction
    {

        public Vector3 PosUI;
        public Transform spawn;
        public GameObject Trigger;

        public Transform engselPintu;

         
        Vector3 Rotate;


        public float rotateBuka;


        public void action(Transform Player)
        {
            Player.transform.position = new Vector3(spawn.position.x, Player.position.y, spawn.position.z);

            if (Trigger != null)
            {
                Trigger.GetComponent<Collider>().enabled = false;
            }

            Rotate = new Vector3(0f, rotateBuka, 0f);
            //Rotate = isClosed ? new Vector3(0f, 0f, 0f) : new Vector3(0f, rotateBuka, 0f);

            engselPintu.DOLocalRotate(Rotate, 1.5f).OnComplete(() => Player.GetComponent<Controller>().currentState(state.Default));
    
        }

        public void BukaPintu()
        {

        }

        /*public void TutupPintu()
        {

        }*/

        public void Pintu_Utama()
        {

        }

        public void Pintu_Kamar()
        {

        }

        public void Pintu_WC()
        {

        }
    }

}