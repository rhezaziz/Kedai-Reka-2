using System.Collections;
using System.Collections.Generic;
using Terbaru;
using UnityEngine;

namespace MiniGame3_3{

    public class Manager : MonoBehaviour
    {
        public Sprite Benar, Salah;
        public List<int> pilihan = new List<int>();

        public List<int> jawaban = new List<int>();

        List<GameObject> buttons = new List<GameObject>();
        
        public int index;

        public void pilih(int value, GameObject objectButton){
            pilihan.Add(value);
            buttons.Add(objectButton);
            index += 1;
        }

        public void resetValue(){
            pilihan.Clear();
            index = 0;
            foreach(var button in buttons){
                button.GetComponent<SelectedUI>().resetValue();
            }
            buttons.Clear();
        }

        public void check(){


            if(jawaban.Count <= pilihan.Count){

                StartCoroutine(hasil());
            //     for(int i = 0; i < pilihan.Count; i++){
            //         if(jawaban[i] == pilihan[i]){
            //             Debug.Log("Benar");
            //             break;
            //         }

            //         Debug.Log("Salah");
            //     }
             }
        }
        public string action;
        IEnumerator hasil(){
            for(int i = 0; i < pilihan.Count; i++){

                Sprite tempSprite = Salah;
                if(jawaban[i] == pilihan[i]){
                    tempSprite = Benar;
                    QuestManager.instance.currentQuest.quest.pointBonus += 50;
                }

                buttons[i].GetComponent<SelectedUI>().hasilAkhir(tempSprite);
                yield return new WaitForSeconds(1.75f);
            }

            
            yield return new WaitForSeconds(2f);
            QuestManager.instance.CheckAction(action);
            
        }

       
    }
}
