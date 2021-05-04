using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuEvents : MonoBehaviour
{
    [Space(3), Header("Menus")]
    [SerializeField] private GameObject m_Main;
    [SerializeField] private GameObject m_Play;
    [SerializeField] private GameObject m_Settings;

    [Space(3), Header("Buttons")]
    [SerializeField] private Button m_PlayButton;
    [SerializeField] private Button m_SettingsButton;
    [SerializeField] private Button m_QuitGameButton;
    [SerializeField] private Button m_BackToMainButton;

    [Space(3), Header("Levels")]
    [SerializeField] private Button m_JoakimButton;
    [SerializeField] private Button m_OssianButton;
    [SerializeField] private Button m_RobertButton;
    [SerializeField] private Button m_TobiasButton;
    [SerializeField] private Button m_TimButton;

    private GameObject m_CurrentMenu;

    private void Awake()
    {
        m_PlayButton.onClick.AddListener(Play);
        m_SettingsButton.onClick.AddListener(OpenSettings);
        m_QuitGameButton.onClick.AddListener(QuitGame);
        m_BackToMainButton.onClick.AddListener(BackToMenu);

        m_JoakimButton.onClick.AddListener(LoadJoakim);
        m_OssianButton.onClick.AddListener(LoadOssian);
        m_RobertButton.onClick.AddListener(LoadRobert);
        m_TobiasButton.onClick.AddListener(LoadTobias);
        m_TimButton.onClick.AddListener(LoadTim);
    }

    #region ButtonEvents

    public void Play()
    {
        m_Main.SetActive(false);
        m_Play.SetActive(true);
        m_BackToMainButton.gameObject.SetActive(true);

        m_CurrentMenu = m_Play;
    }
    public void OpenSettings()
    {
        m_Main.SetActive(false);
        m_Settings.SetActive(true);
        m_BackToMainButton.gameObject.SetActive(true);

        m_CurrentMenu = m_Settings;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void BackToMenu()
    {
        m_Main.SetActive(true);
        m_CurrentMenu.SetActive(false);
        m_BackToMainButton.gameObject.SetActive(false);
    }

    #endregion

    #region LoadScenes

    public void LoadJoakim()
    {
        SceneManager.LoadScene("Joakim_Scene");
    }
    public void LoadOssian()
    {
        SceneManager.LoadScene("Ossians_testscen");
    }
    public void LoadRobert()
    {
        SceneManager.LoadScene("Roberts_Scene");
    }
    public void LoadTobias()
    {
        SceneManager.LoadScene("Tobias_Scene");
    }
    public void LoadTim()
    {
        SceneManager.LoadScene("Tim_Scene");
    }

    #endregion
}
