using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public float attackRange = 1f;
    public LayerMask enemyLayer;
    public SpriteRenderer spriteRenderer;

    void Update()
    {
        // Call the Attack function when the attack button (e.g., "Fire1") is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Rastgele bir sayı üret (1, 2 veya 3)
        int randomTrigger = Random.Range(1, 4);

        // Rastgele sayıya göre animasyonu tetikle
        if (randomTrigger == 1)
        {
            animator.SetTrigger("AttackTrigger");
            StartCoroutine(DelayedHit(0f)); // Hemen hasar ver
        }
        else if (randomTrigger == 2)
        {
            animator.SetTrigger("AttackTrigger2");
            StartCoroutine(DelayedHit(0.2f)); // 0.5 saniye sonra hasar ver
        }
        else if (randomTrigger == 3)
        {
            animator.SetTrigger("AttackTrigger3");
            StartCoroutine(DelayedHit(0.2f)); // 0.5 saniye sonra hasar ver
        }
    }

    IEnumerator DelayedHit(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Burada hasar verme işlemini gerçekleştirin
        PerformHit();
    }

    void PerformHit()
    {
        RaycastHit2D[] hits;

        if (spriteRenderer.flipX)
        {
            hits = Physics2D.RaycastAll(transform.position, -transform.right, attackRange, enemyLayer);
        }
        else
        {
            hits = Physics2D.RaycastAll(transform.position, transform.right, attackRange, enemyLayer);
        }

        foreach (RaycastHit2D hit in hits)
        {
            // Vurulan nesnenin Enemy betiğine sahip olup olmadığını kontrol et
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            if (enemy != null)
            {
                // Düşmanın hasar fonksiyonunu çağır
                enemy.TakeDamage();
            }
        }
    }
}

