using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    public int health = 100;
    public float hitForce = 3f; // Fırlama kuvvetini ayarlayın
    private Rigidbody2D rb; // Rigidbody bileşenini ekleyin
    public SpriteRenderer spriteRenderer;
    private SpriteRenderer mySpriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); // Rigidbody bileşenini al
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        // Collider ekleyin (örneğin, BoxCollider)
    }

    public void TakeDamage()
    {
        //rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        //rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        animator.SetTrigger("Hit");

        Bounce();

        //rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        //rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        health -= 50;

        if (health <= 0)
        {
            Bounce();

            animator.SetTrigger("Dead");

            Invoke("Die", 2f);
            // Düşmanın canı sıfır veya daha azsa, ölüm fonksiyonunu çağır
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("Dead");
            // Tuzağa çarparsa 100 hasar alsın
            Invoke("Die", 2f);
        }
    }


    void Bounce()
    {
        rb.isKinematic = false;

        if (spriteRenderer.flipX)
        {
            mySpriteRenderer.flipX = true;
            Vector2 forceDirection = new Vector2(-1, 0.5f); // 45 derece
            forceDirection.Normalize(); // Vektörü normalize et
            rb.AddForce(forceDirection * hitForce, ForceMode2D.Impulse);
        }
        else
        {
            mySpriteRenderer.flipX = false;
            Vector2 forceDirection = new Vector2(1, 0.5f); // 45 derece
            forceDirection.Normalize(); // Vektörü normalize et
            rb.AddForce(forceDirection * hitForce, ForceMode2D.Impulse);
        }

        StartCoroutine(MakeEnemyKinematic());
    }

    void Die()
    {
        // Trigger the death animation or perform other death-related actions
        Destroy(gameObject);
    }

    public IEnumerator MakeEnemyKinematic()
    {
        yield return new WaitForSeconds(0.8f);
        rb.isKinematic = true;
    }

}

