using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazineFinish : MonoBehaviour
{
    public GameObject winPanel; // Kazanma paneli için referans
    public GameObject pauseButton; // Gizlenecek UI objesi için referans
    public GameObject heartTimeArka; // Gizlenecek UI objesi için referans
    public GameObject heartTimePanel; // Gizlenecek UI objesi için referans
    public GameObject timerPanel; // Gizlenecek UI objesi için referans


    public Timer timer; // Timer script için referans

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.StopMusic();
            audioManager.PlaySFX(audioManager.Win);
            StartCoroutine(DelayedActions());
        }
    }

    IEnumerator DelayedActions()
    {
        // 5 saniye bekler
        yield return new WaitForSeconds(2);

        // Kazanma panelini göster
        winPanel.SetActive(true);

        // GameWon fonksiyonunu çağır
        timer.GameWon();

        // UI objesini gizle
        pauseButton.SetActive(false);
        heartTimeArka.SetActive(false);
        heartTimePanel.SetActive(false);
        timerPanel.SetActive(false);

        // Oyunu duraklat
        PauseGame();
    }

    private void PauseGame()
    {
        // Oyun duraklatma işlevselliğini buraya ekleyin
        Time.timeScale = 0; // Oyunun zaman akışını durdurur
    }
}


