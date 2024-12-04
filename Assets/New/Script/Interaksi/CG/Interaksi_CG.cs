using System.Collections;
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

        public AudioClip clip;


        bool CG;

        public bool value;


        #region Interaction
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
            var Perempuan = Player.GetChild(1).transform;
            Perempuan.gameObject.SetActive(true);
            
            if(col)
                col.enabled = false;
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

            //if(clip) SoundManager.instance.sfx(clip);
        }

        #endregion


        //IEnumerator mulaiVideo()
        //{
        //    video.clip = clips();
        //    VideoClip tempClip = clips();

        //    FindObjectOfType<VideoManager>().action(tempClip);
        //    yield return new WaitForSeconds(1f);

        //}
        void stopAnimation(Transform Player){
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
            btn.GetComponent<Button>().interactable = Interactable();
            
            btn.GetComponentInChildren<Text>().text = namaAction;
        }

        VideoClip clips(){
            if(testVideo.Length < 1)
                return testVideo[0];
            
            int index = FindObjectOfType<WaktuManager>().indexTime();
            return testVideo[index];
        }

        void playInvoke(VideoPlayer vp)
        {
            //panelUI.SetActive(true);
            Debug.Log("Video Selesai Diputar!");
            //ClearRenderTexture();
            video.loopPointReached -= playInvoke;

            extendAction?.Invoke();

        }

        public void playVideo()
        {

            video.clip = clips();

            video.prepareCompleted += OnVideoPrepared;
            video.loopPointReached += EndReached;
            //video.prepareCompleted += AdjustAspectRatio;

            video.Prepare();
        }

        IEnumerator cutScene()
        {
            //video.clip = clips();
            //GameObject PanelUtama = UiManager.instance.panelUtama;
            //float duration = (float)video.length;
            //var camera = Camera.main;
            //camera.transform.DOLocalMoveZ(-7f, 1f);
            //PanelUtama.SetActive(false);

            UiManager.instance.startChinematic();

            yield return new WaitForSeconds(2f);

            UiManager.instance.Chinematic(true);
            video.gameObject.SetActive(true);

            yield return new WaitForSeconds(1.25f);

            playVideo();
        }

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
        ClearRenderTexture();
        UiManager.instance.ChinematicPanel.endChinematic();
    }
    void ClearRenderTexture()
    {
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = texture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = currentRT;
        //rawImage.texture = null;

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

    //IEnumerator checkVideo(){
    //    while(CG){

    //        yield return null;
    //    }
    //}
}

}