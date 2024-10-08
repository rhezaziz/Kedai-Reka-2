using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru
{
    public class Controller : MonoBehaviour
    {
        public state state;

        public Movement playerMove;

        public Player_Interaction interaction;
        public playerProfil profil;


        public List<Mimik> mimik;
        public List<Mimik> mimikPlayer;


        public void currentState(state current)
        {
            switch(current)
            {
                case state.Interaction:
                    playerMove.move = false;
                    state = state.Interaction;
                    Debug.Log("Player Move : "+ playerMove.move);
                    break;

                case state.Default:
                    playerMove.move = true;
                    state = state.Default;
                    Debug.Log("Player Move : "+ playerMove.move);
                    break;
            }
        }
    }

    public enum state
    {
        Default,
        Interaction,
    }
}
