using UnityEngine;

public class Ball : MonoBehaviour
{
    private Animator animator;
    public float destroyTime = 0.3f; // Unity Editor'da süreyi ayarlamak için public bir değişken

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            // Top bir engelle çarparsa
            Break();
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            // Top bir engelle çarparsa ve daha önce çarpışmadıysa
            // Topu yok et veya başka bir kırılma animasyonu oynat
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.freezeRotation = true;
                rb.isKinematic = true;
            }
            animator.SetTrigger("Patla");

            Collider2D col = GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = false;
            }

            Invoke("YokEt", destroyTime);
            //Break();
        }
    }

    void YokEt()
    {
        Destroy(gameObject);
    }

    /*void Break()
    {
        Debug.Log("Break fonksiyonu çağrıldı!");
        // Topu yok et veya başka bir kırılma animasyonu oynat
        animator.SetTrigger("Patla");
        Destroy(gameObject);
    }*/

}


