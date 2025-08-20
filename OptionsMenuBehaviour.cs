using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenuBehaviour : MonoBehaviour
{
    public AudioMixer masterMixer;
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;
    public Toggle fullScreenToggle;

    int currentResolutionIndex = 0;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for(int i=0;i< resolutions.Length;i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        SetAllOptions();
    }

    public void SetVolume(float volume)
    {
        masterMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        if(isFullScreen)
        {
            PlayerPrefs.SetInt("FullScreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("FullScreen", 0);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
        PlayerPrefs.SetInt("ScreenRes", resolutionIndex);
    }

    public void SetAllOptions()
    {
        if (PlayerPrefs.HasKey("ScreenRes"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("ScreenRes");
        }
        else
        {
            resolutionDropdown.value = currentResolutionIndex;
        }
        resolutionDropdown.RefreshShownValue();

        if (PlayerPrefs.HasKey("Volume"))
        {
            masterMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }

        if (PlayerPrefs.GetInt("FullScreen") == 0)
        {
            Screen.fullScreen = false;
            fullScreenToggle.isOn = false;
        }
        if (PlayerPrefs.GetInt("FullScreen") == 1)
        {
            Screen.fullScreen = true;
            fullScreenToggle.isOn = true;
        }
    }
}
