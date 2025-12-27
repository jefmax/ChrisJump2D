using UnityEngine;
using UnityEngine.SceneManagement;

public class AirplaneHealth : MonoBehaviour
{
    public int maxHealth = 2;
    private int currentHealth;

    private SpriteRenderer spriteRenderer;
    private bool isInvulnerable = false;
    public float invulnerableTime = 1.5f;

    private bool isDead = false;
    private float restartDelay = 2f;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int amount)
    {
        if (isInvulnerable || isDead) return;

        currentHealth -= amount;
        Debug.Log("Airplane Lives: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityFlash());
        }
    }

    private void Die()
    {
        isDead = true;
        spriteRenderer.color = Color.red;
        Debug.Log("Airplane destroyed");

        StartCoroutine(RestartAfterDelay());
    }

    private System.Collections.IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private System.Collections.IEnumerator InvulnerabilityFlash()
    {
        isInvulnerable = true;
        float elapsed = 0f;
        Color originalColor = spriteRenderer.color;

        while (elapsed < invulnerableTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        spriteRenderer.enabled = true;
        spriteRenderer.color = originalColor;
        isInvulnerable = false;
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;

        GUI.Label(new Rect(10, 10, 200, 30), "✈️ Life: " + currentHealth, style);

        if (isDead)
        {
            GUIStyle gameOverStyle = new GUIStyle();
            gameOverStyle.fontSize = 40;
            gameOverStyle.alignment = TextAnchor.MiddleCenter;
            gameOverStyle.normal.textColor = Color.white;

            GUI.Label(
                new Rect(Screen.width / 2 - 150, Screen.height / 2 - 30, 300, 60),
                "GAME OVER",
                gameOverStyle
            );
        }
    }
}
