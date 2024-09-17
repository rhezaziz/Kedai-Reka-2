using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Terbaru{

    public class TriggerTutorial : MonoBehaviour
    {
        public UnityEvent action = new UnityEvent();

        void OnTriggerEnter(Collider col){
            if(col.CompareTag("Player")){
                Debug.Log("Kena");
                action?.Invoke();
                
                Destroy(GetComponent<TriggerTutorial>());
            }
        }   
    }
}