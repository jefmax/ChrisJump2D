using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    public float jumpForce = 8f;    // fuerza del salto
    public int damage = 1;          // daño al jugador

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si toca el suelo, salta de nuevo
        if (collision.collider.CompareTag("Ground"))
        {
            Jump();
        }

        // Si toca al jugador, le causa daño
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth player = collision.collider.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }

    private void Jump()
    {
        // Reinicia velocidad vertical antes de aplicar fuerza
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
