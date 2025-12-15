using UnityEngine;

public class Coin : MonoBehaviour
{
    public int points = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Car"))
        {
            ScoreManager.instance.AddPoint(points);
            Destroy(gameObject);
        }
    }
}

