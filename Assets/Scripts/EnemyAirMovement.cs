using UnityEngine;

public class EnemyAirMovement : MonoBehaviour
{
    public float speed = 2f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Movimiento constante hacia la izquierda
        rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
    }
}

