using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Terbaru{
    public class DailyQuest : MonoBehaviour, Interaction
    {

        public listQuest quest;

        public MiniGame miniGame;

        public string sceneName;

        public UnityEvent unityAction;

        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public Dialog dialog;

        public void action(Transform player){
            FindObjectOfType<QuestManager>().StartQuest(quest);
            //miniGame.pindahMiniGame(sceneName);

            //StartCoroutine(TimerCoroutine(4f));
        }  


        private IEnumerator TimerCoroutine(float duration)
        {
            // Tunggu sesuai durasi timer
            yield return new WaitForSeconds(duration);

            // Jika ada action yang sudah diset, panggil action tersebut
            unityAction?.Invoke();
        } 

        public void btnActive(GameObject btn, bool interactable)
        {
            btn.SetActive(interactable);
            btn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Sapu";
            var button = btn.GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(() =>{
                unableButton(button);
            });
        }

        void unableButton(UnityEngine.UI.Button button){
            button.gameObject.SetActive(false);
            button.onClick.RemoveListener(() => unableButton(button));
        }

        public void isTutorial(bool value)
        {
            throw new System.NotImplementedException();
        }
    }
}