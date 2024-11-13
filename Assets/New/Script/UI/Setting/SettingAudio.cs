using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Terbaru
{

    public class SettingAudio : MonoBehaviour
    {
        public Slider sliderMusic;
        public Slider sliderEffect;
        #region Main Menu

        public void startAudioMainMenu()
        {
            sliderMusic.value = PlayerPrefs.GetFloat("Music");
            sliderEffect.value = PlayerPrefs.GetFloat("Effect");
        }
        public void changeVolumeMusic()
        {
            PlayerPrefs.SetFloat("Music", sliderMusic.value);
        }

        public void changeVolumeEffect()
        {
            PlayerPrefs.SetFloat("Effect", sliderEffect.value);
        }

        public void changeValueMusic(float value)
        {
            sliderMusic.value += value;
            PlayerPrefs.SetFloat("Music", sliderMusic.value);
        }

        public void changeValueEffect(float value)
        {
            sliderEffect.value += value;
            PlayerPrefs.SetFloat("Effect", sliderMusic.value);
        }

        #endregion


        #region Ipad
        public AudioSource sourceEffect;
        public AudioSource sourceMusic;
        public TMPro.TMP_Text valueMusic;
        public TMPro.TMP_Text valueEffect;

        public void startSettingUI_Ipad()
        {
            valueEffect.text = Convert.ToInt32(PlayerPrefs.GetFloat("Effect") * 100).ToString();
            valueMusic.text = Convert.ToInt32(PlayerPrefs.GetFloat("Music") * 100).ToString();
            sliderEffect.value = PlayerPrefs.GetFloat("Effect");
            sliderMusic.value = PlayerPrefs.GetFloat("Music");
        }
        public void Ipad_changeVolumeMusic()
        {
            sourceMusic.volume = sliderMusic.value;
            valueMusic.text = Convert.ToInt32(sourceMusic.volume * 100).ToString();
            changeVolumeMusic();
        }

        public void Ipad_changeVolumeEffect()
        {
            sourceEffect.volume = sliderEffect.value;
            valueEffect.text = Convert.ToInt32(sourceEffect.volume * 100).ToString();
            changeVolumeEffect();
            //PlayerPrefs.SetFloat("Effect", sliderEffect.value);
        }

        public void Ipad_changeValueMusic(float value)
        {
            sliderMusic.value += value;

            Ipad_changeVolumeMusic();
        }

        public void Ipad_changeValueEffect(float value)
        {
            sliderEffect.value += value;

            Ipad_changeVolumeEffect();
            //PlayerPrefs.SetFloat("Effect", sliderMusic.value);
        }

        #endregion
    }
}
