using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 6f;
    public Transform playerSeat;

    public Transform groundCheck;
    public float checkRadius = 0.1f;
    public LayerMask groundLayer;

    private bool isGrounded;

    private Rigidbody2D rb;
    private bool playerInside = false;
    private Rigidbody2D playerRb;
    private Collider2D playerCollider;
    private MonoBehaviour playerMovementScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!playerInside) return;

        // Avance automático
        rb.velocity = new Vector2(speed, rb.velocity.y);

        // Salto
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!playerInside && collision.collider.CompareTag("Player"))
        {
            EnterCar(collision.collider.gameObject);
        }
    }

    void EnterCar(GameObject player)
    {
        playerInside = true;

        // Reposicionar al asiento
        player.transform.SetParent(playerSeat);
        player.transform.localPosition = Vector3.zero;

        // Desactivar físicas y colisión del Player
        playerRb = player.GetComponent<Rigidbody2D>();
        playerCollider = player.GetComponent<Collider2D>();
        playerMovementScript = player.GetComponent<MonoBehaviour>();

        playerRb.simulated = false; // En vez de isKinematic: no afecta física en lo absoluto
        playerCollider.enabled = false;
        playerMovementScript.enabled = false;
    }
}

