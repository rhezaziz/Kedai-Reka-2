using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace Terbaru{
    public class Saklar : MonoBehaviour, Interaction
    {
        public bool isOn;

        public Transform tombol;
        public Transform pos;

        public Light lampu;

        public int maxIntent, minIntent;
        Transform player;

        public string namaAnimasiPlayer;

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

        bool interactable;
        public bool Interactable(){
            return interactable;
        }

        public void changeInteractable(bool value){
            interactable = value;
            // return interactable;
        }
        
        public void action(Transform Player)
        {
            Player.position = pos.position;
            player = Player;
            Player.GetComponentInChildren<Animator>().SetTrigger(namaAnimasiPlayer);

            Invoke("triggerSaklar",1f);
            
        }

        void triggerSaklar(){
            isOn = !isOn;
            int tempValue = isOn ? maxIntent : minIntent;
            float rotY = isOn ? 0f : 10f;
            lampu.intensity = tempValue;
            tombol.DORotate(new Vector3(0f, rotY, 0f), 0.1f).OnComplete(() =>
                player.GetComponent<Controller>().currentState(state.Default));
        }

        public void btnActive(GameObject btn, bool interactable){
            btn.SetActive(interactable);
            btn.GetComponent<UnityEngine.UI.Button>().interactable = Interactable();
            
            string text = isOn ? "Matikan Lampu" : "Nyalakan Lampu";
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
        }

        public void isTutorial(bool value)
        {
            //throw new System.NotImplementedException();
        }
    }
}