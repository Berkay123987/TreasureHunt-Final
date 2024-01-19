using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControls : MonoBehaviour
{
    public GameObject settingsPanel; // Ayarlar paneli için bir referans
    public GameObject pauseGraphic;  // Pause görseli için bir referans

    public void OnSettingsClicked()
    {
        settingsPanel.SetActive(true); // Ayarlar panelini aktif hale getir
        pauseGraphic.SetActive(false); // Pause görselini gizle
        Time.timeScale = 0; // Oyunu durdur
    }

    public void OnContinueClicked()
    {
        settingsPanel.SetActive(false); // Ayarlar panelini gizle
        pauseGraphic.SetActive(true); // Pause görselini göster
        Time.timeScale = 1; // Oyunu devam ettir
    }
}



