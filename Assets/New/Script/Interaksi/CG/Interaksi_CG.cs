using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;
using UnityEngine.Events;


namespace Terbaru{

    public class Interaksi_CG : MonoBehaviour, Interaction
    {   
        
        public string namaAction;
        public string AnimationAction;
        public string AnimationActionPerempuan;
        public UnityEvent extendAction;
        public VideoClip[] testVideo;
        public VideoPlayer video;

        public RenderTexture texture;

        public Transform pos;
        public Transform posPerempuan;
        public Collider col;

        public Transform spanw;


        bool CG;

        public bool value;

        public Transform Player;
        public GameObject point;
        public float distancePlayer;
        public void checkDistance(){
            float distance = Vector3.Distance(transform.position, Player.position);

            if(distancePlayer >= distance){
                point.gameObject.SetActive(true);
            }else{
                point.gameObject.SetActive(false);
            } 
        }

        void Update(){
            if(Interactable())
                checkDistance();
        }

        public void isTutorial(bool temp){
            //enabled = temp;
        }

        
        void OnDisable(){
            value = false;
        }
        
        void OnEnable(){
            value = true;
        }

        bool interactable;
        public bool Interactable(){
            return interactable;
        }

        public void changeInteractable(bool value){
            interactable = value;
            // return interactable;
        }

        public void action(Transform Player){
            //UiManager.instance.ChinematicPanel.SetActive(true);
            var Perempuan = Player.GetChild(1).transform;
            Perempuan.gameObject.SetActive(true);
            
            if(col)
                col.enabled = false;
            // Player.GetComponent<Collider>().enabled = false;
            // Player.GetComponent<Rigidbody>().isKinematic = true;
            Player.GetComponent<Controller>().changeRunTimeController(true);
            Player.GetComponentInChildren<Animator>().SetBool(AnimationAction, true);
            extendAction.AddListener(()=>stopAnimation(Player));
            
            Player.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
            Player.position = new Vector3(pos.position.x, Player.position.y, pos.position.z);
            Perempuan.position = new Vector3(posPerempuan.position.x, posPerempuan.position.y, posPerempuan.position.z);
            Perempuan.GetComponentInChildren<Animator>().SetBool(AnimationActionPerempuan, true);


            texture.Release();
            texture.Create();
            StartCoroutine(cutScene());
        }

        void stopAnimation(Transform Player){
            //Player.GetComponent<Collider>().enabled = true;
            //Player.GetComponent<Rigidbody>().isKinematic = false;
            var Perempuan = Player.GetChild(1).transform;
            
            Perempuan.GetComponentInChildren<Animator>().SetBool(AnimationActionPerempuan, false);
            Player.GetComponentInChildren<Animator>().SetBool(AnimationAction, false);
            Perempuan.gameObject.SetActive(false);

            

            if(col) 
                col.enabled = true;
            extendAction.RemoveListener(() => stopAnimation(Player));

            Player.GetComponent<Controller>().changeRunTimeController(false);
            point.gameObject.SetActive(false);
        }

        public void btnActive(GameObject btn, bool interactable){
            
            btn.SetActive(interactable);
            btn.GetComponent<UnityEngine.UI.Button>().interactable = Interactable();
            
            btn.GetComponentInChildren<Text>().text = namaAction;
        }

        


        void Start(){
            texture.Release();
            texture.Create(); 
        }

        VideoClip clips(){
            if(testVideo.Length < 1)
                return testVideo[0];
            
            int index = FindObjectOfType<WaktuManager>().indexTime();
            return testVideo[index];
        }

        public void playVideo(){
            
            video.clip = clips();

            video.prepareCompleted += OnVideoPrepared;
            video.loopPointReached += EndReached;
            video.prepareCompleted += AdjustAspectRatio;

            video.Prepare();
        }

        IEnumerator cutScene(){
            video.gameObject.SetActive(true);
            video.clip = clips();
            GameObject PanelUtama = UiManager.instance.panelUtama;
            float duration = (float)video.length;
            var camera = Camera.main;
            camera.transform.DOLocalMoveZ(-7f, 1f);
            PanelUtama.SetActive(false);

            UiManager.instance.startChinematic();

            yield return new WaitForSeconds(2f);

            UiManager.instance.Chinematic(true);

            yield return new WaitForSeconds(1.25f);

            playVideo();

            //yield return new WaitForSeconds(duration + 0.5f);
            //texture.Release();
            //texture.Create(); 
            //video.gameObject.SetActive(false);

            //UiManager.instance.endChinematic();
            
            //camera.transform.DOLocalMoveZ(-10f, 1f);

            //yield return new WaitForSeconds(2f);
            //extendAction?.Invoke();
            //PanelUtama.SetActive(true);
            
            ////FindObjectOfType<Controller>().currentState(state.Default);

            //UiManager.instance.ChinematicPanel.endChinematic();
            
            
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

        //void OnVideoPrepared(VideoPlayer vp){
        //    vp.Play();
        //}

        //public void playVideo()
        //{
        //    //RenderTexture.active = null;

        //    video.renderMode = VideoRenderMode.RenderTexture;
        //    video.targetTexture = texture;
        //    video.clip = testVideo;

        //    video.prepareCompleted += OnVideoPrepared;
        //    video.loopPointReached += EndReached;
        //    video.Prepare();
        //}

        void OnVideoPrepared(VideoPlayer vp)
        {
            vp.Play();
            video.prepareCompleted -= OnVideoPrepared;

        }

        void EndReached(VideoPlayer vp)
        {
            //panelUI.SetActive(true);
            Debug.Log("Video Selesai Diputar!");
            //ClearRenderTexture();
            video.loopPointReached -= EndReached;



            
            texture.Release();
            texture.Create();
            video.gameObject.SetActive(false);

            UiManager.instance.endChinematic();


          
            extendAction?.Invoke();
           

            //FindObjectOfType<Controller>().currentState(state.Default);

            //UiManager.instance.ChinematicPanel.endChinematic();
            Invoke("disableChinematic", 1.5f);

        }

        void disableChinematic()
        {
            UiManager.instance.ChinematicPanel.endChinematic();
        }
        //void ClearRenderTexture()
        //{
        //    RenderTexture currentRT = RenderTexture.active;
        //    RenderTexture.active = texture;
        //    GL.Clear(true, true, Color.clear);
        //    RenderTexture.active = currentRT;
        //    //rawImage.texture = null;

        //}

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

        IEnumerator checkVideo(){
            while(CG){
                
                yield return null;
            }
        }
    }

}