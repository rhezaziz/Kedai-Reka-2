using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using DG.Tweening;
using Unity.VisualScripting;

namespace Terbaru
{
    public class UiManager : MonoBehaviour
    {
        public TMPro.TMP_Text bantuan;
        public string test;
        public static UiManager instance;
        public RekrutManager rekrut;

        public Misi_Manager quest;

        public GameObject ChinematicPanel;
        public GameObject MarkPrefabs;

        [Header("Profil")]
        public GameObject panelUtama;
        public Image[] bar;
        public TMPro.TMP_Text Saldo;
        public TMPro.TMP_Text Nama;

        Camera cam;

        void Awake(){
            instance = this;

            cam = Camera.main;
        }
        void Start(){
            //camera = Camera.main;
            var profil = GameManager.instance.profil; 

            Nama.text = profil.NamaKarakter;

            UpdateSaldo(profil.Saldo);
            updateEnergy(0);

        }

        public void bantuanText (string text){
            RectTransform panelText = bantuan.transform.parent.GetComponent<RectTransform>();

            float posY = text != "" ? 0f : 1f;

            panelText.DOPivotY(posY, .5f);
            bantuan.text = text;
        }

        public void UpdateSaldo(int saldo){
            NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
            Saldo.text = "" + saldo.ToString("n0", info);
        }

        public void updateEnergy(int value){
            var profil = GameManager.instance.profil;
            
            int jmlEnergy = Mathf.Clamp(profil.Energy -= value, 0, 3);
            Debug.Log(jmlEnergy);
            for(int i = 0; i < bar.Length; i++){
                bar[i].gameObject.SetActive(i < jmlEnergy);
            }
        }

        public void displayRekrut(playerProfil profil){
            rekrut.gameObject.SetActive(true);

            rekrut.display(profil);
        }

        public void displayQuest(){
            Debug.Log("Display Quest");
            quest.gameObject.SetActive(true);

            //quest.UpdateListKarater();
        }

        public void mulaiQuest(List<GameObject> temp, Quest quest){
            QuestManager.instance.NPCs = temp;

            if(quest.firstChangeScene){
            
                StartCoroutine(cutMiniGame(temp, quest));
                
            }
            else
            {
                StartCoroutine(Cutscene(temp, quest));
            }

        }

        IEnumerator cutMiniGame(List<GameObject> temp, Quest quest){
            FindObjectOfType<MiniGame>().pindahMiniGame(quest.sceneGame);

            yield return new WaitForSeconds(4f);
            QuestManager.instance.StartProcessQuest(quest); 
            foreach(var NPC in temp)
                    NPC.SetActive(false);
        }

        public void helperQuest(GameObject _object){
            GameObject Mark = Instantiate(MarkPrefabs);
            Mark.transform.SetParent(_object.transform);
            Mark.name = "Mark";
            

            Mark.transform.localPosition = new Vector3(0f, 
                                            0f,  
                                            0.025f);
        }

        public void closeHelper(GameObject _object){
            GameObject mark = _object.transform.Find("Mark").gameObject;
            Destroy(mark);
        }

        public void Chinematic(bool isActive){
            var camera = Camera.main;
            float zoom = isActive ? -7f : -10f;
            camera.transform.DOLocalMoveZ(zoom, 1f);
            string _action = isActive ? "Mulai" : "Reverse";

            var anim = ChinematicPanel.GetComponent<Animator>();
            //Debug.Log("chinematic");
            anim.SetTrigger(_action);
        }

        public void chinematicDialog(bool isActive){
             var camera = Camera.main;
            string _action = isActive ? "Mulai" : "Reverse";

            var anim = ChinematicPanel.GetComponent<Animator>();
            //Debug.Log("chinematic");
            anim.SetTrigger(_action);
        }

        public void Chinematic(bool isActive, float positionY){
            var camera = Camera.main;
            float zoom = isActive ? -7f : -10f;
            
            string _action = isActive ? "Mulai" : "Reverse";

            var anim = ChinematicPanel.GetComponent<Animator>();
            anim.SetTrigger(_action);
            Vector3 posCamera = new Vector3(camera.transform.localPosition.x, positionY, zoom);
            camera.transform.DOLocalMove(posCamera, 1f);
            panelUtama.SetActive(!isActive);  
            state temp = isActive ? state.Interaction : state.Default;
            FindObjectOfType<Controller>().currentState(temp);          
        }

        IEnumerator Cutscene(List<GameObject> NPCs, Quest quest){
            Animator anim = ChinematicPanel.GetComponent<Animator>();
            var camera = Camera.main;
            panelUtama.SetActive(false);
            ChinematicPanel.SetActive(true);
            camera.transform.DOLocalMoveZ(-7f, 1f);

            anim.SetTrigger("Mulai");


            yield return new WaitForSeconds(3f);

            anim.SetTrigger("Mulai");
            yield return new WaitForSeconds(2f);
            
            QuestManager.instance.StartProcessQuest(quest);
            foreach(var NPC in NPCs)
                NPC.SetActive(false);

            anim.SetTrigger("Mulai");
            camera.transform.DOLocalMoveZ(-10f, 1f);
            yield return new WaitForSeconds(1f);

            panelUtama.SetActive(true);
            ChinematicPanel.SetActive(false);
            //FindObjectOfType<Movement>().move = true;
            FindObjectOfType<Controller>().currentState(state.Default);
        }




        IEnumerator CutsceneMiniGame(List<GameObject> NPCs){
            Animator anim = ChinematicPanel.GetComponent<Animator>();
            var camera = Camera.main;
            panelUtama.SetActive(false);
            ChinematicPanel.SetActive(true);
            camera.transform.DOLocalMoveZ(-7f, 1f);

            anim.SetTrigger("Mulai");


            yield return new WaitForSeconds(3f);

            anim.SetTrigger("Mulai");
            yield return new WaitForSeconds(2f);
            foreach(var NPC in NPCs)
                NPC.SetActive(false);

            anim.SetTrigger("Mulai");
            camera.transform.DOLocalMoveZ(-10f, 1f);
            yield return new WaitForSeconds(1f);

            panelUtama.SetActive(true);
            //ChinematicPanel.SetActive(false);
            FindObjectOfType<Movement>().move = true;
        }


        
    }
}
