using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMethod : MonoBehaviour
{
    public GameObject pauseMenuUI, settingsMenu, win, lose;

    public GameObject uiPlayer, uiBoss;
    public Button resumeButton;
    public Button settingsButton;
    public Button quitButton;
    public Button returnButton;

    private bool isPaused = false, iswin = false, islose;
    public static UIMethod uIMethod;
    private void Awake()
    {
        if (uIMethod == null) uIMethod
         = this;
        else Destroy(this);
    }
    void Start()
    {
        resumeButton.onClick.AddListener(ResumeGame);
        settingsButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(QuitToMainMenu);

        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && !iswin && !islose)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        uiPlayer.SetActive(true);
        uiBoss.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        uiPlayer.SetActive(false);
        uiBoss.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReturnUI()
    {
        pauseMenuUI.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
        Debug.Log("Abrir ajustes");
    }

    public void QuitToMainMenu()
    {
        // Aquí puedes cambiar a la escena del menú principal
        Time.timeScale = 1f; // Asegúrate de restaurar la escala de tiempo
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        // Reiniciar la escena actual
        Time.timeScale = 1f; // Asegúrate de restaurar la escala de tiempo antes de reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        iswin = false;
    }

    public void WinMenu()
    {
        Time.timeScale = 0f;
        uiPlayer.SetActive(false);
        uiBoss.SetActive(false);
        iswin = true;
        win.SetActive(true);
    }

    public void LoseMenu()
    {
        Time.timeScale = 0f;
        uiPlayer.SetActive(false);
        uiBoss.SetActive(false);
        islose = true;
        lose.SetActive(true);
    }
}
