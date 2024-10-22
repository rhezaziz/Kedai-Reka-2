using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace Terbaru
{
    public class Player_Interaction : MonoBehaviour
    {
        public Controller controller;

        public GameObject interactButton;
        public GameObject interactObject;

        //GameObject ObjectGame;
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
            }
        }

        void OnCollisionExit(Collision other){
            if (other != null && other.gameObject.GetComponent<Interaction>() != null)
            {
                onInteraction(false, other.gameObject);
            }
        }

        public void interaksiAction(GameObject ObjectGame)
        {
            interactObject = ObjectGame;
            controller.currentState(state.Interaction);
            ObjectGame.GetComponent<Interaction>().action(transform);
        }

        public void onInteraction(bool interact, GameObject other){
            interactButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
            if(other != null){
                other.GetComponent<Interaction>().btnActive(interactButton, interact);

                interactButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => interaksiAction(other));
                return;
            }

            interactButton.SetActive(interact);
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
