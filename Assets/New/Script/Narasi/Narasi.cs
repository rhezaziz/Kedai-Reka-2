using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terbaru{
    public class Narasi : MonoBehaviour, IDialog
    {
        public NarasiDialog dialog;

        public Camera cameraUtama;
        public GameObject Sekre;

        public Controller player;
        public GameObject Perempuan;

        public List<Kasus> listKasus = new List<Kasus>();
        public int indexKasus = 0;
        

        Dialog tempDialog;
        NarasiDialog narasiDialog;

        public bool chinematic = false;

        NarasiDialog getNarasi(Dialog temp){
            foreach(var narasi in listKasus[indexKasus].dataNarasi){
                if(narasi.narasi == temp){
                    return narasi;
                }
            }

            return null;
        }

        public void haveNarasi(int index){
            indexKasus = index;
            startDialog();
        }

        

        public void startDialog(){
            narasiDialog = listKasus[indexKasus].dataNarasi[0];
            tempDialog = narasiDialog.narasi;
            
            FindObjectOfType<Player_Interaction>().interactObject = this.gameObject; 

            if(!chinematic){
                FindObjectOfType<Controller>().currentState(state.Interaction); 
                      
                UiManager.instance.startChinematic();
            
                UiManager.instance.panelUtama.SetActive(false);
            
            }

            
            chinematic = true;
            if(narasiDialog.dialogWithPerempuan){
                StartCoroutine(mulaiDialogOnAsrama());
            } else{
                StartCoroutine(mulai());
            }

            FindObjectOfType<DialogManager>().panelDialog.SetActive(true);
           
        }

        public void pindahSekre(){
            StartCoroutine(mulai());
            UiManager.instance.startChinematic();
            
            UiManager.instance.panelUtama.SetActive(false);
        }

        public void startNarasi(Dialog temp){
            
            dialog = getNarasi(temp);
            //tempDialog = dialog;

            FindObjectOfType<Player_Interaction>().interactObject = this.gameObject; 

            if(!chinematic){
                FindObjectOfType<Controller>().currentState(state.Interaction); 
                      
                UiManager.instance.Chinematic(true);
            
                UiManager.instance.panelUtama.SetActive(false);
            }
            
            chinematic = true;
            //FindObjectOfType<DialogManager>().StartDialog(tempDialog);
            // 
            FindObjectOfType<DialogManager>().panelDialog.SetActive(true);
            if(narasiDialog.inAsrama){
                 if(narasiDialog.dialogWithPerempuan){
                    StartCoroutine(mulaiDialogOnAsrama());
                    return;
                 }
                FindObjectOfType<DialogManager>().StartDialog(dialog.narasi, null);
           } else{
                StartCoroutine(mulai());
           }
        }

        public void startNarasi(int temp){
            narasiDialog = listKasus[indexKasus].dataNarasi[temp];
            tempDialog = narasiDialog.narasi;
            //tempDialog = dialog;

            //FindObjectOfType<Player_Interaction>().interactObject = this.gameObject; 
            
            //FindObjectOfType<Controller>().currentState(state.Interaction); 
                      
            //UiManager.instance.Chinematic(true);
            
            //UiManager.instance.panelUtama.SetActive(false);
            
            //FindObjectOfType<DialogManager>().StartDialog(tempDialog);
            
            //FindObjectOfType<DialogManager>().StartDialog(tempDialog);
            FindObjectOfType<DialogManager>().panelDialog.SetActive(true);
            startNarasi(tempDialog);
        }

        IEnumerator mulai(){
            FindObjectOfType<DialogManager>().closeDialog();
            if(!chinematic){
                UiManager.instance.startChinematic();
            }
            yield return new WaitForSeconds(2f);

            UiManager.instance.Chinematic(true);

            yield return new WaitForSeconds(1f);
            cameraUtama.gameObject.SetActive(false);
            Sekre.SetActive(true);
            yield return new WaitForSeconds(1f);
            UiManager.instance.Chinematic(true);

            yield return new WaitForSeconds(1f);
            FindObjectOfType<DialogManager>().StartDialog(tempDialog, null);
            
        }

        public void PerempuanNgomong(bool value){
                Perempuan.transform.localPosition = new Vector3(4f, 0f, 0f);
                var perem = Perempuan.transform.GetChild(0);
                perem.GetComponent<SpriteRenderer>().flipX = true;
                perem.GetComponent<Animator>().SetBool("Ngomong", value);
        }

        IEnumerator mulaiDialogOnAsrama(){
            //UiManager.instance.Chinematic(true);
            FindObjectOfType<DialogManager>().playEvent();
            UiManager.instance.ChinematicPanel.endChinematic();
            player.currentState(state.Interaction);
            player.GetComponentInChildren<SpriteRenderer>().flipX = true;
            player.GetComponentInChildren<Animator>().SetBool("Ngomong", true);
            UiManager.instance.startChinematic();
            UiManager.instance.panelUtama.SetActive(false);
            if(narasiDialog.dialogWithPerempuan){
                Perempuan.SetActive(true);
                PerempuanNgomong(true);
            }

            yield return new WaitForSeconds(2f);
            FindObjectOfType<DialogManager>().StartDialog(tempDialog, null);
        }

        IEnumerator kembali(){
            FindObjectOfType<DialogManager>().closeDialog();

                PerempuanNgomong(false);
                //UiManager.instance.Chinematic(false, 1.75f);
                Perempuan.SetActive(false);
                
                // player.GetComponentInChildren<SpriteRenderer>().flipX = false;
            player.GetComponentInChildren<Animator>().SetBool("Ngomong", false);
            FindObjectOfType<DialogManager>().closeDialog();
            UiManager.instance.startChinematic();
            yield return new WaitForSeconds(1f);
            UiManager.instance.Chinematic(true);

            yield return new WaitForSeconds(1f);
            Sekre.SetActive(false);
            cameraUtama.gameObject.SetActive(true);
            player.setPlayerOnFrontDoor();
            Debug.Log("Ke Asrama");
            UiManager.instance.endChinematic();
            yield return new WaitForSeconds(1f);
            UiManager.instance.ChinematicPanel.endChinematic();
            closeDialog();
            FindObjectOfType<WaktuManager>().gantiWaktu(1);
            // UiManager.instance.Chinematic(false);

            // yield return new WaitForSeconds(1f);

            // // UiManager.instance.Chinematic(true);

            // yield return new WaitForSeconds(1f);
            //  FindObjectOfType<DialogManager>().StartDialog(tempDialog);

        }

        

        
        

        public void nextDialog(Dialog dialog){
            tempDialog = dialog;
            FindObjectOfType<DialogManager>().StartDialog(dialog, null);
            chinematic = true;
        }
        public void nextDialog(NarasiDialog dialog){
            tempDialog = dialog.narasi;
            narasiDialog = dialog;
            FindObjectOfType<DialogManager>().StartDialog(tempDialog, null);
            chinematic = true;
        }

        public void endDialog(){

            


            if(tempDialog.quest.proses.Length == 0 && tempDialog.events.GetPersistentEventCount() == 0)
            {
                if (narasiDialog.dialogWithPerempuan)
                {
                    //FindObjectOfType<DialogManager>().closeDialog();

                    PerempuanNgomong(false);
                    UiManager.instance.endChinematic(1.75f);
                    Perempuan.SetActive(false);

                    // player.GetComponentInChildren<SpriteRenderer>().flipX = false;
                    player.GetComponentInChildren<Animator>().SetBool("Ngomong", false);
                    //player.currentState(state.Default);
                }

                Debug.Log("Kembali");
                if(!narasiDialog.inAsrama){
                    StartCoroutine(kembali());
                    return;
                }

                closeDialog();
                    
            }


            
            else if(tempDialog.quest.proses.Length > 0){
                Debug.Log("End Dialog Have Quest");
                //FindObjectOfType<DialogManager>()
                FindObjectOfType<DialogManager>().closeDialog();
                listQuest quest = tempDialog.quest;
                QuestManager.instance.StartQuest(quest);
                chinematic = false;

                if (narasiDialog.dialogWithPerempuan)
                {
                    //FindObjectOfType<DialogManager>().closeDialog();

                    PerempuanNgomong(false);
                    UiManager.instance.endChinematic(1.75f);
                    Perempuan.SetActive(false);

                    // player.GetComponentInChildren<SpriteRenderer>().flipX = false;
                    player.GetComponentInChildren<Animator>().SetBool("Ngomong", false);
                    //player.currentState(state.Default);
                }

                //return;
            } else if(tempDialog.events.GetPersistentEventCount() > 0){
                Debug.Log("Invoke");

                FindObjectOfType<DialogManager>().playEvent();

                tempDialog.events?.Invoke();
                return;
            }
           

            

            
            
        }

        public void closeDialog(){
            //FindObjectOfType<DialogManager>().closeDialog();


            //Debug.Log("Close Dialog");
            chinematic = false;
            UiManager.instance.endChinematic();
            //UiManager.instance.Chinematic(false);
            
            UiManager.instance.panelUtama.SetActive(true);
            //;
            
            FindObjectOfType<Player_Interaction>().interactObject = null;
            
            QuestManager.instance.CheckAction("Narasi");
            Invoke("startAction",1f);

            FindObjectOfType<DialogManager>().closeDialog();
            //gameObject.SetActive(false);
            //Debug.Log("Close Dialog 2 ");
        }

        void startAction(){
            FindObjectOfType<Controller>().currentState(state.Default);

            UiManager.instance.ChinematicPanel.endChineamticOtherScene();
            //OnAction.Invoke();
            
        }
    }

    [System.Serializable]
    public class Kasus{
        public int indexKasus;

        public bool firstDialogWithDinda;
        public List<NarasiDialog> dataNarasi;
    }
    
}
