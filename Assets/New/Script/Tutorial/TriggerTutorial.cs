using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Terbaru{

    public class TriggerTutorial : MonoBehaviour
    {
        void Start(){
            isTutorial = GameManager.instance.isTutorial;
        }

        public bool isTutorial;
        public UnityEvent action = new UnityEvent();

        void OnTriggerEnter(Collider col){
            if(col.CompareTag("Player") && isTutorial){
                Debug.Log("Kena");
                action?.Invoke();
                
                Destroy(GetComponent<TriggerTutorial>());
            }
        }   
    }
}