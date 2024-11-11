using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MiniGame5_3{
    public class Keyboard : MonoBehaviour
    {
        void OnMouseDown(){
            FindObjectOfType<Manager>().typing(true);
        }

        void OnMouseUp(){
            FindObjectOfType<Manager>().typing(false);
        }
    }
}
