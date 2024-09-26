using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Terbaru
{

    public class Pintu : MonoBehaviour, Interaction
    {
        public UnityEvent extendAction;
        public Vector3 PosUI;
        public Transform spawn;
        public GameObject Trigger;

        public Transform engselPintu;

         
        Vector3 Rotate;


        public float rotateBuka;

        public bool value;

        public void isTutorial(bool temp){
            enabled = temp;
        }
        void OnDisable(){
            value = false;
        }
        
        void OnEnable(){
            value = true;
        }

        void Start(){

        }
        public void action(Transform Player)
        {
            Player.transform.position = new Vector3(spawn.position.x, Player.position.y, spawn.position.z);

            if (Trigger != null)
            {
                Trigger.GetComponent<Collider>().enabled = false;
            }

            Rotate = new Vector3(0f, rotateBuka, 0f);
            //Rotate = isClosed ? new Vector3(0f, 0f, 0f) : new Vector3(0f, rotateBuka, 0f);

            //engselPintu.DOLocalRotate(Rotate, 1.5f).OnComplete(() => Player.GetComponent<Controller>().currentState(state.Default));
            engselPintu.DOLocalRotate(Rotate, 1.5f).OnComplete(() => extendAction?.Invoke());
            
        }

        public void tutupPintu(){
            
            engselPintu.DOLocalRotate(Vector3.zero, 1.5f)
            .OnComplete( () =>
                FindObjectOfType<Controller>().currentState(state.Default))
                ;

            Trigger.GetComponent<Collider>().enabled = true;
        }

        public void btnActive(GameObject btn, bool interactable){
            btn.SetActive(interactable);
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Buka Pintu";
        }

        public void tutup(){
            var video = FindObjectOfType<VideoManager>();
            float duration = (float)video.video.length;
            Invoke("tutupPintu", duration + 6f);
        }
    }

}