using UnityEngine;

public class GoalGlowEffect : MonoBehaviour
{
    private SpriteRenderer sr;
    private bool isActivated = false;
    private float pulseSpeed = 4f;
    private Color originalColor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    void Update()
    {
        if (!isActivated)
        {
            // Brillo suave constante antes de tocarlo
            float alpha = 0.7f + Mathf.Sin(Time.time * pulseSpeed) * 0.3f;
            sr.color = new Color(originalColor.r * alpha, originalColor.g * alpha, originalColor.b * alpha, 1f);
        }
    }

    public void ActivateGlow()
    {
        isActivated = true;
        StartCoroutine(FlashEffect());
    }

    private System.Collections.IEnumerator FlashEffect()
    {
        for (int i = 0; i < 5; i++)
        {
            sr.color = Color.white; // brillo fuerte
            yield return new WaitForSeconds(0.1f);
            sr.color = originalColor; // vuelve al color original
            yield return new WaitForSeconds(0.1f);
        }
    }
}
