using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundToggle : MonoBehaviour
{
    public AudioSource musicSource;   // El AudioSource del SoundManager
    public TMP_Text buttonText;       // Texto del botón

    public void ToggleSound()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
            buttonText.text = "Sound OFF";
        }
        else
        {
            musicSource.Play();
            buttonText.text = "Audio";
        }
    }
}
