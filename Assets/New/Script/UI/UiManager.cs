using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using DG.Tweening;

namespace Terbaru
{
    public class UiManager : MonoBehaviour
    {
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
        void Start(){
            var profil = GameManager.instance.profil; 

            Nama.text = profil.NamaKarakter;

            UpdateSaldo(profil.Saldo);
            updateEnergy(0);

            string temp = string.Format(test, "Rheza");
            Debug.Log(temp);
        }

        public void UpdateSaldo(int saldo){
            NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
            Saldo.text = "Rp" + saldo.ToString("n0", info);
        }

        public void updateEnergy(int value){
            var profil = GameManager.instance.profil;
            profil.Energy -= value;
            int jmlEnergy = profil.Energy;
            Debug.Log(jmlEnergy);
            for(int i = 0; i < bar.Length; i++){
                bar[i].gameObject.SetActive(i < jmlEnergy);
            }
        }

        void Awake(){
            instance = this;
        }

        public void displayRekrut(playerProfil profil){
            rekrut.gameObject.SetActive(true);

            rekrut.display(profil);
        }

        public void displayQuest(){
            quest.gameObject.SetActive(true);

            quest.UpdateListKarater();
        }

        public void mulaiQuest(List<GameObject> temp){
            QuestManager.instance.NPCs = temp;
            StartCoroutine(Cutscene(temp));
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

        IEnumerator Cutscene(List<GameObject> NPCs){
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
            ChinematicPanel.SetActive(false);
            FindObjectOfType<Movement>().move = true;
        }
    }
}
