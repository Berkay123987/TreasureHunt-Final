using System.Collections;
using UnityEngine;

public class EnemyPatrolBalik : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public GameObject player; // Oyuncunun GameObject'i
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    private bool isWaiting = false;
    public float attackInterval = 4f; // Saldırı aralığı
    public int damageAmount = 1; // Saldırı hasarı miktarı
    public float playerDetectionRange = 5f; // Oyuncu algılama mesafesi

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = ChooseTargetPoint();
        anim.SetBool("isRunning", true);
        //FlipTowardsTarget(); // Başlangıçta doğru yüze dön
        FlipTowardsPlayer();
    }

    void Update()
    {
        if (!isWaiting)
        {
            if (IsPlayerClose() && IsPlayerWithinBoundaries())
            {
                MoveTowardsPlayer();
            }
            else
            {
                Transform targetPoint = ChooseTargetPoint();
                if (currentPoint != targetPoint)
                {
                    currentPoint = targetPoint;
                    FlipTowardsTarget();
                }

                MoveTowardsCurrentPoint();
            }
        }
    }

    private bool IsPlayerClose()
    {
        return Vector2.Distance(transform.position, player.transform.position) < playerDetectionRange;
    }

    private bool IsPlayerWithinBoundaries()
    {
        float playerX = player.transform.position.x;
        return playerX >= pointA.transform.position.x && playerX <= pointB.transform.position.x;
    }

    private void MoveTowardsPlayer()
    {
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(speed * Mathf.Sign(directionToPlayer.x), rb.velocity.y);
        FlipTowardsPlayer();
    }

    private Transform ChooseTargetPoint()
    {
        float distanceToA = Vector2.Distance(player.transform.position, pointA.transform.position);
        float distanceToB = Vector2.Distance(player.transform.position, pointB.transform.position);

        return distanceToA < distanceToB ? pointA.transform : pointB.transform;
    }

    private void MoveTowardsCurrentPoint()
    {
        Vector2 point = currentPoint.position - transform.position;
        rb.velocity = new Vector2(speed * Mathf.Sign(point.x), rb.velocity.y);

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            StartCoroutine(WaitAndFlip(2.0f));
        }
    }

    private IEnumerator WaitAndFlip(float waitTime)
    {
        isWaiting = true;
        rb.velocity = Vector2.zero;
        anim.SetBool("isRunning", false);

        yield return new WaitForSeconds(waitTime);

        currentPoint = ChooseTargetPoint();
        //FlipTowardsTarget();
        FlipTowardsPlayer();
        anim.SetBool("isRunning", true);
        isWaiting = false;
    }

    private void FlipTowardsTarget()
    {
        if (currentPoint == pointA.transform && transform.localScale.x != 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (currentPoint == pointB.transform && transform.localScale.x != -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void FlipTowardsPlayer()
    {
        if (player.transform.position.x < transform.position.x && transform.localScale.x != -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.transform.position.x > transform.position.x && transform.localScale.x != 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("isRunning", false);
            Attack();
        }
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D detectedPlayer in players)
        {
            if (detectedPlayer.CompareTag("Player"))
            {
                detectedPlayer.GetComponent<GameController>().TakeDamage(damageAmount);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}








