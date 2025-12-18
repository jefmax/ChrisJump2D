using UnityEngine;

public class AirplaneMovement : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 5f;
    public float verticalSpeed = 4f;
    public float horizontalControlSpeed = 3f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");     // W / S
        float horizontal = Input.GetAxis("Horizontal"); // A / D

        Vector2 velocity = rb.linearVelocity;

        // Avance automático
        velocity.x = forwardSpeed;

        // Control manual
        velocity.y = vertical * verticalSpeed;
        velocity.x += horizontal * horizontalControlSpeed;

        rb.linearVelocity = velocity;
    }
}

