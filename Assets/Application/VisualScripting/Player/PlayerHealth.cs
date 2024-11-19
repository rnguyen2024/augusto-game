using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Manages the player health and health bar. 
 */
public class PlayerHealth : MonoBehaviour
{

    //Minimum health is always set to 0. 
    public int maxHealth = 50;
    public int currentHealth;

    //References the healthbar class so that it can be interacted with here. 
    public HealthBarScript healthBar;

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
        healthBar.setHealth(currentHealth);
    }
}
