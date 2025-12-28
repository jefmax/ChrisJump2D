using UnityEngine;

public class Coin : MonoBehaviour
{
    public int points = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Car"))
        {
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddPoint(points);
            }
            else
            {
                Debug.LogError("ScoreManager no existe en esta escena");
            }

            Destroy(gameObject);
        }
    }
}


