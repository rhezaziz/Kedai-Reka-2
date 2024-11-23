using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Terbaru
{
    public class Manager_Ending : MonoBehaviour
    {
        public Dialog_Object dialog;

        public static Manager_Ending instance;

        public List<DialogObjectClick> Wargas = new List<DialogObjectClick>();

        public GameObject ChinematicPanel;

        public SoundManager sound;

        public Transform map;
        public float speed;

        // public void moveMap(){
        //     map.position = Vector3.Lerp(transform.position, CameraPositionFinish.transform.position, Time.deltaTime * speed);
        // }

        public void moveCamera()
        {
            // A coroutine runs every frame until it stops returning values
            StartCoroutine(MoveCamera());
        }

        private IEnumerator MoveCamera()
        {
            // check the distance and see if we still need to move towards the destination ​
            while(Vector3.Distance(transform.position, map.transform.position) > 1.0f){

            

                transform.position = Vector3.Lerp(transform.position, map.transform.position, Time.deltaTime * speed);
        
            // Return  nothing meaningful and wait until next frame​
                yield return null;
            }
        }
        private void Awake()
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }

        IEnumerator Start()
        {
            //GameManager.instance.DestroyThisObject();
            yield return new WaitForSeconds(2f);
            sound.endGame();
            yield return new WaitForSeconds(2f);
            
//            Chinematic(false);
            //GameManager.instance.DestroyThisObject();
            //QuestManager.instance.StartQuest(quest);

            FindObjectOfType<Dialog_Ending>().startDialog();
        }

        public void onDialogWarga(DialogObjectClick dialogWarga)
        {
            foreach (var obj in Wargas)
            {
                if(obj != dialogWarga)
                {
                    obj.interactNPC(false);
                }
            }
        }
        public void tambahMain(DialogObjectClick thisObj)
        {
            thisObj.interactNPC(false);
            Wargas.Remove(thisObj);
            StartCoroutine(JedaDialog());
            
        }


        IEnumerator JedaDialog()
        {
            yield return new WaitForSeconds(1f);
            
            //QuestManager.instance.StartQuest(quest);
            if (Wargas.Count <= 0)
            {
                FindObjectOfType<Dialog_Ending>().startDialog(dialog);
            }
            else
            {
                foreach (var obj in Wargas)
                {
                    obj.interactNPC(true);
                }
            }
            //FindObjectOfType<Dialog_Ending>().startDialog();
        }
        public void startEnding()
        {
           
            StartCoroutine(CountStartEnding());
            
        }

        IEnumerator CountStartEnding()
        {
            yield return new WaitForSeconds(2f);
            //GameManager.instance.DestroyThisObject();
            //QuestManager.instance.StartQuest(quest);
            foreach (var warga in Wargas)
            {
                warga.interactNPC(true);
            }
            //FindObjectOfType<Dialog_Ending>().startDialog();
        }


        public void Chinematic(bool isActive)
        {
            string _action = isActive ? "Mulai" : "Reverse";

            var anim = ChinematicPanel.GetComponent<Animator>();
            Debug.Log("chinematic" + " "+_action);
            anim.SetTrigger(_action);
        }

        public void chinematicWithaouCam(bool isActive)
        {
            string _action = isActive ? "Mulai" : "Reverse";

            var anim = ChinematicPanel.GetComponent<Animator>();
            //Debug.Log("chinematic without C" + " "+_action);
            //Debug.Log("chinematic");
            anim.SetTrigger(_action);
            Debug.Log("Without cam" + _action);
        }

    }
}