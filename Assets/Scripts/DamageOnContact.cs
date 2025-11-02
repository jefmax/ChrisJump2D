using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    public int damageAmount = 1;
    public float damageCooldown = 1f; // tiempo entre daños
    private float lastDamageTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.time < lastDamageTime + damageCooldown) return;

        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damageAmount);
            lastDamageTime = Time.time;
        }
    }
}
