using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2Flag : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isUsed = false;
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Lvl 2 flag set");
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.setLvl2();
            if (isUsed == false)
            {
                playerHealth.setPlayerHealth();
                isUsed = true;
            }
        }
    }
}
