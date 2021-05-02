using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Space(3), Header("Settings Objects")]
    [SerializeField] private GameObject m_Resolution;
    [SerializeField] private GameObject m_TextureQuality;
    [SerializeField] private GameObject m_ShadowQuality;
    [SerializeField] private GameObject m_AntiAliasing;
    [SerializeField] private GameObject m_VerticalSync;
    [SerializeField] private GameObject m_Fullscreen;
    [SerializeField] private GameObject m_MasterVolume;
    [SerializeField] private GameObject m_MusicVolume;
    [SerializeField] private GameObject m_EffectsVolume;
    [SerializeField] private GameObject m_MouseSensitivity;

    [Space(3), Header("Other")]
    [SerializeField] private Button m_ApplyButton = null;

    private Dropdown m_ResolutionDropdown = null;
    private Dropdown m_TextureQualityDropdown = null;
    private Dropdown m_ShadowQualityDropdown = null;
    private Dropdown m_AntiAliasingDropdown = null;
    private Dropdown m_VSyncDropdown = null;
    private Toggle m_FullscreenToggle = null;
    private Slider m_MasterVolumeSlider = null;
    private Slider m_MusicVolumeSlider = null;
    private Slider m_EffectsVolumeSlider = null;
    private Slider m_MouseSensSlider = null;
    private Text m_MasterVolumeText = null;
    private Text m_MusicVolumeText = null;
    private Text m_EffectsVolumeText = null;
    private Text m_MouseSensText = null;

    private SettingsManager m_SettingsManager = null;

    private void Awake()
    {
        m_SettingsManager = GetComponent<SettingsManager>();

        m_ResolutionDropdown = m_Resolution.GetComponentInChildren<Dropdown>();
        m_TextureQualityDropdown = m_TextureQuality.GetComponentInChildren<Dropdown>();
        m_ShadowQualityDropdown = m_ShadowQuality.GetComponentInChildren<Dropdown>();
        m_AntiAliasingDropdown = m_AntiAliasing.GetComponentInChildren<Dropdown>();
        m_VSyncDropdown = m_VerticalSync.GetComponentInChildren<Dropdown>();

        m_FullscreenToggle = m_Fullscreen.GetComponentInChildren<Toggle>();

        m_MasterVolumeSlider = m_MasterVolume.GetComponentInChildren<Slider>();
        m_MusicVolumeSlider = m_MusicVolume.GetComponentInChildren<Slider>();
        m_EffectsVolumeSlider = m_EffectsVolume.GetComponentInChildren<Slider>();
        m_MouseSensSlider = m_MouseSensitivity.GetComponentInChildren<Slider>();

        m_MasterVolumeText = m_MasterVolume.GetComponentInChildren<Text>();
        m_MusicVolumeText = m_MusicVolume.GetComponentInChildren<Text>();
        m_EffectsVolumeText = m_EffectsVolume.GetComponentInChildren<Text>();
        m_MouseSensText = m_MouseSensitivity.GetComponentInChildren<Text>();

        m_MasterVolumeSlider.onValueChanged.AddListener(delegate { OnMasterVolumeChange(); });
        m_MusicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        m_EffectsVolumeSlider.onValueChanged.AddListener(delegate { OnEffectsVolumeChange(); });
        m_MouseSensSlider.onValueChanged.AddListener(delegate { OnMouseSensChange(); });
        m_ApplyButton.onClick.AddListener(delegate { ApplyChanges(); });
    }

    private void OnMasterVolumeChange()
    {
        m_MasterVolumeText.text = "Master Volume = " + (int)(m_MasterVolumeSlider.value * 100) + "%";
    }

    private void OnMusicVolumeChange()
    {
        m_MusicVolumeText.text = "Music Volume = " + (int)(m_MusicVolumeSlider.value * 100) + "%";
    }

    private void OnEffectsVolumeChange()
    {
        m_EffectsVolumeText.text = "Sound Effects Volume = " + (int)(m_EffectsVolumeSlider.value * 100) + "%";
    }

    private void OnMouseSensChange()
    {
        m_MouseSensText.text = "Mouse Sensitivity = " + m_MouseSensSlider.value;
    }

    private void ApplyChanges()
    {
        GameSettings gs = SaveToGameSettings();

        m_SettingsManager.Save(ref gs);
        m_SettingsManager.Apply();
    }

    /// <summary>
    /// Get current selected settings from UI and save to GameSettings
    /// </summary>
    private GameSettings SaveToGameSettings()
    {
        GameSettings currentSettings = new GameSettings();

        currentSettings.IsFullscreen = m_FullscreenToggle.isOn;
        currentSettings.ResolutionIndex = m_ResolutionDropdown.value;

        currentSettings.TextureQuality = (5 - m_TextureQualityDropdown.value);
        currentSettings.ShadowQuality = m_ShadowQualityDropdown.value;
        currentSettings.AntiAliasing = m_AntiAliasingDropdown.value;
        currentSettings.VerticalSync = m_VSyncDropdown.value;

        currentSettings.MasterVolume = m_MasterVolumeSlider.value;
        currentSettings.MusicVolume = m_MusicVolumeSlider.value;
        currentSettings.EffectsVolume = m_EffectsVolumeSlider.value;
        currentSettings.MouseSensitivity = m_MouseSensSlider.value;

        return currentSettings;
    }

    /// <summary>
    /// Use GameSettings to assign a value to each corresponding UI element
    /// </summary>
    public void Refresh(ref GameSettings gameSettings)
    {
        m_FullscreenToggle.isOn = gameSettings.IsFullscreen;

        m_ResolutionDropdown.value = gameSettings.ResolutionIndex;
        m_TextureQualityDropdown.value = (5 - gameSettings.TextureQuality);
        m_ShadowQualityDropdown.value = gameSettings.ShadowQuality;
        m_AntiAliasingDropdown.value = gameSettings.AntiAliasing;
        m_VSyncDropdown.value = gameSettings.VerticalSync;

        if (m_ResolutionDropdown.options.Count == 0)
        {
            foreach (Resolution resolution in m_SettingsManager.Resolutions)
            {
                m_ResolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
            }
        }
        m_ResolutionDropdown.value = gameSettings.ResolutionIndex;

        m_ResolutionDropdown.RefreshShownValue();

        m_MasterVolumeSlider.value = gameSettings.MasterVolume;
        m_MasterVolumeText.text = "Master Volume = " + (int)(m_MasterVolumeSlider.value * 100) + "%";

        m_MusicVolumeSlider.value = gameSettings.MusicVolume;
        m_MusicVolumeText.text = "Music Volume = " + (int)(m_MusicVolumeSlider.value * 100) + "%";

        m_EffectsVolumeSlider.value = gameSettings.EffectsVolume;
        m_EffectsVolumeText.text = "Sound Effects Volume = " + (int)(m_EffectsVolumeSlider.value * 100) + "%";

        m_MouseSensSlider.value = gameSettings.MouseSensitivity;
        m_MouseSensText.text = "Mouse Sensitivity = " + m_MouseSensSlider.value;
    }
}
