using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject gameUI;
    public GameObject pauseMenu;

    private bool isGamePaused = false;

    void Start()
    {
        // Mostrar solo el menú principal al iniciar
        mainMenu.SetActive(true);
        gameUI.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void PlayGame()
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void PauseGame()
    {
        if (isGamePaused) return;

        pauseMenu.SetActive(true);
        gameUI.SetActive(false);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    
    public void ExitGame()
    {
        Debug.Log("Salir del juego...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}



