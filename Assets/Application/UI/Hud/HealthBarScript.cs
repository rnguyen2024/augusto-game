using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script to control the healthbar. 

public class HealthBarScript : MonoBehaviour
{
    //Connects to healthbarfill. 
    public Slider remainingHealth;

    public void setHealth(int health)
    {
        remainingHealth.value = health;
    }

    public void setMaxHealth(int health)
    {
        remainingHealth.maxValue = health;
        remainingHealth.value = health;
        remainingHealth.minValue = 0;
    }
}
