using EasyTransition;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CG_Manager : MonoBehaviour
{
    public playerProfil profil;
    public VideoPlayer videoPlayer;

    public VideoClip[] clips;

    int index;

    public string NamaScene;

    private void Start()
    {
        index = PlayerPrefs.GetInt("indexAnimasi");
        mulai();
    }

    void mulai()
    {
        videoPlayer.clip = clips[index];
        videoPlayer.Play();
        InvokeRepeating("checkOver", .01f, .01f);
    //    videoPlayer.loopPointReached += CheckOver;
    }

    



    

    private void checkOver()
    {
        long playerCurrentFrame = videoPlayer.frame;
        long playerFrameCount = Convert.ToInt64(videoPlayer.frameCount);

        Debug.Log(playerCurrentFrame + " current Frame ");
        Debug.Log(playerFrameCount+ " count Frame ");

        if (playerCurrentFrame < playerFrameCount - 10)
        {
            print("VIDEO IS PLAYING");
        }
        else
        {
            print("VIDEO IS OVER");
            //Do w.e you want to do for when the video is done playing.
            FindObjectOfType<DemoLoadScene>().LoadScene(NamaScene);
            //Cancel Invoke since video is no longer playing
            CancelInvoke("checkOver");
        }
    }
    
}
