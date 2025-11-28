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
        if (Time.time < nextFireTime)
            return;

        bool shootKeyboard = Keyboard.current != null && Keyboard.current.fKey.isPressed;
        bool shootMobile = MobileInput.instance != null && MobileInput.instance.shootPressed;

        // Disparo por teclado o móvil
        if (shootKeyboard || shootMobile)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;

            // Evita disparos infinitos en móvil por dejar pulsado
            if (shootMobile)
                MobileInput.instance.shootPressed = false;
        }
    }

    void Shoot() 
    {
        if (bulletPrefab == null || firePoint == null)
            return;

        // Instanciar bala
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Ignorar colisión con el jugador
        Collider2D bulletCollider = bulletGO.GetComponent<Collider2D>();
        if (bulletCollider != null && playerCollider != null)
        {
            Physics2D.IgnoreCollision(bulletCollider, playerCollider);
        }

        // Dirección según la escala del jugador
        float direction = transform.localScale.x > 0 ? 1f : -1f;
        bulletGO.GetComponent<Bullet>().SetDirection(new Vector2(direction, 0f));
    }
}



