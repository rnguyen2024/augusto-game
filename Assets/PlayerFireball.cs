using System.Collections;
using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    private Vector2 moveDirection;
    private float speed;
    private int impactDamage;
    private int damageOverTime;
    private float dotDuration;

    public void Initialize(Vector2 direction, float fireballSpeed, int impactDmg, int dotDmg, float dotTime, float lifetime)
    {
        moveDirection = direction.normalized;
        speed = fireballSpeed;
        impactDamage = impactDmg;
        damageOverTime = dotDmg;
        dotDuration = dotTime;

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Fireball collided with: " + collision.gameObject.name); //Debug line to check collision

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Fireball hit an enemy!");

            EnemyScript enemy = collision.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                //Apply impact damage immediately
                enemy.TakeDamage(impactDamage);
                
                //Start the DoT effect on the enemy
                enemy.ApplyDamageOverTime(damageOverTime, dotDuration);
            }

            Destroy(gameObject);
        }

        if (collision.CompareTag("Enemy2"))
        {
            Debug.Log("Fireball hit an enemy!");

            Enemy2AI enemy2 = collision.GetComponent<Enemy2AI>();
            if (enemy2 != null)
            {
                //Apply impact damage immediately
                enemy2.TakeDamage(impactDamage);

                //Start the DoT effect on the enemy
                enemy2.ApplyDamageOverTime(damageOverTime, dotDuration);
            }

            Destroy(gameObject);
        }

        if (collision.CompareTag("Objects"))
        {
            Destroy(gameObject);
        }
    }
}