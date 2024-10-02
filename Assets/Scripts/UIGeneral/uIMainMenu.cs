using UnityEngine;
using UnityEngine.SceneManagement;

public class uIMainMenu : MonoBehaviour
{
    public GameObject MenuUI, settingsMenu;

    public void StartGame()
    {
        // Carga la siguiente escena, asumiendo que la escena del juego está en el índice 1
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        MenuUI.SetActive(false);
        Debug.Log("Abrir ajustes");
    }

    public void ReturnUI()
    {
        MenuUI.SetActive(true);
        settingsMenu.SetActive(false);
    }
}
