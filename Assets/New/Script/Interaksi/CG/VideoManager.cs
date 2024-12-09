using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UI;

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
            //UiManager.instance.ChinematicPanel.SetActive(true);

            texture.Release();
            texture.Create();
            //StartCoroutine(cutSceneUpdate(true));
            StartCoroutine(cutScene(true));

        }

        public void action(){
            Debug.Log("Play Video");
            //testVideo = video ? video : null;
            //UiManager.instance.ChinematicPanel.SetActive(true);
            
            texture.Release();
            texture.Create();
            //StartCoroutine(cutSceneUpdate(false));
            StartCoroutine(cutScene(false));
        }

        public void actionOnDialog(VideoClip video)
        {
            FindObjectOfType<DialogManager>().panelDialog.SetActive(false);
            FindObjectOfType<DialogManager>().btnNextDialog.gameObject.SetActive(false);
            testVideo = video ? video : null;
            //UiManager.instance.ChinematicPanel.SetActive(true);
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

        #region Ienumerator
        IEnumerator cutSceneOnDialogEnd(bool adaVideo)
        {
            //video.gameObject.SetActive(true);
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
            //ClearRenderTexture();
            // texture.Release();
            // texture.Create(); 
            //video.gameObject.SetActive(false);

            //Manager_Ending.instance.Chinematic(false);

            //camera.transform.DOLocalMoveZ(-10f, 1f);
            video.gameObject.SetActive(false);
            yield return new WaitForSeconds(2f);
            //PanelUtama.SetActive(true);
            //video.Pause();
            //UiManager.instance.startChinematicWithoutCam();
            //yield return new WaitForSeconds(1f);
            //UiManager.instance.chinematicWithaouCam(true);
            //yield return new WaitForSeconds(2f);
            FindObjectOfType<MiniGame>().pindahMainMenu();
            //FindObjectOfType<Controller>().currentState(state.Default);
            //texture.Release();
            //QuestManager.instance.CheckActionQuest(namaAction);
        }

        IEnumerator cutSceneOnDialog(bool adaVideo)
        {
            //video.gameObject.SetActive(true);
            video.clip = testVideo;
            //FindObjectOfType<Controller>().currentState(state.Interaction);
            //GameObject PanelUtama = UiManager.instance.panelUtama;
            float duration = (float)video.length;
            var camera = Camera.main;
            camera.transform.DOLocalMoveZ(-7f, 1f);
            //PanelUtama.SetActive(false);

            UiManager.instance.startChinematic();

            yield return new WaitForSeconds(2f);

            //UiManager.instance.Chinematic(true);

            //yield return new WaitForSeconds(1.25f);


            if (adaVideo) playVideo();

            yield return new WaitForSeconds(duration + 0.5f);
            ClearRenderTexture();
            // texture.Release();
            // texture.Create(); 
            video.gameObject.SetActive(false);

            UiManager.instance.endChinematic();

            camera.transform.DOLocalMoveZ(-10f, 1f);

            yield return new WaitForSeconds(2f);
            UiManager.instance.ChinematicPanel.endChinematic();
            //PanelUtama.SetActive(true);

            //FindObjectOfType<Controller>().currentState(state.Default);
            texture.Release();
            QuestManager.instance.CheckActionQuest(namaAction);
        }


        IEnumerator cutScene(bool adaVideo)
        {
            Debug.Log("Cut Scene");
            
            video.clip = testVideo;
            //FindObjectOfType<Controller>().currentState(state.Interaction);
            //GameObject PanelUtama = UiManager.instance.panelUtama;
            float duration = 1;
            float tempDuration = (float)video.length;
            var camera = Camera.main;
            camera.transform.DOLocalMoveZ(-7f, 1f);
            //PanelUtama.SetActive(false);

            UiManager.instance.startChinematic();

            yield return new WaitForSeconds(2f);

            UiManager.instance.Chinematic(true);
            //video.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.25f);


            if (adaVideo)
            {
                duration = tempDuration;
                playVideo();
            }
            yield return new WaitForSeconds(duration + 0.5f);
            ClearRenderTexture();
            // texture.Release();
            // texture.Create(); 
            video.gameObject.SetActive(false);

            UiManager.instance.endChinematic();

            camera.transform.DOLocalMoveZ(-10f, 1f);

            yield return new WaitForSeconds(2f);
            UiManager.instance.ChinematicPanel.endChinematic();
            //PanelUtama.SetActive(true);

            // FindObjectOfType<Controller>().currentState(state.Default);
            texture.Release();

            QuestManager.instance.CheckActionQuest(namaAction);
            //QuestManager.instance.CheckAction(namaAction);


        }
        #endregion
        // void Start(){
        //     texture.Release();
        //     texture.Create(); 
        // }

        IEnumerator cutSceneUpdate(bool adaVideo)
        {
            ClearRenderTexture();
            ///video.gameObject.SetActive(true);
            video.clip = testVideo;
            UiManager.instance.startChinematic();

            yield return new WaitForSeconds(2f);

            UiManager.instance.Chinematic(true);

            yield return new WaitForSeconds(1.25f);


            if (adaVideo) playVideo();

            else
            {
                video.gameObject.SetActive(false);

                UiManager.instance.endChinematic();

                yield return new WaitForSeconds(2f);
                UiManager.instance.ChinematicPanel.endChinematic();
                texture.Release();

                QuestManager.instance.CheckActionQuest(namaAction);
            }
        }
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
            video.gameObject.SetActive(true);
            video.renderMode = VideoRenderMode.RenderTexture;
            video.targetTexture = texture; 
            video.clip = testVideo;

            video.prepareCompleted += OnVideoPrepared;
            //video.loopPointReached += EndReached;
            //video.prepareCompleted += AdjustAspectRatio;
            video.Prepare();
        }

        void OnVideoPrepared(VideoPlayer vp){
            vp.Play();
            video.prepareCompleted -= OnVideoPrepared;
            
        }

        void EndReached(VideoPlayer vp)
        {
            //panelUI.SetActive(true);
            Debug.Log("Video Selesai Diputar!");
            //ClearRenderTexture();
            video.loopPointReached -= EndReached;



            video.gameObject.SetActive(false);

            UiManager.instance.endChinematic();


            //UiManager.instance.ChinematicPanel.endChinematic();
            texture.Release();
            //Invoke("disableChinematic", 1.5f);
            disableChinematic();
            QuestManager.instance.CheckActionQuest(namaAction);

        }

        void disableChinematic()
        {
            ClearRenderTexture();
            UiManager.instance.ChinematicPanel.endChinematic();
        }

        void AdjustAspectRatio(VideoPlayer vp)
        {
            //float videoWitdth = vp.texture.width;
            //float videoHeight = vp.texture.height;

            //float screenWidhth = Screen.width;
            //float screenHeight = Screen.height;

            //float videoAspect = videoWitdth / videoHeight;
            //float ScreenAspect = screenWidhth / screenHeight;

            //if (videoAspect > ScreenAspect)
            //{
            //    rawImageTransform.sizeDelta = new Vector2(screenWidhth, screenWidhth / videoAspect);
            //}
            //else
            //{
            //    rawImageTransform.sizeDelta = new Vector2(screenHeight * videoAspect, screenWidhth);

            //}
            vp.prepareCompleted -= AdjustAspectRatio;
            int screenWitdh = Screen.width;
            int screenHeight = Screen.height;

            if (screenWitdh <= 1280 && screenHeight <= 720)
            {
                texture.Release();
                texture.width = 1280;
                texture.height = 720;
            }

            else if (screenWitdh <= 1920 && screenHeight <= 1080)
            {
                texture.Release();
                texture.width = 1920;
                texture.height = 1080;
            }

            else
            {
                texture.Release();
                texture.width = 3840;
                texture.height = 2160;
            }
        }
    }
}