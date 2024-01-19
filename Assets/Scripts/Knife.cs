using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private Animator animator;
    public float destroyTime = 0.3f;
    public SpriteRenderer spriteRenderer;
    public float stuckOffset1 = 0.1f; //Sol için
    public float stuckOffset2 = 0.2f; //Sağ için
    //private Rigidbody2D rb;
    //private bool isStuck = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody2D>();
        //rb.isKinematic = true; // Başlangıçta kinematik mod aktif
        if (spriteRenderer.flipX)
        {
            animator.SetTrigger("TersDonme");
        }
        else
        {
            animator.SetTrigger("Donme");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            // Top bir engelle çarparsa ve daha önce çarpışmadıysa
            // Topu yok et veya başka bir kırılma animasyonu oynat
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            Collider2D col = GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = false;
            }


            if (spriteRenderer.flipX)
            {
                FlipSprite();
            }

            animator.SetTrigger("Stuck");
            // Bunu ekle: Duvara saplanma ofseti ile bıçağı ileriye hareket ettir
            transform.position = new Vector3(transform.position.x + (spriteRenderer.flipX ? -stuckOffset2 : stuckOffset2), transform.position.y, transform.position.z);

            Invoke("YokEt", destroyTime);
            //Break();
        }
    }

    void FlipSprite()
    {
        animator.SetTrigger("Stuck");
        // Sprite'ı çevir
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(transform.position.x + (spriteRenderer.flipX ? -stuckOffset1 : stuckOffset1), transform.position.y, transform.position.z);
    }

    void YokEt()
    {
        Destroy(gameObject);
    }








    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject); // Düşmana çarptığında yok et
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            StickToSurface(collision); // Duvara veya zemine çarptığında işlemleri yap
        }
    }

    void StickToSurface(Collision2D collision)
    {
        isStuck = true;
        animator.SetTrigger("Stuck"); // Saplanma animasyonunu başlat

        rb.velocity = Vector2.zero; // Hızı sıfırla
        rb.angularVelocity = 0; // Açısal hızı sıfırla
        rb.isKinematic = true; // Kinematik modu aktif yap

        AdjustRotation(collision); // Bıçağın dönüşünü ayarla

        Invoke("DestroyKnife", 2f); // 2 saniye sonra yok et
    }

    void AdjustRotation(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        float angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Bıçağın yüzeye göre dönüşünü ayarla
    }

    void DestroyKnife()
    {
        if (isStuck)
        {
            Destroy(gameObject); // Eğer saplanmışsa, yok et
        }
    }*/
}











