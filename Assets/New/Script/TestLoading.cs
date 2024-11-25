using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
        public Image loadingSlader;
        string namaScene;

        void Awake(){
            namaScene = PlayerPrefs.GetString("Scene");
        }

        void Start(){
            Invoke("loadSceneBtn", 2f);
        }

        public void loadSceneBtn(){

            StartCoroutine(testNewLoad());
        }

        void pindahScene(string sceneName){
            SceneManager.LoadScene(sceneName);
        }

        IEnumerator testNewLoad()
        {
            float timer = 5f;
            while (timer >= 0)
            {
                float progress = Mathf.Clamp01(.9f / 0.9f);

                loadingSlader.fillAmount = progress;
                timer -= .1f;
                yield return .1f;
            }
            kembaliAsrama("New Scene");
            Destroy(this.gameObject);
        }

        IEnumerator LoadNewScene(string SceneName){

            sceneActive = SceneManager.GetActiveScene().name;
            SceneManager.sceneLoaded += UnloadScene;
            //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);

            //asyncLoad.allowSceneActivation = false;

            while (!asyncLoad.isDone){
                float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            
                loadingSlader.fillAmount = progress;
                
                yield return null;
            }



            // Scene currentScene = SceneManager.GetActiveScene();

            // GameObject[] rootObject = currentScene.GetRootGameObjects();

            // foreach(GameObject obj in rootObject){
            //     obj.SetActive(false);
            // }

            Destroy(this.gameObject);

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


        public void kembaliAsrama(string namaScene)
        {
            Scene scene = SceneManager.GetSceneByName("Asrama BackUp");
            SceneManager.UnloadSceneAsync(namaScene);
            SceneManager.SetActiveScene(scene);

            ActivateObjectsInPreviousScene();


            //GameManager.instance.mainMenu = false;
            GameManager.instance.updateMainMenu(false);
        }

        public void ActivateObjectsInPreviousScene()
        {
            // Dapatkan scene yang sudah aktif sebelumnya (misalnya scene di index 0)
            Scene previousScene = SceneManager.GetSceneByName("Asrama BackUp");

            if (previousScene.isLoaded)
            {
                // Dapatkan semua root gameObject di scene sebelumnya
                GameObject[] rootObjects = previousScene.GetRootGameObjects();

                // Aktifkan semua root gameObject (atau pilih objek tertentu)
                foreach (GameObject obj in rootObjects)
                {
                    obj.SetActive(true); // Aktifkan objek
                }
            }
        }
    }   
}
