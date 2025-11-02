using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelUIManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI levelCompleteText;
    public CanvasGroup completeGroup;

    [Header("Timing")]
    public float fadeDuration = 0.8f;
    public float showDuration = 1.5f;

    private void Awake()
    {
        if (completeGroup != null) completeGroup.alpha = 0f;
    }

    private void Start()
    {
        // Muestra nombre del nivel basado en buildIndex (puedes cambiar a nombre si prefieres)
        int idx = SceneManager.GetActiveScene().buildIndex;
        //levelText.text = SceneManager.GetActiveScene().name;
        levelText.text = "Level " + idx;
    }

    // Llamar desde LevelGoal cuando el jugador llega a la meta
    public void ShowLevelComplete()
    {
        StopAllCoroutines();
        StartCoroutine(DoShowComplete());
    }

    private IEnumerator DoShowComplete() 
    {
        if (levelCompleteText != null)
            levelCompleteText.text = "¡Nivel Completado!";

        // Fade-in
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            if (completeGroup != null)
                completeGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }
        if (completeGroup != null)
            completeGroup.alpha = 1f;

        // Mantener visible
        yield return new WaitForSeconds(showDuration);
        // (No hacemos fade-out automático aquí para que LevelGoal controle la carga de escena)
    }
}

