using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    [Space(3), Header("Options Buttons")]
    [SerializeField] private Button m_RestartButton = null;
    [SerializeField] private Button m_MainMenuButton = null;

    [Space(3), Header("UI Items")]
    [SerializeField] private GameObject m_HUD = null;
    [SerializeField] private GameObject m_Options = null;

    [Space(3), Header("Other")]
    [SerializeField] private float m_FadeInSpeed = 0.45f;

    private CanvasGroup m_OptionsCanvasGroup;

    private bool m_CoroutineIsRunning;

    private void Start()
    {
        m_RestartButton.onClick.AddListener(Restart);
        m_MainMenuButton.onClick.AddListener(OpenMainMenu);

        m_OptionsCanvasGroup = m_Options.GetComponent<CanvasGroup>();

        m_CoroutineIsRunning = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Notify();
        }
    }

    public void Notify()
    {
        if (!m_CoroutineIsRunning)
        {
            m_CoroutineIsRunning = true;
            StartCoroutine(FadeIn());

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            m_HUD.SetActive(false);
        }
    }

    private IEnumerator FadeIn()
    {
        m_Options.SetActive(true);
        m_OptionsCanvasGroup.alpha = 0.0f;

        while (m_OptionsCanvasGroup.alpha < 1.0f)
        {
            yield return null;
            m_OptionsCanvasGroup.alpha += m_FadeInSpeed * Time.deltaTime;
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
