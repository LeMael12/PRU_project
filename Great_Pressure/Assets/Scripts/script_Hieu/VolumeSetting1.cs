using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider hurtSlider;


    public void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else {
        SetMusicVolume();
            SetHurtVolume();
        }
    }
    public void SetMusicVolume() {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void SetHurtVolume()
    {
        float volume = hurtSlider.value;
        myMixer.SetFloat("Hurt", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("hurtVolume", volume);
    }
    public void LoadVolume() {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        hurtSlider.value = PlayerPrefs.GetFloat("HurtVolume");

        SetMusicVolume();
        SetHurtVolume();
    }

}
