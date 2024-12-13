using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class level2Flag : MonoBehaviour
{
    // Start is called before the first frame update
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
            playerHealth.setPlayerHealth();
        }
    }
}
