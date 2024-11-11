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
            UiManager.instance.ChinematicPanel.SetActive(true);
            var Perempuan = Player.GetChild(1).transform;
            Perempuan.gameObject.SetActive(true);
            
            if(col)
                col.enabled = false;
            // Player.GetComponent<Collider>().enabled = false;
            // Player.GetComponent<Rigidbody>().isKinematic = true;
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
            extendAction?.Invoke();
            PanelUtama.SetActive(true);
            
            FindObjectOfType<Controller>().currentState(state.Default);
            
            
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