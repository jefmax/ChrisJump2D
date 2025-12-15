using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 2;
    private int currentHealth;

    private SpriteRenderer spriteRenderer;
    private bool isInvulnerable = false;
    public float invulnerableTime = 2f;

    private bool isDead = false;
    private float deathTimer = 2f; // segundos antes de reiniciar

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int amount)
    {
        if (isInvulnerable || isDead) return;

        currentHealth -= amount;
        Debug.Log("Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityFlash());
        }
    }

    public void AddLife(int amount = 1)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Debug.Log("Vida aumentada. Vida actual: " + currentHealth);
    }

    private void Die()
    {
        isDead = true;
        spriteRenderer.color = Color.red; // tono muerto
        Debug.Log("El jugador ha muerto");
        StartCoroutine(RestartAfterDelay());
    }

    private System.Collections.IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(deathTimer);
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
        GUI.Label(new Rect(10, 10, 200, 30), "❤️ Life: " + currentHealth, style);

        if (isDead)
        {
            GUIStyle deathStyle = new GUIStyle();
            deathStyle.fontSize = 40;
            deathStyle.alignment = TextAnchor.MiddleCenter;
            deathStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 20, 300, 50), "💀 Jugador ha muerto 💀", deathStyle);
        }
    }
}

