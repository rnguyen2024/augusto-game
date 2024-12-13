using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRefill : MonoBehaviour
{
    private float destroyTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //If player comes into contact with the "door" 
        //Calls loadRoom in room Manager to load the next scene
        if (other.CompareTag("Player"))
        {
            Debug.Log("Refilled health");
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.setPlayerHealth();
            StartCoroutine(waitToDestroy(destroyTime));
        }
    }

    private IEnumerator waitToDestroy(float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
