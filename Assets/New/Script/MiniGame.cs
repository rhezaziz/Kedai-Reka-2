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

        public void pindahDialogToMiniGame(string namaScene){
            StartCoroutine(mulaiMiniGameFromDialog(namaScene));
        }

        public void pindahDialogToMiniGameKampung(string namaScene)
        {
            StartCoroutine(mulaiMiniGameFromDialogKampung(namaScene));
        }

        public void pindahKeKampung(string namaScene)
        {
            SceneManager.LoadScene(namaScene);
        }

        public void pindahScene(string namaScene){

            sceneActive = namaScene;
            SceneManager.LoadSceneAsync(namaScene, LoadSceneMode.Additive);
        }

        public void pindahMaps(string namaScene)
        {
            StartCoroutine(KembaliMaps(namaScene));
        }
    
        public void pindah(string namaScene){
            StartCoroutine(kembali(namaScene));
            //SceneManager.sceneLoaded += UnloadScene;
            //SceneManager.LoadSceneAsync(namaScene, LoadSceneMode.Additive);
        }

        IEnumerator mulaiMiniGameFromDialog(string namaScene){
            //control.currentState(state.Interaction);
            //control.playerMove.move = false;
            //UiManager.instance.chinematicWithaouCam(true);
            yield return new WaitForSeconds(1f);
            UiManager.instance.startChinematicWithoutCam();
            
            //FindObjectOfType<UiManager>().panelUtama.SetActive(false);
            yield return new WaitForSeconds(1f);
            
            UiManager.instance.Chinematic(true);

            yield return new WaitForSeconds(4f);
            Debug.Log("Pindah");
            sceneActive = namaScene;
            SceneManager.LoadSceneAsync(namaScene, LoadSceneMode.Additive);

            //yield return new WaitForSeconds(1f);
            DeactivateObjectsInPreviousScene();
            UiManager.instance.endChinematicWithoutCam();
            yield return new WaitForSeconds(1.5f);
            UiManager.instance.ChinematicPanel.endChineamticOtherScene();

            

        }

        public void openMainMenu(string namaScene)
        {
            sceneActive = namaScene;
            SceneManager.LoadSceneAsync(namaScene, LoadSceneMode.Additive);
            DeactivateObjectsInPreviousScene();
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
        IEnumerator mulaiMiniGameFromDialogKampung(string namaScene)
        {
            //    control.currentState(state.Interaction);
            //    control.playerMove.move = false;
            Manager_Ending.instance.chinematicWithaouCam(true);

           //indObjectOfType<UiManager>().panelUtama.SetActive(false);
            yield return new WaitForSeconds(1f);

            //UiManager.instance.Chinematic(true);



            sceneActive = namaScene;
            SceneManager.LoadSceneAsync(namaScene, LoadSceneMode.Single);

            yield return new WaitForSeconds(1f);
            DeactivateObjectsInPreviousSceneKampung();

            yield return new WaitForSeconds(1.5f);
            //UiManager.instance.chinematicWithaouCam(false);
        }


        public void pindahMainMenu()
        {
            PlayerPrefs.SetInt("Selesai", 1);
            //sceneActive = namaScene;
            SceneManager.LoadSceneAsync("New Scene", LoadSceneMode.Single);
            Destroy(gameObject);
        }
           
        IEnumerator mulaiMiniGame(string namaScene){
            //control.currentState(state.Interaction);
            //control.playerMove.move = false;
            UiManager.instance.startChinematic();
            
            //FindObjectOfType<UiManager>().panelUtama.SetActive(false);
            yield return new WaitForSeconds(3f);
            
            UiManager.instance.Chinematic(true);
            yield return new WaitForSeconds(3f);

            
            sceneActive = namaScene;
            SceneManager.LoadSceneAsync(namaScene, LoadSceneMode.Additive);

            yield return new WaitForSeconds(1f);
            DeactivateObjectsInPreviousScene();
            UiManager.instance.endChinematic();
            yield return new WaitForSeconds(1.5f);
            UiManager.instance.ChinematicPanel.endChineamticOtherScene();
        
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

        public void DeactivateObjectsInPreviousSceneKampung()
        {
            Scene previousScene = SceneManager.GetSceneByName("Kampung");

            if (previousScene.isLoaded)
            {
                GameObject[] rootObjects = previousScene.GetRootGameObjects();

                foreach (GameObject obj in rootObjects)
                {
                    obj.SetActive(false); // Nonaktifkan objek
                }
            }
        }

        public void ActivateObjectsInPreviousSceneKampung()
        {
            // Dapatkan scene yang sudah aktif sebelumnya (misalnya scene di index 0)
            Scene previousScene = SceneManager.GetSceneByName("Kampung");

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
            UiManager.instance.startChinematic();
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
            UiManager.instance.endChinematic();
            Debug.Log("kembali Quest");
            //UiManager.instance.endChinematic();
            QuestManager.instance.CheckAction("Kembali");
            //yield return new WaitForSeconds(1f);
            //UiManager.instance.startChinematic();
        }


        IEnumerator KembaliMaps(string value)
        {
            UiManager.instance.startChinematicWithoutCam();
            //UiManager.instance.chinematicWithaouCam(true);
            //FindObjectOfType<Controller>().currentState(state.Interaction);
            yield return new WaitForSeconds(1f);

            UiManager.instance.chinematicWithaouCam(true);
            yield return new WaitForSeconds(3f);

            Scene scene = SceneManager.GetSceneByName("Asrama BackUp");
            SceneManager.UnloadSceneAsync(value);
            SceneManager.SetActiveScene(scene);

            ActivateObjectsInPreviousScene();

            //yield return new WaitForSeconds(1f);
           // UiManager.instance.Chinematic(true);
            yield return new WaitForSeconds(2f);
            UiManager.instance.endChinematicWithoutCam();


            //FindObjectOfType<UiManager>().panelUtama.SetActive(true);
            //control.currentState(state.Default);
            //control.playerMove.move = true;
            FindObjectOfType<MapsManager>().panelMuncul();
            //QuestManager.instance.CheckActionQuest("Kembali");
            Debug.Log("Selesai Quest");

            yield return new WaitForSeconds(1f);
            UiManager.instance.ChinematicPanel.endChinematic();
            //

        }

        IEnumerator kembali(string value){
            //UiManager.instance.Chinematic(true);
            UiManager.instance.startChinematic();
            //FindObjectOfType<Controller>().currentState(state.Interaction);
            yield return new WaitForSeconds(2f);
            
            UiManager.instance.Chinematic(true);
            yield return new WaitForSeconds(3f);

            Scene scene = SceneManager.GetSceneByName("Asrama BackUp");
            SceneManager.UnloadSceneAsync(value);
            SceneManager.SetActiveScene(scene);
            
            ActivateObjectsInPreviousScene();
            
            //yield return new WaitForSeconds(1f);

            yield return new WaitForSeconds(2f);
            UiManager.instance.endChinematic();
            yield return new WaitForSeconds(1f);

            UiManager.instance.ChinematicPanel.endChinematic();
            //FindObjectOfType<UiManager>().panelUtama.SetActive(true);
            //control.currentState(state.Default);
            //control.playerMove.move = true;
            QuestManager.instance.CheckActionQuest("Kembali");
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