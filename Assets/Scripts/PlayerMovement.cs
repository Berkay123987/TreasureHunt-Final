using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;
    private bool isTouchingWall;
    //public GameObject knifePrefab;
    public GameObject healthAnimationObject;
    //public GameObject knifeThrowPrefab;
    //public Transform firePoint1;
    //public Transform firePoint2;
    public float fireSpeed = 5f;
    //private bool canThrowKnife = true;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    private bool allowFlip = true;//AnimasyonFlip


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //healthAnimationObject.SetActive(false); // Başlangıçta objeyi gizle
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // Düşme Animasyonu Kontrolü
        if (!isGrounded && rb.velocity.y < 0)
        {
            animator.SetBool("isFalling", true);
        }

        /*if (Input.GetButtonDown("Fire1") && canThrowKnife)
        {
            //StartCoroutine(ThrowKnifeWithDelay());
        }*/
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Horizontal");

        if (!isTouchingWall || (isTouchingWall && isGrounded))
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            if (moveInput != 0)
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isIdle", false);
            }
            else
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isIdle", true);
            }

            if (moveInput > 0 && allowFlip)
                spriteRenderer.flipX = false;
            else if (moveInput < 0 && allowFlip)
                spriteRenderer.flipX = true;

            // Yerçekimi Kontrolü
            if (isGrounded)
            {
                animator.SetBool("isFalling", false);
            }
        }
    }

    public void SetAllowFlip(bool value)
    {
        allowFlip = value;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
        }
        else if (collision.gameObject.CompareTag("Enemy")) // Düşman etiketi
        {
            GameController.instance.TakeDamage(1);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        animator.SetTrigger("jump");
        isGrounded = false;
    }

    public void PlayHealthAnimation()
    {
        healthAnimationObject.SetActive(true); // Objeyi göster
        Animator anim = healthAnimationObject.GetComponent<Animator>();
        anim.SetTrigger("playHealthAnim"); // Animasyonu başlat

        StartCoroutine(HideHealthAnimationObject(anim.GetCurrentAnimatorStateInfo(0).length)); // Animasyon bitiminde gizle
    }

    IEnumerator HideHealthAnimationObject(float delay)
    {
        yield return new WaitForSeconds(delay);
        healthAnimationObject.SetActive(false);
    }
}




/*IEnumerator ThrowKnifeWithDelay()
    {
        // Prevent the player from throwing knives rapidly
        canThrowKnife = false;

        // Throw the knife
        ThrowKnife();

        // Wait for 0.5 seconds before allowing another knife throw
        yield return new WaitForSeconds(0.5f);

        // Allow the player to throw another knife
        canThrowKnife = true;
    }*/

/*void ThrowKnife()
{
        Transform selectedFirePoint;

        if (spriteRenderer.flipX)
        {
            selectedFirePoint = firePoint2;

        }
        else
        {
            selectedFirePoint = firePoint1;
        }

        GameObject newKnife = Instantiate(knifeThrowPrefab, selectedFirePoint.position, Quaternion.identity);
        Rigidbody2D rb = newKnife.GetComponent<Rigidbody2D>();
        rb.velocity = spriteRenderer.flipX ? -transform.right * fireSpeed : transform.right * fireSpeed;
        animator.SetTrigger("throw");


        /*GameObject newKnife = Instantiate(knifeThrowPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = newKnife.GetComponent<Rigidbody2D>();
        rb.velocity = spriteRenderer.flipX ? -transform.right * fireSpeed : transform.right * fireSpeed;
        Debug.Log("Knife Velocity: " + rb.velocity);*/

// Use the character's forward direction for knife's movement
/*GameObject knife = Instantiate(knifePrefab, transform.position, Quaternion.identity);
Rigidbody2D rbKnife = knife.GetComponent<Rigidbody2D>();
rbKnife.velocity = new Vector2(spriteRenderer.flipX ? -10f : 10f, 0);
rbKnife.gravityScale = 0; // Yer çekimi etkisini sıfırla
animator.SetTrigger("throw");
}*/





