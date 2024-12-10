using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


namespace Terbaru
{
    public class ChinematicController : MonoBehaviour
    {

        public bool full;
        public bool blank;
        public bool half;

        public GameObject panelUtama;
        public Animator anim;
        //
        public bool fullToHalf()
        {
            half = true;
            full = false;
            return half;
        }

        //
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

        public void startChinematicOtherScene()
        {
            gameObject.SetActive(true);
        }

        #region Chinematic With Cam
        public void startChinematic()
        {
            //Debug.Log("Start Dialog Controller");
            gameObject.SetActive(true);

            var controller = FindObjectOfType<Controller>();
            if (controller)
            {
                controller.currentState(state.Interaction);
                panelUtama.SetActive(false);
            }

            Chinematic(true);

        }
        public void endChinematic()
        {
            if (full || half)
            {
                Chinematic(false);
            }
            //yield return new WaitForSeconds(1f);
            if (!FindObjectOfType<MapsManager>().onLokasi)
            {
                FindObjectOfType<Controller>().currentState(state.Interaction);
                panelUtama.SetActive(true);
                FindObjectOfType<Controller>().currentState(state.Default);

            }
            Debug.Log("End Chinematic Controller");
            gameObject.SetActive(false);
        }

        public void endChineamticOtherScene()
        {

            if (full || half)
            {
                UiManager.instance.Chinematic(false);
            }

            gameObject.SetActive(false);
        }
        #endregion


        #region Without Cam
        public void startChinematicWithoutCam()
        {
            //Debug.Log("Start Dialog Controller");
            gameObject.SetActive(true);

            var controller = FindObjectOfType<Controller>();
            if (controller)
            {
                controller.currentState(state.Interaction);
                panelUtama.SetActive(false);
            }

            chinematicWithaouCam(true);

        }
        public void endChinematicWithoutCam()
        {
            if (full || half)
            {
                chinematicWithaouCam(false);
            }
            //yield return new WaitForSeconds(1f);
            if (!FindObjectOfType<MapsManager>().onLokasi)
            {
                FindObjectOfType<Controller>().currentState(state.Interaction);
                panelUtama.SetActive(true);
                FindObjectOfType<Controller>().currentState(state.Default);

            }
            Debug.Log("End Chinematic Controller");
            gameObject.SetActive(false);
        }

        public void endChineamticOtherSceneWithoutCam()
        {

            if (full || half)
            {
                chinematicWithaouCam(false);
            }

            gameObject.SetActive(false);
        }

        #endregion
        public void Chinematic(bool isActive)
        {
            var camera = Camera.main;
            float zoom = isActive ? -7f : -10f;
            camera.transform.DOLocalMoveZ(zoom, 1f);
            string _action = isActive ? "Mulai" : "Reverse";

            var anim = GetComponent<Animator>();
            //Debug.Log("chinematic");
            anim.SetTrigger(_action);
        }

        public void chinematicWithaouCam(bool isActive)
        {
            string _action = isActive ? "Mulai" : "Reverse";

            var anim = GetComponent<Animator>();
            //Debug.Log("chinematic");
            anim.SetTrigger(_action);
            Debug.Log("Without cam" + _action);
        }
    }
}