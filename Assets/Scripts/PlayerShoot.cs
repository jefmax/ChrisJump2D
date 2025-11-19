using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 0.25f;
    private float nextFireTime = 0f;

    private Collider2D playerCollider;

    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.fKey.isPressed &&
            Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
            return;

        // Crear la bala en el firePoint
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // IGNORAR colisión con el jugador
        Collider2D bulletCollider = bulletGO.GetComponent<Collider2D>();
        if (bulletCollider != null && playerCollider != null)
        {
            Physics2D.IgnoreCollision(bulletCollider, playerCollider, true);
        }

        // 📌 Dirección del disparo (basado en hacia dónde mira el player)
        float direction = transform.localScale.x > 0 ? 1f : -1f;
        bulletGO.GetComponent<Bullet>().SetDirection(new Vector2(direction, 0f));
    }
}

