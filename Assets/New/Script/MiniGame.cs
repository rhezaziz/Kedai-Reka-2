using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Terbaru{
    public class MiniGame : MonoBehaviour
    {
        public string sceneActive;

        public Controller control;


        public void pindahMiniGame(string namaScene){
            StartCoroutine(mulaiMiniGame(namaScene));
        }

        public void kembaliMiniGame(string nama){
            StartCoroutine(kembaliQuest(nama));
        }

        public void pindahScene(string namaScene){

            sceneActive = namaScene;
            SceneManager.LoadSceneAsync(namaScene, LoadSceneMode.Additive);
        }
    
        public void pindah(string namaScene){
            StartCoroutine(kembali(namaScene));
            //SceneManager.sceneLoaded += UnloadScene;
            //SceneManager.LoadSceneAsync(namaScene, LoadSceneMode.Additive);
        }

        IEnumerator mulaiMiniGame(string namaScene){
            control.currentState(state.Interaction);
            control.playerMove.move = false;
            UiManager.instance.Chinematic(true);
            
            FindObjectOfType<UiManager>().panelUtama.SetActive(false);
            yield return new WaitForSeconds(3f);
            
            UiManager.instance.Chinematic(true);
            yield return new WaitForSeconds(3f);

            
            sceneActive = namaScene;
            SceneManager.LoadSceneAsync(namaScene, LoadSceneMode.Additive);

            yield return new WaitForSeconds(1f);
            DeactivateObjectsInPreviousScene();
            UiManager.instance.Chinematic(false);

        
        }

        public void DeactivateObjectsInPreviousScene()
        {
            Scene previousScene = SceneManager.GetSceneByName("Asrama BackUp");

            if (previousScene.isLoaded)
            {
                GameObject[] rootObjects = previousScene.GetRootGameObjects();

                foreach (GameObject obj in rootObjects)
                {
                    obj.SetActive(false); // Nonaktifkan objek
                }
            }
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


        IEnumerator kembaliQuest(string value){
            UiManager.instance.Chinematic(true);
            //FindObjectOfType<Controller>().currentState(state.Interaction);
            yield return new WaitForSeconds(1f);
            
            UiManager.instance.Chinematic(true);
            yield return new WaitForSeconds(3f);

            Scene scene = SceneManager.GetSceneByName("Asrama BackUp");
            SceneManager.UnloadSceneAsync(value);
            SceneManager.SetActiveScene(scene);
            
            ActivateObjectsInPreviousScene();
            
            //yield return new WaitForSeconds(1f);

            yield return new WaitForSeconds(2f);
            UiManager.instance.Chinematic(true);
            QuestManager.instance.CheckAction("Kembali");
        }

        IEnumerator kembali(string value){
            UiManager.instance.Chinematic(true);
            //FindObjectOfType<Controller>().currentState(state.Interaction);
            yield return new WaitForSeconds(1f);
            
            UiManager.instance.Chinematic(true);
            yield return new WaitForSeconds(3f);

            Scene scene = SceneManager.GetSceneByName("Asrama BackUp");
            SceneManager.UnloadSceneAsync(value);
            SceneManager.SetActiveScene(scene);
            
            ActivateObjectsInPreviousScene();
            
            //yield return new WaitForSeconds(1f);

            yield return new WaitForSeconds(2f);
            UiManager.instance.Chinematic(false);
            
            
            FindObjectOfType<UiManager>().panelUtama.SetActive(true);
            control.currentState(state.Default);
            control.playerMove.move = true;
            Debug.Log("Selesai Quest");
            //
            
        }


        public void UnloadScene(Scene scene, LoadSceneMode loadScene)
        {
            //SceneManager.sceneLoaded -= UnloadScene;
            SceneManager.SetActiveScene(scene);
            SceneManager.UnloadSceneAsync(sceneActive);
        }
    }
}