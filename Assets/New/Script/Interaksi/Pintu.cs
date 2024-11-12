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
        public UnityEvent ExtraAction;
        public Vector3 PosUI;
        public Transform spawn;
        public GameObject Trigger;

        public Transform engselPintu;

        

         
        Vector3 Rotate;


        public float rotateBuka;

        public bool value;

        public void isTutorial(bool temp){
           // enabled = temp;
        }
        void OnDisable(){
            value = false;
        }
        
        void OnEnable(){
            value = true;
        }
        public Transform Player;
        public GameObject point;
        public float distancePlayer;
        public void checkDistance(){
            float distance = Vector3.Distance(transform.position, Player.position);

            if(distancePlayer >= distance){
                point.gameObject.SetActive(true);
            }else{
                point.gameObject.SetActive(false);
            } 
        }

        void Update(){
            checkDistance();
        }

        public bool interact;
        public bool Interactable(){
            return interact;
        }

        public void changeInteractable(bool value){
            interact = value;
            // return interactable;
        }
        public void action(Transform Player)
        {
            Player.transform.position = new Vector3(spawn.position.x, Player.position.y, spawn.position.z);

            if (Trigger != null)
            {
                Trigger.GetComponent<Collider>().enabled = false;
            }

            Rotate = new Vector3(0f, rotateBuka, 0f);
            SoundManager.instance.sfx(29);
            engselPintu.DOLocalRotate(Rotate, 1.5f).OnComplete(() => {
                extendAction?.Invoke();
                ExtraAction?.Invoke();
                });
            
        }

        public void tutupPintu(){
            
            engselPintu.DOLocalRotate(Vector3.zero, 1.5f)
            .OnComplete( () => {
                //FindObjectOfType<Controller>().currentState(state.Default);
                SoundManager.instance.sfx(30);
                })
                ;

            Trigger.GetComponent<Collider>().enabled = true;
        }

        public void btnActive(GameObject btn, bool interactable){
            btn.SetActive(interactable);
            btn.GetComponent<UnityEngine.UI.Button>().interactable = Interactable();
            
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Buka Pintu";
        }

        public void tutup(){
            var video = FindObjectOfType<VideoManager>();
            float duration = (float)video.video.length;
            Invoke("tutupPintu", duration + 7f);
        }
    }

}