using UnityEngine;

public class EnemyAir : MonoBehaviour
{
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AirplaneHealth airplaneHealth = collision.gameObject.GetComponent<AirplaneHealth>();

            if (airplaneHealth != null)
            {
                airplaneHealth.TakeDamage(damage);
            }

            Destroy(gameObject); // el enemigo muere al chocar
        }
    }
}


