using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeneme : MonoBehaviour
{
    private Rigidbody2D rb;
    private float Move;

    public float speed;
    public float jump;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(Move * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            anim.SetBool("isJumping", !isGrounded());
            rb.AddForce(new Vector2(rb.velocity.x, jump * 10));
        }

        if (Move != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

}






/*public float moveSpeed = 5f;
public float jumpForce = 5f;
private Rigidbody2D rb;
private Animator animator;
private SpriteRenderer spriteRenderer;
public Vector2 boxSize;
public float castDistance;
public LayerMask groundLayer;

void Start()
{
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
}

void Update()
{
    if (Input.GetButtonDown("Jump") && IsGroundedCheck())
    {
        Jump();
    }

    if (!IsGroundedCheck() && rb.velocity.y < 0)
    {
        animator.SetBool("isFalling", true);
    }
    else
    {
        animator.SetBool("isFalling", false);
    }
}

void FixedUpdate()
{
    float moveInput = Input.GetAxis("Horizontal");
    rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

    if (moveInput != 0)
    {
        animator.SetBool("isRunning", true);
        spriteRenderer.flipX = moveInput < 0;
    }
    else
    {
        animator.SetBool("isRunning", false);
    }

    animator.SetBool("isJumping", !IsGroundedCheck());
}

void Jump()
{
    rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
}

public bool IsGroundedCheck()
{
    return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
}

private void OnDrawGizmos()
{
    Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
}
}*/