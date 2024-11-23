using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;

namespace Terbaru{
    public class VideoManager : MonoBehaviour
    {
        public string namaAction;
        public VideoClip testVideo;
        public VideoPlayer video;

        public RenderTexture texture;


        bool CG;

        public bool value;

        public bool isTutorial(){
            return value;
        }

        
        void OnDisable(){
            value = false;
        }
        
        void OnEnable(){
            value = true;
        }

        public void action(VideoClip video){
            Debug.Log("Play Video");
            testVideo = video ? video : null;
            UiManager.instance.ChinematicPanel.SetActive(true);
            Debug.Log("on Step");
            texture.Release();
            texture.Create();
            StartCoroutine(cutScene(true));

        }

        public void action(){
            Debug.Log("Play Video");
            //testVideo = video ? video : null;
            UiManager.instance.ChinematicPanel.SetActive(true);
            
            texture.Release();
            texture.Create();
            StartCoroutine(cutScene(false));
        }

        public void actionOnDialog(VideoClip video)
        {
            FindObjectOfType<DialogManager>().panelDialog.SetActive(false);
            FindObjectOfType<DialogManager>().btnNextDialog.gameObject.SetActive(false);
            testVideo = video ? video : null;
            UiManager.instance.ChinematicPanel.SetActive(true);
            Debug.Log("on Step");
            texture.Release();
            texture.Create();
            StartCoroutine(cutSceneOnDialog(true));
        }

        public void actionOnDialogEnd(VideoClip video)
        {
            FindObjectOfType<DialogManager>().panelDialog.SetActive(false);
            FindObjectOfType<DialogManager>().btnNextDialog.gameObject.SetActive(false);
            testVideo = video ? video : null;
            Manager_Ending.instance.ChinematicPanel.SetActive(true);
            texture.Release();
            texture.Create();
            StartCoroutine(cutSceneOnDialogEnd(true));
        }
        IEnumerator cutSceneOnDialogEnd(bool adaVideo)
        {
            video.gameObject.SetActive(true);
            video.clip = testVideo;
            //FindObjectOfType<Controller>().currentState(state.Interaction);
            //GameObject PanelUtama = UiManager.instance.panelUtama;
            float duration = (float)video.length;
            var camera = Camera.main;
            camera.transform.DOLocalMoveZ(-7f, 1f);
            //PanelUtama.SetActive(false);
            Manager_Ending.instance.Chinematic(true);
            //UiManager.instance.Chinematic(true);

            yield return new WaitForSeconds(2f);

            //UiManager.instance.Chinematic(true);

            //yield return new WaitForSeconds(1.25f);


            if (adaVideo) playVideo();

            yield return new WaitForSeconds(duration + 0.5f);
            ClearRenderTexture();
            // texture.Release();
            // texture.Create(); 
            //video.gameObject.SetActive(false);

            //Manager_Ending.instance.Chinematic(false);

            //camera.transform.DOLocalMoveZ(-10f, 1f);

            yield return new WaitForSeconds(2f);
            //PanelUtama.SetActive(true);
            video.Pause();
            //FindObjectOfType<Controller>().currentState(state.Default);
            //texture.Release();
            //QuestManager.instance.CheckActionQuest(namaAction);
        }

        IEnumerator cutSceneOnDialog(bool adaVideo)
        {
            video.gameObject.SetActive(true);
            video.clip = testVideo;
            FindObjectOfType<Controller>().currentState(state.Interaction);
            GameObject PanelUtama = UiManager.instance.panelUtama;
            float duration = (float)video.length;
            var camera = Camera.main;
            camera.transform.DOLocalMoveZ(-7f, 1f);
            PanelUtama.SetActive(false);

            UiManager.instance.Chinematic(true);

            yield return new WaitForSeconds(2f);

            //UiManager.instance.Chinematic(true);

            //yield return new WaitForSeconds(1.25f);


            if (adaVideo) playVideo();

            yield return new WaitForSeconds(duration + 0.5f);
            ClearRenderTexture();
            // texture.Release();
            // texture.Create(); 
            video.gameObject.SetActive(false);

            UiManager.instance.Chinematic(false);

            camera.transform.DOLocalMoveZ(-10f, 1f);

            yield return new WaitForSeconds(2f);
            PanelUtama.SetActive(true);

            FindObjectOfType<Controller>().currentState(state.Default);
            texture.Release();
            QuestManager.instance.CheckActionQuest(namaAction);
        }




        // void Start(){
        //     texture.Release();
        //     texture.Create(); 
        // }
        void ClearRenderTexture()
        {
            // Aktifkan RenderTexture
            RenderTexture.active = texture;

            // Bersihkan dengan warna hitam atau warna tertentu
            GL.Clear(true, true, Color.black);

            // Nonaktifkan RenderTexture
            RenderTexture.active = null;
        }
        public void playVideo(){
            //RenderTexture.active = null;
            
            video.renderMode = VideoRenderMode.RenderTexture;
            video.targetTexture = texture; 
            video.clip = testVideo;

            video.prepareCompleted += OnVideoPrepared;

            video.Prepare();
        }

        IEnumerator cutScene(bool adaVideo){
            Debug.Log("Cut Scene");
            video.gameObject.SetActive(true);
            video.clip = testVideo;
            FindObjectOfType<Controller>().currentState(state.Interaction);
            GameObject PanelUtama = UiManager.instance.panelUtama;
            float duration = (float)video.length;
            var camera = Camera.main;
            camera.transform.DOLocalMoveZ(-7f, 1f);
            PanelUtama.SetActive(false);

            UiManager.instance.Chinematic(true);

            yield return new WaitForSeconds(2f);

            UiManager.instance.Chinematic(true);

            yield return new WaitForSeconds(1.25f);

            
            if(adaVideo) playVideo();

            yield return new WaitForSeconds(duration + 0.5f);
            ClearRenderTexture();
            // texture.Release();
            // texture.Create(); 
            video.gameObject.SetActive(false);

            UiManager.instance.Chinematic(false);
            
            camera.transform.DOLocalMoveZ(-10f, 1f);

            yield return new WaitForSeconds(2f);
            PanelUtama.SetActive(true);
            
            FindObjectOfType<Controller>().currentState(state.Default);
            texture.Release();
            QuestManager.instance.CheckActionQuest(namaAction);
            //QuestManager.instance.CheckAction(namaAction);
            
            
        }

        // void Update(){
        //     if(video.isPrepared){
        //         double videoDuration = video.length;
            
        //     // Mendapatkan waktu pemutaran saat ini dalam detik
        //         double currentTime = video.time;

        //         Debug.Log("Video Duration: " + videoDuration + " seconds");
        //         Debug.Log("Current Video Time: " + currentTime + " seconds");
        //     }
        // }

        void OnVideoPrepared(VideoPlayer vp){
            vp.Play();
        }

        IEnumerator checkVideo(){
            while(CG){
                
                yield return null;
            }
        }
    }
}