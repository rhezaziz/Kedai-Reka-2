using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


namespace Terbaru
{


    public class TestVideo : MonoBehaviour
    {
        public VideoPlayer videoPlayer;
        public RectTransform rawImageTransform;

        public GameObject panelUI;
        public RenderTexture texture;
        public UnityEngine.UI.RawImage rawImage;
        public VideoClip clip;

        public List<UnityEngine.UI.Button> buttons;
        //void Start()
        //{

        //}

        public void pilih(UnityEngine.UI.Button button)
        {
            foreach (var btn in buttons)
            {
                btn.interactable = true;
            }

            button.interactable = false;
        }

        public void playVideo()
        {
            rawImage.texture = texture;
            //RenderTexture.active = null;
            videoPlayer.loopPointReached += EndReached;

            videoPlayer.clip = clip;
            //videoPlayer.Play();
            //videoPlayer.clear
            videoPlayer.prepareCompleted += OnVideoPrepared;
            videoPlayer.prepareCompleted += AdjustAspectRatio;
            videoPlayer.Prepare();
        }

        void OnVideoPrepared(VideoPlayer vp)
        {
            Debug.Log("Mulai");
            videoPlayer.prepareCompleted -= AdjustAspectRatio;
            videoPlayer.prepareCompleted -= OnVideoPrepared;

            vp.Play();
        }
        public void action(VideoClip video)
        {
            panelUI.SetActive(false);
            // Mendaftarkan event handler untuk saat video mencapai akhir
            
            clip = video;
            //texture.Release();
            //texture.Create();
            playVideo();

        }
        // Fungsi ini akan dipanggil ketika video selesai diputar
        void EndReached(VideoPlayer vp)
        {
            panelUI.SetActive(true);
            Debug.Log("Video Selesai Diputar!");
            //ClearRenderTexture();
            videoPlayer.loopPointReached -= EndReached;
            
        }
        void ClearRenderTexture()
        {
            RenderTexture currentRT = RenderTexture.active;
            RenderTexture.active = texture;
            GL.Clear(true, true, Color.clear);
            RenderTexture.active = currentRT;
            rawImage.texture = null;
           
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

            int screenWitdh = Screen.width;
            int screenHeight = Screen.height;

            if(screenWitdh <= 1280 && screenHeight <= 720)
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

        //void Start()
        //{
        //    // Mendaftarkan event handler untuk saat video mencapai akhir
        //    videoPlayer.loopPointReached += EndReached;

        //    // Memulai video
        //    videoPlayer.Play();
        //}

        //// Fungsi ini akan dipanggil ketika video selesai diputar
        //void EndReached(VideoPlayer vp)
        //{
        //    Debug.Log("Video Selesai Diputar!");
        //}
    }
}