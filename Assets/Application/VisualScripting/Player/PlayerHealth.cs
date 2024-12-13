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
    public bool hitLevel2 = false;
    public bool hasShield = false;
    private GameObject shieldIcon;

    //References the healthbar class so that it can be interacted with here. 
    public HealthBarScript healthBar;

    //Tracks damage taken if enemy is in constant contact with player
    private Coroutine damageAccumulated;

     private float collisionCooldown = 0.8f; //Cooldown time between damage
     private bool canTakeDamage = true; 
     public bool isPoisoned = false;

    public SpriteRenderer sprite;
    public SpriteRenderer sprite2;

    //Audio source for playing damage sound
    private AudioSource audioSource;
    public AudioClip damageSound;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        shieldIcon = GameObject.Find("ShieldStatus");
        shieldIcon.SetActive(false);

        //Gets AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if ( hasShield == true && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(noDamage(8f));
            
        }

    }

    public void takeDamage(int damage)
    {
        if (canTakeDamage == true)
        {
            currentHealth -= damage;

            //Plays damage sound
            if (audioSource != null && damageSound != null)
            {
                audioSource.PlayOneShot(damageSound);
            }

            //Plays "Hurt" animation
            animator.SetTrigger("Hurt");
            if (isPoisoned)
            {
                StartCoroutine(FlashPurple());
            }
            else
            {
                StartCoroutine(FlashRed());
            }

            healthBar.setHealth(currentHealth);

            if (currentHealth <= 0)
            {
                Debug.Log("Player Died!");
                //Death logic here!
                animator.SetBool("IsDead", true);
                currentScene = SceneManager.GetActiveScene().buildIndex;
                Debug.Log("Current Scene " + currentScene);

                SceneManager.LoadScene(1);

                SceneManager.LoadScene(currentScene);
                if (hitLevel2 == false)
                {
                    SceneManager.LoadScene(3, LoadSceneMode.Additive);
                }

                if (hitLevel2 == true)
                {
                    SceneManager.LoadScene(6, LoadSceneMode.Additive);
                }
            }
        }
    }

//Detects a collision with an enemy & calls takeDamage function
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Initial Damage
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            takeDamage(5); //Damage player takes
            Debug.Log("Player has collided with an enemy!");

            //Coroutine for continuous damage
            if(damageAccumulated == null)
            {
                damageAccumulated = StartCoroutine(ApplyDamageOverTime(1.5f, 5, collision.gameObject));
            }

            if (canTakeDamage)
            {
                StartCoroutine(CollisionCooldown());
            }
        }
    }

    //Detects when collision with enemy ends
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
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

    private IEnumerator CollisionCooldown()
    {
        //Damage Cooldown
        canTakeDamage = false;
        yield return new WaitForSeconds(collisionCooldown);
        canTakeDamage = true;
    }

    public IEnumerator FlashRed(){
        sprite.color = Color.red;
        sprite2.color = Color.red;
        
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        sprite2.color = Color.white;
        
    }

    public IEnumerator FlashPurple(){
        sprite.color = new Color(0.6f, 0f, 0.8f); //Purple
        sprite2.color = new Color(0.6f, 0f, 0.8f); //Purple
        
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        sprite2.color = Color.white;
        
    }

    public void setLvl2()
    {
        hitLevel2 = true;
    }

    public void setPlayerHealth()
    {
        currentHealth = maxHealth;
        healthBar.setHealth(currentHealth);
    }

    public void setShield()
    {
        hasShield = true;
        shieldIcon.SetActive(true);
    }

    private IEnumerator noDamage(float shieldTime)
    {
        canTakeDamage = false;
        sprite.color = new Color(0.5f, 0.7f, 1f); // Light Blue
        sprite2.color = new Color(0.5f, 0.7f, 1f); // Light Blue
        yield return new WaitForSeconds(shieldTime);
        canTakeDamage = true;
        shieldIcon.SetActive(false);
        sprite.color = Color.white;
        sprite2.color = Color.white;
    }
}