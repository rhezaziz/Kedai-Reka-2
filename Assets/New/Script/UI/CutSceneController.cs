using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terbaru
{
    public class CutSceneController : MonoBehaviour
    {
        public ChinematicController controller;

        public void startChinematic()
        {
            Debug.Log("Start Chinematic");
            controller.startChinematic();
            //Chinematic(true);
        }

        public void endChinematic()
        {
            controller.endChinematic();
        }
    }

}
