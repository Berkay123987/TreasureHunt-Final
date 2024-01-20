using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yengec : MonoBehaviour
{
    public float attackInterval = 4f; // Attack interval (in seconds)
    public int damageAmount = 1; // Damage amount for the attack

    private float attackTime = 0f;

    void Update()
    {
        // Perform attack at regular intervals
        if (Time.time >= attackTime)
        {
            Attack();
            attackTime = Time.time + attackInterval;
        }
    }

    void Attack()
    {
        // Trigger the "Attack" animation
        GetComponent<Animator>().SetTrigger("Attack");

        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, 1.2f);

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
