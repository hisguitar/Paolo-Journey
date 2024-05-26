using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Setting : MonoBehaviour
{
    public Canvas setting;
    
    [SerializeField] public Slider volumeSlider;
    public AudioSource audioSource;

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1);
        audioSource.volume = volumeSlider.value;

        volumeSlider.onValueChanged.AddListener(delegate {ChangeVolume(volumeSlider.value); });
    }
    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }
    

    public void SettingButton()
    {
        setting.gameObject.SetActive(true);
    }
    public void QuitSettingButton()
    {
        setting.gameObject.SetActive(false);
    }

}
