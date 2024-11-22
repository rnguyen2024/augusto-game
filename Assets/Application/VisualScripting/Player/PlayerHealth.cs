using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Manages the player health and health bar. 
 */
public class PlayerHealth : MonoBehaviour
{
    public Animator animator;
    //Minimum health is always set to 0. 
    public int maxHealth = 50;
    public int currentHealth;
    private int currentScene;

    //References the healthbar class so that it can be interacted with here. 
    public HealthBarScript healthBar;

    //Tracks damage taken if enemy is in constant contact with player
    private Coroutine damageAccumulated;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    void Update()
    {

        //I used this function just to test the healthbar. It will be replaced once enemies are implemented. 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(5);
        }
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        healthBar.setHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Player Died!");
            //Death logic here!
            animator.SetBool("IsDead", true);
            currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);

        }
    }

     //Detects a collision with an enemy & calls takeDamage function
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Initial Damage
        if (collision.gameObject.CompareTag("Enemy"))
        {
            takeDamage(10); //Damage player takes
            Debug.Log("Player has collided with an enemy!");

            //Coroutine for continuous damage
            if(damageAccumulated == null)
            {
                damageAccumulated = StartCoroutine(ApplyDamageOverTime(1.5f, 5, collision.gameObject));
            }
        }
    }

    //Detects when collision with enemy ends
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player left contact enemy!");

            //Stops the continuous damage coroutine
            if (damageAccumulated != null)
            {
                StopCoroutine(damageAccumulated);
                damageAccumulated = null;
            }
        }
    }


    //Interface for coroutine, sums up damage over time (time for each increment, damage dealt, gameobject/enemy)
    private IEnumerator ApplyDamageOverTime(float interval, int damage, GameObject enemy)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            // Ensure the enemy is still active and valid
            if (enemy == null)
            {
                break;
            }

            takeDamage(damage);
            Debug.Log("Player is taking continuous damage from enemy!");
        }
    }
}
