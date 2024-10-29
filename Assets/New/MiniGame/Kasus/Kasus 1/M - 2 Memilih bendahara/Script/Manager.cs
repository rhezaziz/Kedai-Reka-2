using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Terbaru;
using UnityEngine.UI;

namespace MiniGame1_2{

    public class Manager : MonoBehaviour
    {
        
        public int jumlahCharacter;
        public GameObject prefabs;
        public Transform parent;

        public GameObject panelResult;

        public Sprite Benar, Salah;

        public Button checkResult;

        bool selesai;

        public string action = "Memilih TIM Anggran";

        public List<data> datas = new List<data>();
        // Start is called before the first frame update
        List<data> temp = new List<data>();

        //public List<GameObject> indikatorSeleted = new List<GameObject>();

        List<Container_Character> listPanel = new List<Container_Character>();  

        void startInit(int value){
            selesai = false;
            temp = datas;
            for(int i = 0 ; i < value; i++){
                GameObject container = Instantiate(prefabs);
                container.transform.SetParent(parent);
                container.transform.localScale = Vector2.one;
                int randomIndex = Random.Range(0, temp.Count - 1);
                data tempData = temp[randomIndex];
                temp.Remove(tempData);
                container.SetActive(true);

                container.GetComponent<Container_Character>().initValue(tempData);
                listPanel.Add(container.GetComponent<Container_Character>());
            }
        }

        void Start(){
            //mulaiGame();
        }

        public void mulaiGame()
        {
            startInit(3);
        }  

        

        public Sprite getSprite(bool value){
            return value ? Benar : Salah;
        }
        

        Container_Character terpilih = null;
        //List<data> terpilih = new List<data>();
        public void check(Container_Character data){
            if(terpilih != null){
                terpilih.unSelect();
            }

            terpilih = data;
            checkResult.interactable = true;
            //terpilih = data;

            //int index = terpilih.Count - 1;
            //var selected = indikatorSeleted[index].transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>();
            //GameObject childSelected = selected.transform.GetChild(0).gameObject;
            // foreach(var item in listPanel){
            //     item.GetComponent<Container_Character>().click.interactable = false;
            // }
            //selected.DOColor(new Color(Color.white.r, Color.white.g, Color.white.b, 1), .5f);
            //selected.sprite = data.SelectedImage;

            //StartCoroutine(animasiGanti());
        }

        public void result(){
            listPanel.Remove(terpilih);
            checkResult.gameObject.SetActive(false);
            foreach(var panel in listPanel){
                panel.notSelected();
            }

            StartCoroutine(end());
        }

        IEnumerator end(){
            yield return new WaitForSeconds(1.5f);
            animTerpilih();

            yield return new WaitForSeconds(1.5f);
            QuestManager.instance.CheckAction(action);
        }

        void animTerpilih(){
            terpilih.Terpilih();
            foreach(var panel in listPanel){
                panel.gameObject.SetActive(false);
            }

            //QuestManager.instance.CheckAction(action);
        }
        
    }
    [System.Serializable]
        public class data{
            public string Nama;
            //public List<string> sifat;
            public Sprite ImageInSelection;
            public Sprite SelectedImage;
            public bool sesuai;

            public List<sifat> sifat = new List<sifat>();   
        }

        public enum sifat{
            Jajan,
            Akuntasi,
            Rapih,
            Berbohong,
            Jujur
        }
}
