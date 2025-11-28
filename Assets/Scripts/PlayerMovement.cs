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

    // 🟢 FirePoint
    public Transform firePoint;

    private MobileInput mi;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        mi = MobileInput.instance;
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        horizontal = 0; // 🔴 IMPORTANTE: limpiamos antes de leer teclas o móvil

        // 🟢--------------- CONTROLES DE TECLADO ---------------
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed) horizontal = -1;
            else if (Keyboard.current.dKey.isPressed) horizontal = 1;

            if (Keyboard.current.spaceKey.wasPressedThisFrame && jumpCount < maxJumps)
                Jump();
        }

        // 🟢--------------- CONTROLES MÓVILES (AHORA SE LEEN DESPUÉS) ---------------
        if (mi != null)
        {
            if (mi.leftPressed) horizontal = -1;
            else if (mi.rightPressed) horizontal = 1;

            if (mi.jumpPressed && jumpCount < maxJumps)
            {
                Jump();
                mi.jumpPressed = false; // 🔵 Muy importante
            }
        }

        // 🌀 Girar el personaje
        if (horizontal != 0)
        {
            float direction = Mathf.Sign(horizontal);
            transform.localScale = new Vector3(direction, 1f, 1f);

            if (firePoint != null)
            {
                firePoint.localScale = new Vector3(direction, 1f, 1f);
                firePoint.localRotation = Quaternion.Euler(0, direction == 1 ? 0 : 180, 0);
            }
        }

        // 🔄 Actualizar animación
        anim.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0f) return;

        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                jumpCount = 0;
                anim.SetBool("isJumping", false);
                break;
            }
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpCount++;
        anim.SetBool("isJumping", true);
    }
}

