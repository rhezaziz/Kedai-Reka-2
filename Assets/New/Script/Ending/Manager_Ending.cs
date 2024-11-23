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

        private void Awake()
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }

        IEnumerator Start()
        {
            yield return new WaitForSeconds(4f);
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
            //GameManager.instance.DestroyThisObject();
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
            //Debug.Log("chinematic");
            anim.SetTrigger(_action);
        }

        public void chinematicWithaouCam(bool isActive)
        {
            string _action = isActive ? "Mulai" : "Reverse";

            var anim = ChinematicPanel.GetComponent<Animator>();
            //Debug.Log("chinematic");
            anim.SetTrigger(_action);
            Debug.Log("Without cam" + _action);
        }

    }
}