using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 2f;

    private Rigidbody2D rb;

    private Vector2 initialVelocity = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);

        // Si PlayerShoot nos dio una velocidad inicial, úsala.
        if (initialVelocity != Vector2.zero)
        {
            rb.velocity = initialVelocity;
        }
        else
        {
            // Comportamiento por defecto (si no se pasó una velocidad)
            rb.velocity = transform.right * speed;
        }
    }

    public void SetDirection(Vector2 direction)
    {
        initialVelocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(collision.collider.gameObject);
        }

        Destroy(gameObject);
    }
}

