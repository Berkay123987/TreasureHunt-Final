using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartCode : MonoBehaviour
{
    /*public void StartGameOrRestart()
    {
        if (TimeDevam.Instance != null)
        {
            TimeDevam.Instance.ResetTimer();
        }
        // Oyun başlangıç veya restart kodları...
    }*/

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void LoadSceneIndex2()
    {
        if (TimeDevam.Instance != null)
        {
            TimeDevam.Instance.ResetTimer();
        }

        audioManager.PlayMusic();
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }
}

