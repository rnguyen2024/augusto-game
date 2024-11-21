using System.Collections;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float speed;
    public float chaseDistance;
    public float stopDuration; // Time spent stopping to shoot
    public float shootInterval; // Time between consecutive shots during the stop phase
    public GameObject projectilePrefab; // Prefab for the boss's projectile
    public Transform shootPoint; // The point where projectiles spawn
    private float distance;
    private Transform playerTransform;
    private Rigidbody2D rb2d;
    private Vector2 currentDirection;
    private bool isStopping = false;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;

        // Start the random stop-and-shoot coroutine
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
                rb2d.velocity = direction * speed;
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
}
