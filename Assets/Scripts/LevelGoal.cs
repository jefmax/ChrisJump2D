using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour
{
    public float delayBeforeLoad = 2.2f;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (other.CompareTag("Player") || other.CompareTag("Car"))
        {
            triggered = true;

            // ✨ Efecto visual al tocar la meta
            GoalGlowEffect glow = GetComponent<GoalGlowEffect>();
            if (glow != null)
                glow.ActivateGlow();

            // Mostrar mensaje UI
            LevelUIManager ui = FindObjectOfType<LevelUIManager>();
            if (ui != null)
                ui.ShowLevelComplete();

            // Iniciar carga del siguiente nivel
            StartCoroutine(LoadNextLevelAfterDelay());
        }
    }

    private IEnumerator LoadNextLevelAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex >= SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene("MainMenu");
        else
            SceneManager.LoadScene(nextIndex);
    }
}




