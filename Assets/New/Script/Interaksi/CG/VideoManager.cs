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
            testVideo = video;
            UiManager.instance.ChinematicPanel.SetActive(true);
            
            texture.Release();
            texture.Create();
            StartCoroutine(cutScene());

        }


        // void Start(){
        //     texture.Release();
        //     texture.Create(); 
        // }

        public void playVideo(){
            video.clip = testVideo;

            video.prepareCompleted += OnVideoPrepared;

            video.Prepare();
        }

        IEnumerator cutScene(){
            video.gameObject.SetActive(true);
            video.clip = testVideo;
            GameObject PanelUtama = UiManager.instance.panelUtama;
            float duration = (float)video.length;
            var camera = Camera.main;
            camera.transform.DOLocalMoveZ(-7f, 1f);
            PanelUtama.SetActive(false);

            UiManager.instance.Chinematic(true);

            yield return new WaitForSeconds(2f);

            UiManager.instance.Chinematic(true);

            yield return new WaitForSeconds(1.25f);

            playVideo();

            yield return new WaitForSeconds(duration + 0.5f);
            texture.Release();
            texture.Create(); 
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