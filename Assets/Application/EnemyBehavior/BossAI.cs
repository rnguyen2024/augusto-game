using System.Collections;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float speed;
    public float chaseDistance;
<<<<<<< HEAD
    public float stopDuration; // Time spent stopping to shoot
    public float shootInterval; // Time between consecutive shots during the stop phase
    public GameObject projectilePrefab; // Prefab for the boss's projectile
    public Transform shootPoint; // The point where projectiles spawn
=======
    public float stopDuration; //Time spent stopping to shoot
    public float shootInterval; //Time between consecutive shots during the stop phase
    public GameObject projectilePrefab; //Prefab for the boss's projectile
    public Transform shootPoint; //The point where projectiles spawn
>>>>>>> main
    private float distance;
    private Transform playerTransform;
    private Rigidbody2D rb2d;
    private Vector2 currentDirection;
    private bool isStopping = false;
<<<<<<< HEAD
    public float aoeRadius = 5f; // Radius of the AoE effect
    public float slowAmount = 0.5f; // How long the slow effect lasts
=======
    public float aoeRadius = 5f; //Radius of the AoE effect
    public float slowAmount = 0.5f; //How long the slow effect lasts
>>>>>>> main

    private float nextDamageTime;


    public Animator animator;
    
    
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;

        rb2d = GetComponent<Rigidbody2D>();
        //rb2d.freezeRotation = true;

<<<<<<< HEAD
        // Start the random stop-and-shoot coroutine
=======
        //Start the random stop-and-shoot coroutine
>>>>>>> main
        StartCoroutine(RandomStopAndShoot());
    }

    void Update()
    {
        if (!isStopping)
        {
            distance = Vector2.Distance(transform.position, playerTransform.position);

            if (distance < chaseDistance)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                currentDirection = direction;
<<<<<<< HEAD
                animator.SetFloat("BaseSpeed", Mathf.Abs(direction.x));
=======
                animator.SetFloat("BaseSpeed", Mathf.Abs(direction.y));
>>>>>>> main
                rb2d.velocity = direction * speed;

                //Checks for horizontal movement and flips accordingly
                if(direction.x != 0)
                {
<<<<<<< HEAD
=======
                    animator.SetFloat("BaseSpeed", Mathf.Abs(direction.x));
>>>>>>> main
                    FlipEnemy(direction.x);
                }
            }
            else
            {
                rb2d.velocity = Vector2.zero;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Objects"))
        {
            rb2d.velocity = Vector2.zero;
        }
        //Checks if collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
        }
    }

    public void StopAndShoot()
    {
        if (!isStopping)
        {
            StartCoroutine(StopAndShootRoutine());
        }
    }

    private IEnumerator StopAndShootRoutine()
    {
        // Set Rigidbody2D to Kinematic while shooting
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        isStopping = true;
        rb2d.velocity = Vector2.zero; // Stop movement

        float timer = 0f;
        while (timer < stopDuration)
        {
            ShootProjectile();
            yield return new WaitForSeconds(shootInterval);
            timer += shootInterval;
        }

        isStopping = false;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && shootPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Vector2 shootDirection = (playerTransform.position - shootPoint.position).normalized;

            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            if (projectileRb != null)
            {
                float projectileSpeed = 5f;
                projectileRb.velocity = shootDirection * projectileSpeed;
            }
        }
    }

    private IEnumerator RandomStopAndShoot()
    {
        while (true)
        {
            // Wait for random interval between the given seconds
            float waitTime = Random.Range(7f, 11f);
            yield return new WaitForSeconds(waitTime);

            StopAndShoot();
        }
    }

    void FlipEnemy(float directionX)
    {
        //Flip the character based on the horizontal movement direction
        if (directionX > 0) //Moving right
        {
            transform.localScale = new Vector3(-4, 4, 1);
        }
        else if (directionX < 0) //Moving left
        {
            transform.localScale = new Vector3(4, 4, 1);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Object entered AoE: {other.name}");
        // Check if the object entering the AoE is the player
        if (other.CompareTag("Player"))
        {
            PlayerControls playerControls = other.GetComponent<PlayerControls>();
            if (playerControls != null)
            {
                playerControls.ApplySlowEffect(slowAmount);
                Debug.Log("Player slowed!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"Object exited AoE: {other.name}");
        // Check if the object leaving the AoE is the player
        if (other.CompareTag("Player"))
        {
            PlayerControls playerControls = other.GetComponent<PlayerControls>();
            if (playerControls != null)
            {
                playerControls.RemoveSlowEffect();
                Debug.Log("Player speed restored!");
            }
        }
    }

    
}
