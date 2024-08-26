using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace Terbaru{

    public class Loading : MonoBehaviour
    {
        public TMP_Text loadingText;
        public Image progressBar;

        void Start(){
            AsyncOperation async = SceneManager.LoadSceneAsync(PlayerPrefs.GetString("Scene"));

            async.allowSceneActivation = false;

            //SceneManager.sceneLoaded += UnloadScene;

            StartCoroutine(LoadNewScene(async));
        }
        void UnloadScene(Scene scene, LoadSceneMode loadSceneMode)
        {
            SceneManager.sceneLoaded -= UnloadScene;
            SceneManager.SetActiveScene(scene);
            SceneManager.UnloadSceneAsync("Scene Main Menu");
        }

        IEnumerator LoadNewScene(AsyncOperation asyncLoad){
            while(!asyncLoad.isDone){
                float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            
                progressBar.fillAmount = progress / 0.9f;

                if(asyncLoad.progress >= 0.9f){
                    asyncLoad.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
