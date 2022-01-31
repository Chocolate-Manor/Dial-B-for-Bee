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
    [SerializeField] private GameObject panelUI;
    private Resolution[] resolutions;

    private const int DefaultLensDistortion = 0;
    private const int DefaultFullScreen = 1;
    private const float DefaultMusicVolume = 1.0f;
    private const float DefaultSoundEffectVolume = 1.0f;
    private int defaultResolutionIndex;

    private void Awake()
    {
        // Get all possible resolutions
        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        resolutionDropdown.AddOptions(resolutions.Select(resolution => resolution.ToString()).ToList());
        defaultResolutionIndex = Screen.resolutions.Length - 1;
        LoadPreferences();
    }
    
    //Script component is disabled by default in the prefab.
    //Enable the script when activating the UI to use the update method.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            backButton.onClick.Invoke();
        }
    }

    public void SetResolution(int value)
    {
        int index = Math.Min(value, resolutions.Length - 1);
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
        lensDistortionToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("LensDistortionPreference", DefaultLensDistortion));
        fullScreenToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("FullScreenPreference", DefaultFullScreen));
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolumePreference", DefaultMusicVolume);
        soundEffectsSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolumePreference", DefaultSoundEffectVolume);
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference", defaultResolutionIndex);
    }

    public void ResetSettings()
    {
        lensDistortionToggle.isOn = Convert.ToBoolean(DefaultLensDistortion);
        fullScreenToggle.isOn = Convert.ToBoolean(DefaultFullScreen);
        musicSlider.value = DefaultMusicVolume;
        soundEffectsSlider.value =  DefaultSoundEffectVolume;
        resolutionDropdown.value = defaultResolutionIndex;
    }

    public void CloseSettings()
    {
        SavePreferences();
        panelUI.SetActive(false);
        enabled = false;
    }

}
