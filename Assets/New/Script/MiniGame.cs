using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Terbaru{
    public class MiniGame : MonoBehaviour
    {
        public string sceneActive;


        public void pindahMiniGame(string namaScene){
            StartCoroutine(mulaiMiniGame(namaScene));
        }
    
        public void pindah(string namaScene){
            StartCoroutine(kembali(namaScene));
            //SceneManager.sceneLoaded += UnloadScene;
            //SceneManager.LoadSceneAsync(namaScene, LoadSceneMode.Additive);
        }

        IEnumerator mulaiMiniGame(string namaScene){
            UiManager.instance.Chinematic(true);
            yield return new WaitForSeconds(3f);
            
            UiManager.instance.Chinematic(true);
            yield return new WaitForSeconds(3f);

            sceneActive = namaScene;
            SceneManager.LoadSceneAsync(namaScene, LoadSceneMode.Additive);

            yield return new WaitForSeconds(1f);
            UiManager.instance.Chinematic(true);

        
        }

        IEnumerator kembali(string value){
            UiManager.instance.Chinematic(true);
            yield return new WaitForSeconds(1f);
            
            UiManager.instance.Chinematic(true);
            yield return new WaitForSeconds(3f);

            Scene scene = SceneManager.GetSceneByName("Asrama");
            SceneManager.SetActiveScene(scene);
            SceneManager.UnloadSceneAsync(value);

            yield return new WaitForSeconds(2f);
            UiManager.instance.Chinematic(false);
            //yield return new WaitForSeconds(1f);
            
        }


        public void UnloadScene(Scene scene, LoadSceneMode loadScene)
        {
            //SceneManager.sceneLoaded -= UnloadScene;
            SceneManager.SetActiveScene(scene);
            SceneManager.UnloadSceneAsync(sceneActive);
        }
    }
}