using UnityEngine;
using System.Collections;

public class Heykel : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform firePoint;
    public float fireSpeed = 5f;
    public float nextFireTime = 0f;
    public float fireRate = 0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate; // Fire once every second
        }
    }

    void Fire()
    {
        StartCoroutine(WaitAndTrigger());

        animator.SetTrigger("Fire");
    }

    IEnumerator WaitAndTrigger()
    {
        // Örnek olarak, 0.5 saniye bekletiyoruz. İstediğiniz süreyi ayarlayabilirsiniz.
        yield return new WaitForSeconds(0.5f);

        GameObject newBall = Instantiate(ballPrefab, firePoint.position, Quaternion.Euler(0, 0, 90));
        Rigidbody2D rb = newBall.GetComponent<Rigidbody2D>();
        rb.velocity = -transform.right * fireSpeed;
    }

}
