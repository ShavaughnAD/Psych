using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//Reference: Brackeys. (2017). SETTINGS MENU in Unity. Retrieved from https://www.youtube.com/watch?v=YOaYQrN1oYQ
public class SettingsMenu : MonoBehaviour
{
    #region Public Variables
    public AudioMixer masterMixer;
    public AudioMixer musicMixer;
    public AudioMixer soundEffectsMixer;
    public Dropdown resolutionDropdown;
    public Slider MasterSlider;
    public Slider BGMSlider;
    public Slider SFxSlider;
    #endregion

    #region Private Variables
    Resolution[] resolutions;
    #endregion

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

             if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        InitAudio();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution _resolution = resolutions[resolutionIndex];
        Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreen);
        //TODO Make a "Are you sure text pop up so that player can revert"
    }

    public void SetMasterVolume(float volume)
    {
        Debug.Log(Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Master_Vol", Mathf.Log10(volume) * 20);
        masterMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("BGM_Vol", Mathf.Log10(volume) * 20);
        masterMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFx_Vol", Mathf.Log10(volume) * 20);
        masterMixer.SetFloat("soundEffectsVolume", Mathf.Log10(volume) * 20);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void InitAudio()
    {
        masterMixer = Resources.Load("MainMixer") as AudioMixer;
        if (masterMixer == null)
            Debug.LogError("SettingMenu Cant find MasterMixer!");

        masterMixer.SetFloat("volume", PlayerPrefs.GetFloat("Master_Vol"));
        masterMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("BGM_Vol"));
        masterMixer.SetFloat("soundEffectsVolume", PlayerPrefs.GetFloat("SFx_Vol"));

        MasterSlider.value = Mathf.Pow(10,(PlayerPrefs.GetFloat("Master_Vol") / 20));
        BGMSlider.value = Mathf.Pow(10, (PlayerPrefs.GetFloat("BGM_Vol") / 20));
        SFxSlider.value = Mathf.Pow(10, (PlayerPrefs.GetFloat("SFx_Vol") / 20));
    }

    public void ResetAudioSetting()
    {
        PlayerPrefs.SetFloat("Master_Vol", 1);
        PlayerPrefs.SetFloat("SFx_Vol", 1);
        PlayerPrefs.SetFloat("BGM_Vol", 1);

        masterMixer.SetFloat("volume", 0);
        masterMixer.SetFloat("musicVolume", 0);
        masterMixer.SetFloat("soundEffectsVolume", 0);
    }
}
