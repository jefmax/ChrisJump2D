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
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount++;

                anim.SetBool("isJumping", true); // ⬆️ Cambiar a animación de salto
            }
        }
        if (horizontal != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontal), 1f, 1f);
        }

        // 🔄 Actualizar velocidad al Animator
        anim.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0f) return;
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
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

