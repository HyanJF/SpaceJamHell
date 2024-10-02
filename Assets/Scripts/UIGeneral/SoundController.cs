using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public AudioSource musicSource;  
    public AudioSource sfxSource;    

    public Slider musicSlider;      
    public Slider sfxSlider;        

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f); 
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume; 
        PlayerPrefs.SetFloat("MusicVolume", volume); 
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume; 
        PlayerPrefs.SetFloat("SFXVolume", volume); 
    }
}
