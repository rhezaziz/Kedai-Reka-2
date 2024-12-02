using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Terbaru{

    public class Interaksi_Animasi : MonoBehaviour, Interaction
    {
        public bool tutorialState;
        [SerializeField] private string namaAnimasi;
        [SerializeField] private string namaAnimasiPlayer;

        public bool flipX;

        public string namaAction;

        public GameObject pointer;

        public bool value;

        public AudioSource suara;

        public void isTutorial(bool temp){
            //enabled = temp;
        }

        public void suaraPlay(){
            suara.Play();
        }

        public void suaraStop(){
            suara.Stop();
        }

        public void suaraObject(int index){
            SoundManager.instance.sfx(index);
        }

        
        void OnDisable(){
            value = false;
        }
        
        void OnEnable(){
            value = true;
        }


        bool interactable;
        public bool Interactable(){
            return interactable;
        }

        public void changeInteractable(bool value){
            interactable = value;
            //return interactable;
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


        public void action(Transform Player){

            //tutorialState = GameManager.instance.isTutorial;
           // FindObjectOfType<Controller>().currentState(state.Interaction);
            
            UiManager.instance.startChinematic(-.25f);
            Player.GetComponent<Controller>().changeRunTimeController(true);
            Player.GetComponentInChildren<SpriteRenderer>().flipX = flipX;
            GetComponent<Collider>().enabled = false;
            float PosX = transform.GetChild(0).position.x;
            float PosY = Player.transform.position.y;
            float PosZ = transform.position.z;

            Player.transform.position = new Vector3(PosX, PosY, PosZ);
            GetComponent<Animator>().SetTrigger(namaAnimasi);
            Player.GetComponentInChildren<Animator>().SetTrigger(namaAnimasiPlayer);
            pointer.SetActive(false);
            
        }

        public void btnActive(GameObject btn, bool interactable){
            suara.PlayOneShot(suara.clip);
            btn.GetComponent<UnityEngine.UI.Button>().interactable = Interactable();
            
            btn.SetActive(interactable);
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = namaAction;
        }

        public void AnimasiEnd(){
            //FindObjectOfType<Controller>().currentState(state.Default);
            Player.GetComponent<Controller>().changeRunTimeController(false);
            GetComponent<Collider>().enabled = true;

            UiManager.instance.endChinematic(1.75f);

            if(tutorialState){
                tutorialState = false;
                FindObjectOfType<TutorialManager>().EndAnimation();
            }

            UiManager.instance.ChinematicPanel.endChinematic();
        }
    }
}
