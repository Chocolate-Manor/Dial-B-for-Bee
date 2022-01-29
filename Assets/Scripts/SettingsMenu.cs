using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private VolumeProfile mainCameraProfile;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectsSlider;
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Toggle lensDistortionToggle;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Button backButton;
    [SerializeField] private Button saveButton;
    private Resolution[] resolutions;
    
    void Start()
    {
        // Get all possible resolutions
        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        resolutionDropdown.AddOptions(resolutions.Select(resolution => resolution.ToString()).ToList());
        LoadPreferences();
        
        // //Add all listeners
        // lensDistortionToggle.onValueChanged.AddListener(delegate {  SetLensDistortion(lensDistortionToggle.isOn);  });
        // fullScreenToggle.onValueChanged.AddListener(delegate {  SetFullScreen(fullScreenToggle.isOn);  });
        // musicSlider.onValueChanged.AddListener(delegate {  SetMusicVolume(musicSlider.value);  });
        // soundEffectsSlider.onValueChanged.AddListener(delegate {  SetSoundEffectVolume(soundEffectsSlider.value);  });
        // resolutionDropdown.onValueChanged.AddListener(delegate {  SetResolution(resolutionDropdown.value);  });
        // backButton.onClick.AddListener(CloseSettings);
        // saveButton.onClick.AddListener(SavePreferences);
    }

    // private void Awake()
    // {
    //     LoadPreferences();
    // }

    public void SetResolution(int value)
    {
        int index = Math.Max(value, resolutions.Length - 1);
        Resolution chosenResolution = resolutions[index];
        Screen.SetResolution(chosenResolution.width, chosenResolution.height, fullScreenToggle.isOn);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("Music Volume", Mathf.Log10(value) * 20);
    }

    public void SetSoundEffectVolume(float value)
    {
        audioMixer.SetFloat("Sound Effects Volume", Mathf.Log10(value) * 20);
    }

    public void SetLensDistortion(bool isDistorted)
    {
        mainCameraProfile.TryGet(out LensDistortion lensDistortion);
        lensDistortion.active = isDistorted;
    }
    
    public void SavePreferences()
    {
        PlayerPrefs.SetInt("LensDistortionPreference", Convert.ToInt32(lensDistortionToggle.isOn));
        PlayerPrefs.SetInt("FullScreenPreference", Convert.ToInt32(fullScreenToggle.isOn));
        PlayerPrefs.SetFloat("MusicVolumePreference", musicSlider.value);
        PlayerPrefs.SetFloat("SoundEffectsVolumePreference", soundEffectsSlider.value);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
    }

    public void LoadPreferences()
    {
        lensDistortionToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("LensDistortionPreference", 1));
        fullScreenToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("FullScreenPreference", 1));
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolumePreference", 1.0f);
        soundEffectsSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolumePreference", 1.0f);
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference", Screen.resolutions.Length - 1);
    }

    public void CloseSettings()
    {
        //Load preferences before leaving the menu to discard any unwanted changes.
        LoadPreferences();
        gameObject.GetComponentInChildren<CanvasRenderer>().gameObject.SetActive(false);
    }

}
