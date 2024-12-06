using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Enemy2AI : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer sprite;
    public SpriteRenderer sprite2;

    public int maxHealth = 100;
    int currentHealth;
    private bool isUnderDoT = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");
        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ApplyDamageOverTime(int damage, float duration)
    {
        // Prevent multiple overlapping DoT effects
        if (!isUnderDoT)
        {
            StartCoroutine(DamageOverTimeCoroutine(damage, duration));
        }
    }

    private IEnumerator DamageOverTimeCoroutine(int damage, float duration)
    {
        isUnderDoT = true;
        yield return new WaitForSeconds(1f);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
        }

        isUnderDoT = false;
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

    public IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        sprite2.color = Color.red;

        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        sprite2.color = Color.white;

    }

}
