using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Terbaru
{
    public class Player_Interaction : MonoBehaviour
    {
        bool interaksi;
        Controller controller;

        public GameObject interactButton;

        GameObject ObjectGame;
        bool animasi = false;
        public static bool Interaksi;

        private void Start()
        {
            controller = GetComponent<Controller>();

        }
        
        void OnCollisionEnter(Collision other){
            if (other != null && other.gameObject.GetComponent<Interaction>() != null)
            {
                onInteraction(true, other.gameObject);
                // Debug.Log("Kena");
                // interactButton.SetActive(true);
                // interactButton.GetComponentInChildren<UnityEngine.UI.Text>().text = other.gameObject.name;
                // interaksi = true;
                // ObjectGame = other.gameObject;

                // interactButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => interaksiAction());
            }
        }

        void OnCollisionExit(Collision other){
            if (other != null && other.gameObject.GetComponent<Interaction>() != null)
            {
                onInteraction(false, other.gameObject);
            }
        }
        // //public List<Interaksi> list_Interaksi;
        // private void OnTriggerEnter(Collider other)
        // {
        //     if (other != null && other.GetComponent<Interaction>() != null)
        //     {
        //         interactButton.SetActive(true);
        //         interactButton.GetComponentInChildren<UnityEngine.UI.Text>().text = other.name;
        //         interaksi = true;
        //         ObjectGame = other.gameObject;

        //         interactButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => interaksiAction());
        //     }
        // }

        // private void OnTriggerExit(Collider other)
        // {
        //     if (other != null && other.GetComponent<Interaction>() != null)
        //     {
        //         interactButton.SetActive(false);
        //         interaksi = false;
        //         ObjectGame = null;

        //         interactButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        //     }
        // }



        private void Update()
        {
            //interaksiAction();
        }

        public void interaksiAction()
        {
            controller.currentState(state.Interaction);
            ObjectGame.GetComponent<Interaction>().action(transform);

            
            /*if (Input.GetKeyDown(KeyCode.Space) && interaksi)
            {
                //Debug.Log("Kena");
            }*/
        }

        public void onInteraction(bool interact, GameObject other){
            interactButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
            interactButton.SetActive(interact);
            interactButton.GetComponentInChildren<UnityEngine.UI.Text>().text = other.gameObject.name;
            interaksi = interact;
            ObjectGame = interact ? other.gameObject : null;

            interactButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => interaksiAction());
        }

        public void ChangeAnimation(string boolean, string trigger)
        {
            Animator anim = GetComponentInChildren<Animator>();
            animasi = !animasi;

            if (!animasi)
                anim.SetTrigger(trigger);

            anim.SetBool(boolean, animasi);
        }
    }

    //Ambil Item
    interface Ambil
    {
        public void ambilBarang();
    }

    public enum Interaksi
    {
        Kucing,
        Masak,
        Ngobrol,
        Tanaman,
        Siram_Tanaman,
        Nonton
    }

}
