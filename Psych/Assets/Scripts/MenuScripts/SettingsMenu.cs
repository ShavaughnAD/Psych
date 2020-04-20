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
        masterMixer = Resources.Load("MainMixer") as AudioMixer;
    }

    public void SetReolution(int resolutionIndex)
    {
        Resolution _resolution = resolutions[resolutionIndex];
        Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreen);
        //TODO Make a "Are you sure text pop up so that player can revert"
    }

    public void SetMasterVolume(float volume)
    {
        masterMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        masterMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
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
}
