using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public int maxHealth = 3;
    private int currentHealth;

    public Image[] hearts;
    public GameObject gameOverPanel;
    public GameObject deadAnimation; // yeni Ekleme

    private bool isVulnerable = true;
    public float invulnerabilityDuration = 2f;

    public GameObject timer;
    public GameObject pauseImage;


    Vector2 checkpointPos;
    Rigidbody2D playerRb;

    AudioManager audioManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
        currentHealth = maxHealth;
        UpdateHearts();
        gameOverPanel.SetActive(false);

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    private void Start()
    {
        checkpointPos = transform.position;
        playerRb = GetComponent<Rigidbody2D>();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCurrentHealth(int health)
    {
        currentHealth = health;
        UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
        if (isVulnerable)
        {
            audioManager.PlaySFX(audioManager.Gameover);
            currentHealth -= damage;
            UpdateHearts();

            if (currentHealth <= 0)
            {
                GameOver();
            }
            else
            {
                StartCoroutine(Respawn(0.1f));
                StartCoroutine(Invulnerability());
            }
        }
    }

    IEnumerator Invulnerability()
    {
        isVulnerable = false;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isVulnerable = true;
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }

    IEnumerator Respawn(float duration)
    {

        Animator anim = deadAnimation.GetComponent<Animator>();
        anim.SetTrigger("Dead"); // Animasyonu başlat
        // Karakterin hareketlerini durdurma
        //playerRb.velocity = Vector2.zero;
        //playerRb.simulated = false;
        FindObjectOfType<PlayerMovement>().enabled = false;

        yield return new WaitForSeconds(1.1f); // YENİ EKLEME

        /*
        // Karakterin hareketlerini durdurma
        playerRb.velocity = Vector2.zero;
        playerRb.simulated = false;
        // Karakteri görsel olarak gizleme
        */
        GetComponent<SpriteRenderer>().enabled = false;

        // Bekleme süresi
        yield return new WaitForSeconds(duration);

        // Karakteri checkpoint'e taşıma ve hareketleri etkinleştirme
        FindObjectOfType<PlayerMovement>().enabled = true;
        transform.position = checkpointPos;
        GetComponent<SpriteRenderer>().enabled = true;
        //playerRb.simulated = true;

        // Canları yenileme
        //currentHealth = maxHealth;
        UpdateHearts();
    }

    /*void GameOver()
    {
        gameOverPanel.SetActive(true);
        // Oyunun hareketlerini durdurma (opsiyonel)
        Time.timeScale = 0;
    }*/

    void GameOver()
    {
        Animator anim = deadAnimation.GetComponent<Animator>();
        anim.SetTrigger("Dead"); // Animasyonu başlat
        // Karakterin hareketlerini durdurma
        FindObjectOfType<PlayerMovement>().enabled = false;
        // 1 saniyelik gecikme eklemek için Invoke kullanılıyor
        Invoke("ActivateGameOverPanel", 1f);
    }

    void ActivateGameOverPanel()
    {
        audioManager.StopMusic();
        audioManager.PlaySFX(audioManager.Dead);
        // Game Over panelini etkinleştir
        gameOverPanel.SetActive(true);

        if (timer != null) timer.SetActive(false);
        if (pauseImage != null) pauseImage.SetActive(false);

        // Oyunun hareketlerini durdurma (opsiyonel)
        Time.timeScale = 0;
    }


    //sağlık objesini için
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Health"))
        {
            if (GetCurrentHealth() < maxHealth)
            {
                audioManager.PlaySFX(audioManager.Can);
                SetCurrentHealth(GetCurrentHealth() + 1);
                Destroy(other.gameObject); // Sağlık objesini kaldır
                FindObjectOfType<PlayerMovement>().PlayHealthAnimation(); // Sağlık animasyonunu oynat
            }
        }
    }


    // Burada gerekirse diğer yönetim işlevlerini ekleyebilirsiniz.
}

