using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f; //Speed of the projectile
    public float lifetime = 5f; //Time before the projectile disappears
    private Transform playerTransform;

    void Start()
    {
        // Find the player in the scene
        playerTransform = GameObject.FindWithTag("Player").transform;
        
        // Destroy the projectile after a certain time to avoid cluttering the scene
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the projectile towards the player
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy the projectile on collision with player or Objects
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Objects"))
        {
            Destroy(gameObject);
        }
    }
}
