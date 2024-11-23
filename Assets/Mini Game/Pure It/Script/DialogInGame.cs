using System.Collections;
using System.Collections.Generic;
using Check;
using UnityEngine;
using UnityEngine.Events;

namespace Terbaru
{
    public class DialogInGame : MonoBehaviour, IDialog
    {

        public Dialog dialog;

        public UnityEvent events;
        public void startDialog()
        {
            //dialog = FindObjectOfType<NPCDialogManager>().tempDialog(indexDialog(), nama);

            //FindObjectOfType<Player_Interaction>().interactObject = this.gameObject;
            //FindObjectOfType<Controller>().currentState(state.Interaction);

            Manager_Ending.instance.Chinematic(true);

            //UiManager.instance.panelUtama.SetActive(false);

            //GetComponent<NPC_Controller>().currentCondition(animasi.Ngobrol);S

            FindObjectOfType<DialogManager>().StartDialog(dialog, this.gameObject);
            

        }

        public void StartDialog(Dialog_Object obj)
        {
            dialog.dialogObject = obj;
            startDialog();
        }

        public void endDialog()
        {

            Manager_Ending.instance.Chinematic(false);
            //Debug.Log("End");
            //GetComponent<NPC_Controller>().currentCondition(animasi.Idle);

            //UiManager.instance.panelUtama.SetActive(true);
            //;

            //FindObjectOfType<Player_Interaction>().interactObject = null;

            //FindObjectOfType<QuestManager>().CheckAction(tempAction);
            //Invoke("startAction", 1f);
            events?.Invoke();
            FindObjectOfType<DialogManager>().closeDialog();
            //if (!ending) Manager_Ending.instance.startEnding();
            //else if (ending) FindObjectOfType<MiniGame>().pindahDialogToMiniGameKampung("water Purify");
            //gameObject.SetActive(false);
        }


        //public void startDialog(Dialog_Object dialigOBJ)
        //{
        //    dialog.dialogObject = dialigOBJ;
        //    ending = true;

        //    startDialog();
        //}

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}