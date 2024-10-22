using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Terbaru;

namespace MiniGame1_1{

    public class Manager : MonoBehaviour
    {
        
        public int jumlahCharacter;
        public GameObject prefabs;
        public Transform parent;

        public GameObject panelResult;

        public Sprite Benar, Salah;

        bool selesai;

        string action = "Memilih TIM Anggran";

        public List<data> datas = new List<data>();
        // Start is called before the first frame update
        List<data> temp = new List<data>();

        public List<GameObject> indikatorSeleted = new List<GameObject>();

        List<GameObject> listPanel = new List<GameObject>();  
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
                listPanel.Add(container);
            }
        }

        public int jumlahBenar;
        public int jumlahSalah;

        public void initPanel(){
            if(temp.Count < jumlahCharacter){
                result();
                
            }else{
                foreach(var container in listPanel){

                    container.transform.SetParent(parent);
                    container.transform.localScale = Vector2.one;
                    int randomIndex = Random.Range(0, temp.Count - 1);
                    data tempData = temp[randomIndex];
                    temp.Remove(tempData);
                    container.transform.GetChild(0).gameObject.SetActive(true);
                    

                    container.GetComponent<Container_Character>().initValue(tempData);
                }
            }
        }

        void result(){
            Debug.Log("Selesai");
            selesai = true;
            foreach(var container in listPanel){
                container.SetActive(false);
            }
            
            foreach(var pilih in terpilih){
                GameObject container = Instantiate(prefabs);
                container.transform.SetParent(parent);
                container.transform.localScale = Vector2.one;
                container.SetActive(true);


                container.GetComponent<Container_Character>().result(pilih);

            }
            Debug.Log("Selesai Part 2");
            //StartCoroutine(animasiGanti());
            //QuestManager.instance.CheckAction(action);
        }

        public Sprite getSprite(bool value){
            return value ? Benar : Salah;
        }

        List<data> terpilih = new List<data>();
        public void check(data data){

            terpilih.Add(data);

            int index = terpilih.Count - 1;
            var selected = indikatorSeleted[index].transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>();
            //GameObject childSelected = selected.transform.GetChild(0).gameObject;
            foreach(var item in listPanel){
                item.GetComponent<Container_Character>().click.interactable = false;
            }
            selected.DOColor(new Color(Color.white.r, Color.white.g, Color.white.b, 1), .5f);
            selected.sprite = data.SelectedImage;

            StartCoroutine(animasiGanti());
        }

        IEnumerator animasiGanti(){
            var rectParent = parent.GetComponent<RectTransform>();

            //rectParent.pivot = new Vector2(1f, .5f);
            yield return new WaitForSeconds(1f);
            
            rectParent.DOPivot(new Vector2(1f, 0.5f), 1f);
            rectParent.DOAnchorMax(new Vector2(0, 0.5f), 1f);
            rectParent.DOAnchorMin(new Vector2(0, 0.5f), 1f);

            yield return new WaitForSeconds(1f);
            
            rectParent.anchorMin = new Vector2(1f, .5f);
            rectParent.anchorMax = new Vector2(1f, 0.5f);
            rectParent.pivot = new Vector2(0f, .5f);

            rectParent.anchoredPosition = Vector2.zero;
            //rectParent.pivot = new Vector2(.5f, .5f);
            initPanel();
            Debug.Log("Ke Tengah");
            yield return new WaitForSeconds(.5f);
            rectParent.DOPivot(Vector2.one / 2f, 1f);
            rectParent.DOAnchorMin(new Vector2(.5f, .5f), 1f);
            rectParent.DOAnchorMax( new Vector2(.5f, 0.5f), 1f);
            yield return new WaitForSeconds(1f);
            if(selesai){
                yield return new WaitForSeconds(1f);
                if(testGame)
                    balikMainMenu();
                else
                    QuestManager.instance.CheckAction(action);
            }
        }
        public bool testGame = true;
        void balikMainMenu(){
            FindObjectOfType<MainMenu>().PindahScene("New Scene");
        }

        void Update(){
            if(Input.GetKeyDown(KeyCode.Escape) && testGame){
                testGame = false;
                balikMainMenu();
            }
            
        }

        void Start(){
            mulaiGame();
        }

        public void mulaiGame()
        {
            startInit(jumlahCharacter);
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
