using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using food;

namespace Terbaru{

public class RekrutManager : MonoBehaviour
    {
        //public listCharacters[] data;
        public playerProfil data;
        public GameObject container;
        public GameObject Popup;
        
        public TMP_Text currentPoint;
        public Material mat;
        public GameObject panelList;
        public Transform parent;

        public Button ClosePanelRekrut;
        

        bool first = false;
        void Start(){
            
            ClosePanelRekrut.onClick.AddListener(() => FindObjectOfType<Controller>().currentState(state.Default));
        }

        public void rekrutKarakter(listCharacters chara){
            chara.characterLock = true;

            display(data);
        }

        List<GameObject> listProfil = new List<GameObject>();
        public static Material matTemp;
        public void display(playerProfil profil){
            List<listCharacters> temp = profil.character;
            data = profil;
            matTemp = mat;
            NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
            currentPoint.text = "Point : " + profil.Saldo.ToString("n0", info);
            
            if(listProfil.Count == temp.Count){
                updateListCharacter(temp);
                return;
            }

            for(int i = 0; i < temp.Count; i++){
                if (temp[i].onComputer)
                {
                    GameObject panel = Instantiate(container);
                    panel.SetActive(temp[i].onComputer);
                    listProfil.Add(panel);
                    panel.SetActive(true);
                    panel.name = i.ToString();

                    var character = panel.GetComponent<Container_Rekrut>();
                    character.initData(temp[i]);

                    panel.transform.SetParent(parent);
                    panel.transform.localScale = Vector2.one;
                }
                
            }
        }

        void updateListCharacter(List<listCharacters> characters){
            for(int i = 0; i < characters.Count; i++){
                if (characters[i].onComputer)
                {
                    var temp = listProfil[i].GetComponent<Container_Rekrut>();
                    if (temp.isLock != characters[i].characterLock)
                    {
                        temp.initData(characters[i]);
                    }
                }
            }
        }


        [Header("Lihat Profil")]
        public GameObject panelInfo;
        public Image FotoProfil;
        public TMP_Text namaProfil;
        public TMP_Text TitleProfil;
        public TMP_Text skillProfil;
        public TMP_Text DeskripsionProfil;
        public Button beliProfil;
        public void displayLihatProfil(GameObject index){
            int tempIndex = int.Parse(index.name);
            panelInfo.SetActive(true);
            var dataInfo = data.character[tempIndex];
            FotoProfil.sprite = dataInfo.imageInfoCharacter;
            namaProfil.text = dataInfo.namaCharacter;
            TitleProfil.text = dataInfo.titleCharacter;
            skillProfil.text = dataInfo.skills[0].ToString();
            DeskripsionProfil.text = dataInfo.deskripsiKarakter;

            if (!dataInfo.characterLock)
            {
                beliProfil.gameObject.SetActive(false);
                FotoProfil.material = null;
            }
            else
            {
                beliProfil.onClick.RemoveAllListeners();

                beliProfil.gameObject.SetActive(true);
                beliProfil.onClick.AddListener(() => rekrut(tempIndex));
                NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
                beliProfil.transform.GetChild(0).GetComponent<TMP_Text>().text = dataInfo.cost.ToString("n0", info);
                FotoProfil.material = mat;
            }
        }

        void rekrut(int index)
        {   
            var temp = data.character[index];
            int saldo = data.Saldo;
            int harga = temp.cost;

            popUpMuncul(saldo >= harga);
            
            if (saldo >= harga)
            {
                beliProfil.gameObject.SetActive(false);
                data.Saldo -= harga;
                FotoProfil.material = null;
                temp.characterLock = false;
                //updateListCharacter(index);
                UiManager.instance.UpdateSaldo(data.Saldo);
                beliProfil.gameObject.SetActive(false);
                GameManager.instance.updateCharacter(index);
                //Debug.Log("Berhasil");
                //FindObjectOfType<UIManager>().updateCoint();
                display(data);
            }
            else
            {
                Debug.Log("Point Kurang");
            }
        
        }

        [Header("Pop up")]
        public GameObject popUp;
        public Button okBtn;
        public Button btnKembali;
        public TMP_Text textpopUp;
        void popUpMuncul(bool succes)
        {
            popUp.SetActive(true);
            GameObject button = popUp.transform.GetChild(0).GetChild(1).GetChild(1).gameObject;
            //Button btnOK = popUp.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>();
            //TMP_Text textPopUp = popUp.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            button.SetActive(false);
            okBtn.transform.GetChild(0).GetComponent<TMP_Text>().text = "OK";
            okBtn.onClick.RemoveAllListeners();
            okBtn.onClick.AddListener(() => popUp.SetActive(false));

            okBtn.onClick.AddListener(() => okBtn.onClick.RemoveAllListeners());
            textpopUp.text = succes ? "Berhasil Rekrut Karakater" : "Saldo Kurang Untuk Rekrut Karakter";
        }

        public void popUpMuncul(GameObject index)
        {
            int tempIndex = int.Parse(index.name);

            var temp = data.character[tempIndex];
            
            popUp.SetActive(true);
            //Button btnKembali = popUp.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Button>();
            //Button btnOK = popUp.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>();
            //TMP_Text textPopUp = popUp.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            NumberFormatInfo info = new CultureInfo("de-de", false).NumberFormat;
            string hargaText = temp.cost.ToString("n0", info);

            textpopUp.text = "Apa anda yakin rekrut karakter ?";
            btnKembali.gameObject.SetActive(true);
            btnKembali.onClick.AddListener(() => okBtn.onClick.RemoveAllListeners());
            btnKembali.onClick.AddListener(() => popUp.SetActive(false));
            btnKembali.onClick.AddListener(() => btnKembali.onClick.RemoveAllListeners());
            okBtn.transform.GetChild(0).GetComponent<TMP_Text>().text = hargaText;
            okBtn.onClick.AddListener(() => rekrut(tempIndex));
            //btnOK.
        }

        void closeInfo()
        {
            //Beli.GetComponent<Button>().onClick.RemoveAllListeners();

            //Kembali.GetComponent<Button>().onClick.RemoveAllListeners();

        }
    }
}