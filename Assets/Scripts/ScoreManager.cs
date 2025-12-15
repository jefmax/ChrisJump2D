using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;
    private int coinsForLife = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoint(int amount = 1)
    {
        score += amount;
        coinsForLife += amount;

        Debug.Log("Puntos: " + score);

        if (coinsForLife >= 3)
        {
            coinsForLife = 0;

            PlayerHealth ph = FindObjectOfType<PlayerHealth>();
            if (ph != null)
            {
                ph.AddLife(1);
            }
        }
    }
}
 
