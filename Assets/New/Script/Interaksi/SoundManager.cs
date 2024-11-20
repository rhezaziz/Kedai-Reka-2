using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Sound Backsound")]
    public AudioSource BacksoundSource;
    public AudioClip[] backsoundClip;

    [Header("Sound Effect")]
    public AudioSource effectSource;
    public AudioClip[] InvorenmentClip;
    public AudioClip[] sfxUIClip;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start()
    {
        BacksoundSource.volume = PlayerPrefs.GetFloat("Music");
        effectSource.volume = PlayerPrefs.GetFloat("Effect");
    }
    public void playSoundQuest(){
        BacksoundSource.Stop();
        BacksoundSource.clip = backsoundClip[2]; 
        BacksoundSource.Play();
    }

    public void playSoundAsrama(){
        BacksoundSource.Stop();
        BacksoundSource.clip = backsoundClip[1]; 
        BacksoundSource.Play();    
    }

    public void stopAudio(){
        BacksoundSource.Stop();
    }

    public void sfx(int index){
        effectSource.PlayOneShot(InvorenmentClip[index]);
    }

    public void stopStx(){
        effectSource.Stop();
    }

    public void sfx(AudioClip clip){
        effectSource.Stop();
        effectSource.PlayOneShot(clip);
    }

    public void uiSFX(int index){
        effectSource.PlayOneShot(sfxUIClip[index]);
    }

    public void stopSFX(){
        effectSource.Stop();   
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
