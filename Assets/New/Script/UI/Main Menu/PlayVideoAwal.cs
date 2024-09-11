using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


namespace Terbaru{

    public class PlayVideoAwal : MonoBehaviour
    {
        
        bool isPlay = false;

        
        public VideoClip testVideo;
        public VideoPlayer video;

        public RenderTexture texture;
        // Start is called before the first frame update

        void OnEnable(){
            playVideo();
        }

        void Start(){
            texture.Release();
            texture.Create(); 
        }

        public void playVideo(){
            video.clip = testVideo;
            
            video.prepareCompleted += OnVideoPrepared;

            video.Prepare();
            
        }

        void OnVideoPrepared(VideoPlayer vp){
            vp.Play();
            isPlay = true;
        }
        // Update is called once per frame
        void Update()
        {
            if(isPlay){
                if(!video.isPlaying){
                    isPlay = false;

                    Invoke("loading", 2f);
                }
            }            
        }

        void loading(){
        
            this.enabled = false;
            FindObjectOfType<MainMenu>().PindahScene("Asrama");
            Debug.Log("Loading");
            
        }
    }
}
