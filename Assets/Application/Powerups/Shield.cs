using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private float destroyTime = 0.5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        //If player comes into contact with the "door" 
        //Calls loadRoom in room Manager to load the next scene
        if (other.CompareTag("Player"))
        {
            Debug.Log("Obtained Shield");
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.setShield();
            StartCoroutine(waitToDestroy(destroyTime));
        }
    }

    private IEnumerator waitToDestroy(float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
