using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class EnemyScript : MonoBehaviour
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

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        animator.SetTrigger("Hurt");
        StartCoroutine(FlashRed());
        if(currentHealth <= 0){
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
        
        yield return new WaitForSeconds(1.0f); // Pause for 1 seconds
        gameObject.SetActive(false);
    }

    void Die(){
        Debug.Log("Enemy died!");
        animator.SetBool("IsDead", true); 
        
        //Disable collider to prevent further interactions
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        //Stop any movement by resetting Rigidbody2D velocity
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; //Stop all movement
        }

        //Check if AIChase exists and disable it, otherwise check for BossAI
        AIChase aiChase = GetComponent<AIChase>();
        Enemy2AI AI2 = GetComponent<Enemy2AI>();
        Enemy3AI AI3 = GetComponent<Enemy3AI>();
        AIBehavior4 AI4 = GetComponent<AIBehavior4>();
        AIBehaavior5 AI5 = GetComponent<AIBehaavior5>();
        AIBehaavior6 AI6 = GetComponent<AIBehaavior6>();
        BossAI AIBOSS1 = GetComponent<BossAI>();
        Boss2AI AIBOSS2 = GetComponent<Boss2AI>();

        if (aiChase != null)
        {
            aiChase.enabled = false;
        }
        else if (AI2 != null)
        {
            AI2.enabled = false;
        }
        else if (AI3 != null)
        {
            AI3.enabled = false;
        }
        else if (AI4 != null)
        {
            AI4.enabled = false;
        }
        else if (AI5 != null)
        {
            AI5.enabled = false;
        }
        else if (AI6 != null)
        {
            AI6.enabled = false;
        }
        else if (AIBOSS1 != null)
        {
            AIBOSS1.enabled = false;
        }
        else if (AIBOSS2 != null)
        {
            AIBOSS2.enabled = false;
        }
        
        StartCoroutine(WaitAndExecute());
        GetComponent<EnemyScript>().enabled = false;
        this.enabled = false;
    }

    public IEnumerator FlashRed(){
        sprite.color = Color.red;
        sprite2.color = Color.red;
        
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        sprite2.color = Color.white;
        
    }
}

