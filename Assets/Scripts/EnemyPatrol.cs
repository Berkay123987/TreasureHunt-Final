using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    private bool isWaiting = false; // Bekleme durumu için kontrol değişkeni
    public float attackInterval = 4f; // Attack interval (in seconds)
    public int damageAmount = 1; // Damage amount for the attack

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isRunning", true);
    }

    void Update()
    {
        if (!isWaiting) // Eğer beklemiyorsa, hareket et
        {
            Attack();
            Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {
                StartCoroutine(WaitAndFlip(2.0f)); // Bekleme ve çevirme işlemini başlat
            }
        }
    }

    private IEnumerator WaitAndFlip(float waitTime)
    {
        isWaiting = true; // Bekleme durumuna geç
        rb.velocity = Vector2.zero; // Hareketi durdur
        anim.SetBool("isRunning", false); // Animasyonu durdur

        yield return new WaitForSeconds(waitTime); // Belirtilen süre kadar bekle

        // Flip ve hedef noktasını güncelle
        Flip();
        currentPoint = (currentPoint == pointA.transform) ? pointB.transform : pointA.transform;
        anim.SetBool("isRunning", true); // Animasyonu tekrar başlat
        isWaiting = false; // Bekleme durumunu kapat
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    void Attack()
    {
        // Trigger the "Attack" animation
        GetComponent<Animator>().SetTrigger("Attack");

        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        // Inflict damage to each player with the "Player" tag
        foreach (Collider2D player in players)
        {
            if (player.CompareTag("Player"))
            {
                // Inflict damage to the player
                player.GetComponent<GameController>().TakeDamage(damageAmount);
            }
        }
    }
}
