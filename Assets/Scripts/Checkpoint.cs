using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameController gameController;
    public Transform respawnPoint;
    //private SpriteRenderer spriteRenderer;
    //public Sprite passive, active;
    Collider2D cool;
    private Animator animator;

    // Başlangıçta çalışır
    void Awake()
    {
        // GameController ve SpriteRenderer component'lerini al
        gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        cool = GetComponent<Collider2D>();
        // Animator component'ini al
        animator = GetComponent<Animator>();
    }

    // Başka bir collider bu objenin collider'ı ile temas ettiğinde çalışır
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Çarpışan objenin tag'ı "Player" ise
        if (collision.CompareTag("Player"))
        {
            // GameController üzerinde checkpoint güncellemesi yap
            gameController.UpdateCheckpoint(respawnPoint.position);
            // Sprite'ı aktif olan ile değiştir
            //spriteRenderer.sprite = active;
            cool.enabled = false;
            // Animasyonu oynat
            animator.SetTrigger("checkpointReached");
        }
    }
}