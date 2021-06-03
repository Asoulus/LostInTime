using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    private const string resolution = "ResolutionOption";
    private const string quality = "QualityOption";
    private const string masterVolume = "VolumeOption";

    Resolution[] resolutions;

    public AudioMixer volumeMixer;

    public Slider volumeSlider;

    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;

    public Toggle fullscreenToggle;

    private int screenInt;

    private void Awake()
    {
        screenInt = PlayerPrefs.GetInt("FullScreenState");

        if (screenInt == 1)
        {
            fullscreenToggle.isOn = true;
        }
        else
        {
            fullscreenToggle.isOn = false;
        }


        resolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(resolution, resolutionDropdown.value);
            SavePreferences();
        }));

        qualityDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(quality, qualityDropdown.value);
            SavePreferences();
        }));
    }

    private void Start()
    {
        //****************AUDIO*****************
        volumeSlider.value = PlayerPrefs.GetFloat(masterVolume, 1f);
        volumeMixer.SetFloat("volume", PlayerPrefs.GetFloat(masterVolume));


        //==============QUALITY==============
        qualityDropdown.value = PlayerPrefs.GetInt(quality, 1);


        //==============RESOLUTION==============
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt(resolution, currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();
    }

    public void ChangeVolume(float _volume)
    {
        PlayerPrefs.SetFloat(masterVolume, _volume);
        volumeMixer.SetFloat("volume", PlayerPrefs.GetFloat(masterVolume));
    }

    public void SetQuality(int _qualityIndex)
    {
        QualitySettings.SetQualityLevel(_qualityIndex);
    }

    public void SetFullscreen(bool _isFullscreen)
    {
        Screen.fullScreen = _isFullscreen;

        if (!_isFullscreen)
        {
            PlayerPrefs.SetInt("FullScreenState", 0);
        }
        else
        {
            PlayerPrefs.SetInt("FullScreenState", 1);
        }
    }

    public void SetResolution(int _resolutionIndex)
    {
        Resolution resolution = resolutions[_resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SavePreferences()
    {
        PlayerPrefs.Save();
    }
}
