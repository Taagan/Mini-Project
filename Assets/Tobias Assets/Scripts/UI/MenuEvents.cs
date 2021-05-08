using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuEvents : MonoBehaviour
{
    [Space(3), Header("Menus")]
    [SerializeField] private GameObject m_Main;
    [SerializeField] private GameObject m_Settings;

    [Space(3), Header("Buttons")]
    [SerializeField] private Button m_PlayButton;
    [SerializeField] private Button m_SettingsButton;
    [SerializeField] private Button m_QuitGameButton;
    [SerializeField] private Button m_BackToMainButton;

    private GameObject m_CurrentMenu;

    private void Awake()
    {
        m_PlayButton.onClick.AddListener(Play);
        m_SettingsButton.onClick.AddListener(OpenSettings);
        m_QuitGameButton.onClick.AddListener(QuitGame);
        m_BackToMainButton.onClick.AddListener(BackToMenu);
    }

    #region ButtonEvents

    public void Play()
    {
        SceneManager.LoadScene("ossians_bana1");
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
}
