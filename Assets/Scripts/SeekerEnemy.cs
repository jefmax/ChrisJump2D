using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SeekerEnemy : MonoBehaviour
{
    public float detectionRadius = 6f;
    public float speed = 3f;
    public float attackRange = 1.2f;
    public float attackCooldown = 1.0f;
    public int damage = 1;

    Transform player;
    Rigidbody2D rb;
    float lastAttackTime = -999f;
    Vector2 movement;
    SpriteRenderer sr;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p) player = p.transform;
        rb.freezeRotation = true; // evitar rotaciones raras
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= detectionRadius)
        {
            // Mover hacia el jugador si no está demasiado cerca
            if (dist > attackRange)
            {
                Vector2 dir = (player.position - transform.position).normalized;
                movement = dir * speed;
                if (animator) animator.SetBool("isMoving", true);
            }
            else
            {
                movement = Vector2.zero;
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    Attack();
                }
                if (animator) animator.SetBool("isMoving", false);
            }

            // Flip sprite según dirección
            if (sr != null)
            {
                if (player.position.x < transform.position.x) sr.flipX = true;
                else sr.flipX = false;
            }
        }
        else
        {
            // fuera de rango de detección
            movement = Vector2.zero;
            if (animator) animator.SetBool("isMoving", false);
        }
    }

    void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            Vector2 newPos = rb.position + movement * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
        }
    }

    void Attack()
    {
        lastAttackTime = Time.time;
        if (animator) animator.SetTrigger("attack");

        // intentar dañar al jugador si está en rango
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (var c in hits)
        {
            if (c.CompareTag("Player"))
            {
                var ph = c.GetComponent<PlayerHealth>();
                if (ph != null) ph.TakeDamage(damage);
                else
                {
                    // alternativa: buscar componente con método público
                    Debug.LogWarning("Player no tiene PlayerHealth. Añade uno para recibir daño.");
                }
            }
        }
    }

    // Debug visual en editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

