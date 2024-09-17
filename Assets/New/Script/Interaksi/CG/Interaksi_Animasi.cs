using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru{

    public class Interaksi_Animasi : MonoBehaviour, Interaction
    {
        [SerializeField] bool tutorialState;
        [SerializeField] private string namaAnimasi;
        [SerializeField] private string namaAnimasiPlayer;

        public bool flipX;

        public string namaAction;

        public GameObject pointer;

        public bool value;

        public bool isTutorial(){
            return value;
        }

        
        void OnDisable(){
            value = false;
        }
        
        void OnEnable(){
            value = true;
        }


        void Start(){

        }

        public void action(Transform Player){
            FindObjectOfType<Controller>().currentState(state.Interaction);
            
            UiManager.instance.Chinematic(true, -.25f);
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
            btn.SetActive(interactable);
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = namaAction;
        }

        public void AnimasiEnd(){
            FindObjectOfType<Controller>().currentState(state.Default);
            GetComponent<Collider>().enabled = true;
            UiManager.instance.Chinematic(false, 1.75f);

            if(tutorialState){
                tutorialState = false;
                FindObjectOfType<TutorialManager>().EndAnimation();
            }
        }
    }
}
