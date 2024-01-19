using System.Collections;
using UnityEngine;

public class EnemyPatrolBalık : MonoBehaviour
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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = ChooseTargetPoint();
        anim.SetBool("isRunning", true);
    }

    void Update()
    {
        if (!isWaiting)
        {
            MoveTowardsCurrentPoint();
            Attack(); // Saldırı fonksiyonu çağrısı
        }
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

        Flip();
        currentPoint = ChooseTargetPoint();
        anim.SetBool("isRunning", true);
        isWaiting = false;
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D player in players)
        {
            if (player.CompareTag("Player"))
            {
                player.GetComponent<GameController>().TakeDamage(damageAmount);
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




