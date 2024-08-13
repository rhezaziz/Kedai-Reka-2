using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace  Terbaru
{ 
    public class TestLoading : MonoBehaviour
    {
        [Header("UI GameObject")]
        [SerializeField] private GameObject MainMenuScreen;
        [SerializeField] private GameObject LoadingScreen;

        [Header("Slider")]
        public Slider loadingSlader;

        public void loadSceneBtn(string sceneName){
            MainMenuScreen.SetActive(false);
            LoadingScreen.SetActive(true);

            //pindahScene(sceneName);
            StartCoroutine(LoadNewScene(sceneName));
        }

        void pindahScene(string sceneName){
            SceneManager.LoadScene(sceneName);
        }

        IEnumerator LoadNewScene(string SceneName){

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);

            //asyncLoad.allowSceneActivation = false;

            while(!asyncLoad.isDone){
                float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            
                loadingSlader.value = progress;
                
                yield return null;
            }

        }
    }   
}
