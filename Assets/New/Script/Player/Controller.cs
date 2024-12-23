using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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

        public void setPlayerOnFrontDoor(){
            transform.localPosition = profil.position;
        }

        public void setDefault(){
            currentState(state.Default);
        }


        public RuntimeAnimatorController controllerPerempuan;
        public RuntimeAnimatorController controllerLaki;

        public RuntimeAnimatorController tempRunTimeAnimator;
        public void GantiPerempuan(bool value)
        {
            RuntimeAnimatorController tempController = value ? controllerPerempuan : controllerLaki;
            tempRunTimeAnimator = tempController;
            transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = tempController;
        }

        public void changeRunTimeController(bool defaultValue)
        {
            RuntimeAnimatorController tempController = defaultValue ? controllerLaki : tempRunTimeAnimator;
            transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = tempController;
        }

        


        public void currentState(state current)
        {
            switch(current)
            {
                case state.Interaction:
                    playerMove.move = false;
                    state = state.Interaction;
                    //Debug.Log("Player Move : "+ playerMove.move);
                    break;

                case state.Default:
                    playerMove.move = true;
                    state = state.Default;
                    //Debug.Log("Player Move : "+ playerMove.move);
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
