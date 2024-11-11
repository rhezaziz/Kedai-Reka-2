using System.Collections;
using System.Collections.Generic;
using Terbaru;
using UnityEngine;

namespace MiniGame5_3{
    public class Manager : MonoBehaviour
    {

        public float waktu;
        public float seluruhWaktu;
        public Collider2D keyboard;
        [TextArea(5,10)]
        public string isi;
        private Queue<string> sentences;

        [Header("UI")]
        public TMPro.TMP_Text displayText;
        public UnityEngine.UI.Image barValue;
        public TMPro.TMP_Text valueText;
        public UnityEngine.UI.Scrollbar slide;
        public SpriteRenderer[] onClickKeyboard;

        [Header("Sound")]
        public AudioSource suara;
        public AudioClip ngetik;

        // Start is called before the first frame update
        int jumlahDialog;
        float testJumlah;

        public string action;
        void Start()
        {
            sentences = new Queue<string>();
            sentences.Clear();
            sentences.Enqueue(isi);
            test();
            jumlahDialog = sentences.Count;
            waktu = seluruhWaktu / sentences.Count;
            
        }

        void test(){
            var sentence = sentences.Dequeue();
            foreach(char letter in sentence.ToCharArray())
            {
                // string temp = letter.ToString();
                sentences.Enqueue(letter.ToString());
                //displayText.text += letter;
                //temp.ToCharArray() -= letter;
                //isi
                //yield return new WaitForSeconds(waktu);
            }
        }

        bool done;

        public void typing(bool value){
            if(done)
                return;

            if(value){
                //var sentence = sentences.Dequeue();
                
                StartCoroutine(TypeSentence());
                suara.Play();
            }
            else if(!value){
                //sentences.Clear();
                //sentences.Enqueue(isi);
                onClickKeyboard[tempIndex].enabled = false;
                StopAllCoroutines();
                suara.Stop();
            }
        }
        int tempIndex = 0;
        IEnumerator TypeSentence()
        {
            //dialogText.text = "";
           // done = false;
            //string temp = sentence;
            
            while(sentences.Count >= 1){
                var letter = sentences.Dequeue();
                displayText.text += letter;
                int randomIndex = Random.Range(0, onClickKeyboard.Length - 1);
                
                onClickKeyboard[tempIndex].enabled = false;
                onClickKeyboard[randomIndex].enabled = true;
                tempIndex = randomIndex;
                
                testJumlah = jumlahDialog - sentences.Count;

                barValue.fillAmount = testJumlah / jumlahDialog;
                float persen = barValue.fillAmount * 100;
                valueText.text = $"{(int)persen}%";
                slide.value = 0;
                //temp.ToCharArray() -= letter;
                //isi
                yield return new WaitForSeconds(waktu);
            }
            
            typing(false);
            done = true;
            Invoke("GameOver",2f);
            Debug.Log(done);
            keyboard.enabled = false;
            yield return new WaitForSeconds(2f);
            //GameOver();
            // foreach(char letter in sentence.ToCharArray())
            // {
            //     displayText.text += letter;
            //     //temp.ToCharArray() -= letter;
            //     //isi
            //     yield return new WaitForSeconds(waktu);
            // }

            
        }

        void GameOver (){
            Debug.Log("GameOver");
            QuestManager.instance.currentQuest.quest.pointBonus += 50;
            QuestManager.instance.CheckAction(action);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
