using UnityEngine;

public class TimeDevam : MonoBehaviour
{
    public static TimeDevam Instance;

    public float elapsedTime;
    public bool gameWon = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateElapsedTime(float deltaTime)
    {
        if (!gameWon)
        {
            elapsedTime += deltaTime;
        }
    }

    public void GameWon()
    {
        gameWon = true;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        gameWon = false;
    }
}



