using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace  Terbaru
{ 
    public class TestLoading : MonoBehaviour
    {
        //[Header("UI GameObject")]
        //[SerializeField] private GameObject MainMenuScreen;
        //[SerializeField] private GameObject LoadingScreen;

        [Header("Slider")]
        public Slider loadingSlader;
        string namaScene;

        void Awake(){
            namaScene = PlayerPrefs.GetString("Scene");
        }

        void Start(){
            Invoke("loadSceneBtn", 2f);
        }

        public void loadSceneBtn(){

            StartCoroutine(LoadNewScene(namaScene));
        }

        void pindahScene(string sceneName){
            SceneManager.LoadScene(sceneName);
        }

        IEnumerator LoadNewScene(string SceneName){

            sceneActive = SceneManager.GetActiveScene().name;
            SceneManager.sceneLoaded += UnloadScene;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);

            //asyncLoad.allowSceneActivation = false;

            while(!asyncLoad.isDone){
                float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            
                loadingSlader.value = progress;
                
                yield return null;
            }

            // Scene currentScene = SceneManager.GetActiveScene();

            // GameObject[] rootObject = currentScene.GetRootGameObjects();

            // foreach(GameObject obj in rootObject){
            //     obj.SetActive(false);
            // }

            // Destroy(this.gameObject);

        }
        public string sceneActive;
        public void pindah(string namaScene){
            sceneActive = SceneManager.GetActiveScene().name;
            SceneManager.sceneLoaded += UnloadScene;
            SceneManager.LoadSceneAsync(namaScene, LoadSceneMode.Additive);
        }


        void UnloadScene(Scene scene, LoadSceneMode loadScene)
        {
            SceneManager.sceneLoaded -= UnloadScene;
            SceneManager.SetActiveScene(scene);
            SceneManager.UnloadSceneAsync(sceneActive);
        }
    }   
}
