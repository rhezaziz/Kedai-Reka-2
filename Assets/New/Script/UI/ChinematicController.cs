using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terbaru
{
    public class ChinematicController : MonoBehaviour
    {

        public bool full;
        public bool blank;
        public bool half;

        public GameObject panelUtama;
        public bool fullChinematic()
        {
            full = true;
            return full;
        }

        public bool halfChinematic()
        {
            half = true;
            return half;
        }

        public bool fullToBlank()
        {
            full = false;
            return full;
        }

        public bool fullToHalf()
        {
            half = true;
            full = false;
            return half;
        }

        public bool halfToFull()
        {
            half = false;
            full = true;
            return full;
        }

        public bool resetChinematic()
        {
            half = false;
            full = false;
            return false;
        }

        public void startChinematic()
        {
            //if (half || full) endChinematic();

            Debug.Log("Start Dialog Controller");
            gameObject.SetActive(true);

            var controller = FindObjectOfType<Controller>();
            if (controller)
            {

                controller.currentState(state.Interaction);
                panelUtama.SetActive(false);
            }

            

        }

        public void startChinematicOtherScene()
        {
            gameObject.SetActive(true);
        }


        public void endChinematic()
        {
            Debug.Log("End Dialog controller");

            //if(half || full) UiManager.instance.Chinematic(false);


            check();
            //StartCoroutine(check());
        }

        public void endChineamticOtherScene()
        {

            if (full || half)
            {
                UiManager.instance.Chinematic(false);
            }

            gameObject.SetActive(false);
        }

        void check()
        {
            if (full || half)
            {
                UiManager.instance.Chinematic(false);
            }
            //yield return new WaitForSeconds(1f);
            if (!FindObjectOfType<MapsManager>().onLokasi)
            {
                FindObjectOfType<Controller>().currentState(state.Interaction);
                panelUtama.SetActive(true);
                FindObjectOfType<Controller>().currentState(state.Default);

            }


            gameObject.SetActive(false);


        }

        
    }

}