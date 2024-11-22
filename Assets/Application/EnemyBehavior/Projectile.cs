using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f; //Speed of the projectile
    public float lifetime = 5f; //Time before the projectile disappears
    private Vector2 moveDirection;

    void Start()
    {
        //Find the player in the scene and makes sure transform isn't accessed if no player
        Transform playerTransform = GameObject.FindWithTag("Player").transform;
        
        if(playerTransform != null)
        {
            moveDirection = (playerTransform.position - transform.position).normalized;
            //Rotates the fireball to face the player
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg; //Gets the angle
            transform.rotation = Quaternion.Euler(0, 0, angle); //Rotates
        }
        else
        {
            moveDirection = Vector2.up;
        }
        //Destroy the projectile after a certain time
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        //Shoots the projectile towards the player
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
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
