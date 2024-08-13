using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terbaru
{
    public class Controller : MonoBehaviour
    {
        public state state;

        public Movement playerMove;
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
                    break;

                case state.Default:
                    playerMove.move = true;
                    state = state.Default;
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
