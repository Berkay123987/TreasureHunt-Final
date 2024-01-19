using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI winTimeText;

    private void Update()
    {
        if (TimeDevam.Instance != null)
        {
            TimeDevam.Instance.UpdateElapsedTime(Time.deltaTime);
            UpdateTimerDisplay(TimeDevam.Instance.elapsedTime);
        }
    }

    private void UpdateTimerDisplay(float elapsedTime)
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameWon()
    {
        if (TimeDevam.Instance != null)
        {
            TimeDevam.Instance.GameWon();
            winTimeText.text = timerText.text; // Kazanma ekranında zamanı güncelleyin
            // Burada ayrıca kazanma ekranını etkinleştirebilirsiniz
        }
    }
}




/*
[SerializeField] TextMeshProUGUI timerText;
float elapsedTime;

void Update()
{
    elapsedTime += Time.deltaTime;
    int minutes = Mathf.FloorToInt(elapsedTime / 60);
    int seconds = Mathf.FloorToInt(elapsedTime % 60);
    timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
}*/

//Calısıyor!!!

/*
using UnityEngine;
using TMPro; // TextMeshPro kütüphanesini kullanmak için gerekli

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI winTimeText; // Kazanma ekranı için zaman metni
    float elapsedTime;
    bool gameWon = false;

    void Update()
    {
        if (!gameWon)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameWon()
    {
        gameWon = true;
        winTimeText.text = timerText.text; // Kazanma ekranında zamanı güncelleyin
        // Burada ayrıca kazanma ekranını etkinleştirebilirsiniz
    }
}*/