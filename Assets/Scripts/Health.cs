using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    public GameObject gameOverPanel; // Oyun bitiş paneli
    public GameObject heartPanel; // Kalp paneli
    public string enemyTag = "Enemy"; // Düşman nesnelerinin etiketi

    // Oyun başladığında çağrılır
    private void Start()
    {
        gameOverPanel.SetActive(false); // Oyun bitiş panelini gizle
        heartPanel.SetActive(true); // Kalp panelini göster
    }

    // Çarpışma algılandığında çağrılır
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Eğer çarpışan nesne düşman etiketine sahipse
        if (collision.gameObject.CompareTag(enemyTag))
        {
            Debug.Log("Çarpışma algılandı: " + collision.gameObject.name);
            HeartLost(); // Bir kalp kaybet
        }
    }

    // Kalp kaybetme işlemi için ko-rutin
    IEnumerator CloseHeart(int x)
    {
        yield return new WaitForSeconds(1); // Animasyon gibi bir bekleme süresi

        transform.GetChild(x).gameObject.SetActive(false); // Kalbi gizle

        bool anyHeartLeft = CheckForRemainingHearts(); // Kalp kaldı mı diye kontrol et

        if (!anyHeartLeft) // Eğer hiç kalp kalmadıysa
        {
            gameOverPanel.SetActive(true); // Oyun bitiş panelini göster
            heartPanel.SetActive(false); // Kalp panelini gizle
        }
    }

    // Kalan kalpleri kontrol et
    bool CheckForRemainingHearts()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                return true; // Aktif kalp bulunursa true dön
            }
        }
        return false; // Aktif kalp yoksa false dön
    }

    // Kalp kaybetme fonksiyonu
    void HeartLost()
    {
        for (int i = transform.childCount - 1; i >= 0; i--) // Kalpleri tersten kontrol et
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy) // Eğer kalp aktifse
            {
                StartCoroutine(CloseHeart(i)); // Kalp kaybetme ko-rutinini başlat
                break;
            }
        }
    }
}





