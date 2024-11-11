using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terbaru{
    
    public class Jalur : MonoBehaviour
    {
        public LineRenderer line;

        public Transform Player;
        public Transform pos;


        void Start(){
            line.SetPosition(0, new Vector3(Player.position.x, -10f, Player.position.z));
            line.SetPosition(1, new Vector3(pos.position.x, -10f, pos.position.z));
        }
    }
}