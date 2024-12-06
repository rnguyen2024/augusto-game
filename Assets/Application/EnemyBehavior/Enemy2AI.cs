using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2AI : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator WaitAndExecute()
    {

        yield return new WaitForSeconds(1.0f); // Pause for 2 seconds
        gameObject.SetActive(false);
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        animator.SetBool("IsDead", true);
        StartCoroutine(WaitAndExecute());

        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<AIChaseNoAttack>().enabled = false;
        GetComponent<EnemyScript>().enabled = false;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Stop any movement
            rb.isKinematic = true;     // Disable physics interactions
        }

        this.enabled = false;
    }

}
