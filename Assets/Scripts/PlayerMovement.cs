using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float horizontal;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private int jumpCount = 0;
    public int maxJumps = 2;

    // 🟢 NUEVO: referencia al FirePoint
    public Transform firePoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // ✅ Referencia al Animator
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        // Movimiento horizontal
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed) horizontal = -1;
            else if (Keyboard.current.dKey.isPressed) horizontal = 1;
            else horizontal = 0;

            // Salto / Doble salto
            if (Keyboard.current.spaceKey.wasPressedThisFrame && jumpCount < maxJumps)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount++;

                anim.SetBool("isJumping", true); // ⬆️ Cambiar a animación de salto
            }
        }

        // Girar el personaje
        if (horizontal != 0)
        {
            float direction = Mathf.Sign(horizontal);
            transform.localScale = new Vector3(direction, 1f, 1f);

            // 🟢 NUEVO: hacer que el FirePoint también se voltee
            if (firePoint != null)
            {
                firePoint.localScale = new Vector3(direction, 1f, 1f);

                firePoint.localRotation = Quaternion.Euler(0, direction == 1 ? 0 : 180, 0);
            }
        }

        // 🔄 Actualizar velocidad al Animator
        anim.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0f) return;
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detectar si colisionó con el suelo
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                jumpCount = 0;
                anim.SetBool("isJumping", false); // ⬇️ Volver a animación normal
                break;
            }
        }
    }
}

