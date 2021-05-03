using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button m_ResumeButton;
    [SerializeField] private Button m_CheckpointButton;
    [SerializeField] private Button m_RestartButton;
    [SerializeField] private Button m_MainMenuButton;

    [SerializeField] private GameObject m_HUD;     // player's head-up-display
    [SerializeField] private GameObject m_Confirm; // menu for user to confirm their selection
    [SerializeField] private GameObject m_Options; // all the options to select when paused

    private ConfirmMenu m_ConfirmMenu;
    public bool IsPaused { get; private set; }

    private void Start()
    {
        m_ResumeButton.onClick.AddListener(TogglePause);
        m_CheckpointButton.onClick.AddListener(LoadCheckpoint);
        m_RestartButton.onClick.AddListener(Restart);
        m_MainMenuButton.onClick.AddListener(OpenMainMenu);

        m_ConfirmMenu = m_Confirm.GetComponent<ConfirmMenu>();
        IsPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = 1.0f - Time.timeScale;

        Cursor.lockState = (IsPaused) ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = IsPaused;

        m_HUD.SetActive(!IsPaused);
        m_Confirm.SetActive(false);
        m_Options.SetActive(IsPaused);
    }

    private void LoadCheckpoint()
    {
        if (!m_Confirm.activeSelf)
        {
            m_Confirm.SetActive(true);
            m_Options.SetActive(false);

            m_ConfirmMenu.YesAction(new UnityAction(() =>
            {

            }));

            m_ConfirmMenu.NoAction(new UnityAction(() =>
            {
                m_Confirm.SetActive(false);
                m_Options.SetActive(true);
            }));
        }
    }

    private void Restart()
    {
        m_Confirm.SetActive(true);
        m_Options.SetActive(false);

        m_ConfirmMenu.YesAction(new UnityAction(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1.0f;
        }));

        m_ConfirmMenu.NoAction(new UnityAction(() =>
        {
            m_Confirm.SetActive(false);
            m_Options.SetActive(true);
        }));
    }

    private void OpenMainMenu()
    {
        m_Confirm.SetActive(true);
        m_Options.SetActive(false);

        m_ConfirmMenu.YesAction(new UnityAction(() =>
        {
            SceneManager.LoadScene("MainMenu");
        }));

        m_ConfirmMenu.NoAction(new UnityAction(() =>
        {
            m_Confirm.SetActive(false);
            m_Options.SetActive(true);
        }));
    }
}