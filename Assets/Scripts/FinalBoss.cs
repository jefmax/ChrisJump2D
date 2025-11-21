using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class FinalBoss : MonoBehaviour
{
    [Header("Boss Health")]
    public int maxHealth = 5;
    public int currentHealth;
    public TextMeshProUGUI healthText; // contador en pantalla

    [Header("Movement & Attack")]
    public float detectionRadius = 6f;
    public float speed = 3f;
    public float attackRange = 1.2f;
    public float attackCooldown = 1.0f;
    public int damage = 1;
    public GameObject levelGoal;

    Transform player;
    Rigidbody2D rb;
    float lastAttackTime = -999f;
    Vector2 movement;
    SpriteRenderer sr;
    Animator animator;

    void Start()
    {
        // Inicializa vida
        currentHealth = maxHealth;
        UpdateHealthUI();

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p) player = p.transform;

        rb.freezeRotation = true;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= detectionRadius)
        {
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
                    Attack();

                if (animator) animator.SetBool("isMoving", false);
            }

            if (sr != null)
                sr.flipX = player.position.x < transform.position.x;
        }
        else
        {
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

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (var c in hits)
        {
            if (c.CompareTag("Player"))
            {
                var ph = c.GetComponent<PlayerHealth>();
                if (ph != null) ph.TakeDamage(damage);
            }
        }
    }

    // ---------------- VIDA --------------------

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateHealthUI();

        // Fase 2: cuando llega a 3 vidas
        if (currentHealth == 3)
        {
            speed += 1f;
        }

        // Fase 3: cuando llega a 1 vida
        if (currentHealth == 1)
        {
            speed += 1f;
        }

        // Parpadeo visual
        StartCoroutine(DamageFlash());

        if (currentHealth <= 0)
            Die();
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = currentHealth.ToString();
    }

    void Die()
    {
        // Activar el LevelGoal
        if (levelGoal != null)
        {
            levelGoal.SetActive(true);
        }

        Destroy(gameObject);
    }

    //-------------------------------------------
    IEnumerator DamageFlash()
    {
        if (sr == null) yield break;

        Color originalColor = sr.color;

        // Parpadeo (rojo -> normal)
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        sr.color = originalColor;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

