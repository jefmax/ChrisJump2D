using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button audioButton;
    public Button exitButton;

    private bool audioEnabled = true;

    void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        
    }

    void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    void ToggleAudio()
    {
        audioEnabled = !audioEnabled;
        AudioListener.volume = audioEnabled ? 1 : 0;
        audioButton.GetComponentInChildren<Text>().text = audioEnabled ? "Audio: ON" : "Audio: OFF";
    }

    void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}

