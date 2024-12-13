using System.Collections;
using UnityEngine;

public class Boss2AI : MonoBehaviour
{
    public float speed;
    public float chaseDistance;
    public float stopDuration; //Time spent stopping to shoot
    public float shootInterval; //Time between consecutive shots during the stop phase
    public GameObject projectilePrefab; //Prefab for the boss's projectile
    public Transform shootPoint; //The point where projectiles spawn
    public GameObject player;
    private float distance;
    private Transform playerTransform;
    private Rigidbody2D rb2d;
    private Vector2 currentDirection;
    private bool isStopping = false;
    public float fireballSpreadAngle = 30f;
    public int fireballCount = 3;
    public float knockbackForce = 5f;

    private float nextDamageTime;


    public Animator animator;
    
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;

        rb2d = GetComponent<Rigidbody2D>();
        //rb2d.freezeRotation = true;

        //Start the random stop-and-shoot coroutine
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
                animator.SetFloat("BaseSpeed", Mathf.Abs(direction.y));
                rb2d.velocity = direction * speed;

                //Checks for horizontal movement and flips accordingly
                if(direction.x != 0)
                {
                    animator.SetFloat("BaseSpeed", Mathf.Abs(direction.x));
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
            PlayerControls player = collision.gameObject.GetComponent<PlayerControls>();
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            
            if (player != null)
            {
                PlayerControls movement = player.GetComponent<PlayerControls>();
                player.TakeKnockback(knockbackDirection, knockbackForce, "Boss");
                StartCoroutine(DisableMovementDuringKnockback(movement, 0.5f));
            }
            StartCoroutine(StopAndAttack());
        }
    }

    public void StopAndShoot()
    {
        if (!isStopping)
        {
            StartCoroutine(StopAndShootRoutine());
        }
    }

    private IEnumerator StopAndAttack(){
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        isStopping = true;
        rb2d.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        isStopping = false;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
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
            ShootWaveProjectile();
            yield return new WaitForSeconds(shootInterval);
            timer += shootInterval;
        }

        isStopping = false;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    private void ShootWaveProjectile()
    {
        float startAngle = -fireballSpreadAngle / 2; 
        float angleIncrement = fireballSpreadAngle / (fireballCount - 1);
        Vector2 directionToPlayer = (playerTransform.position - shootPoint.position).normalized;

        if (projectilePrefab != null && shootPoint != null)
        {
            for (int i = 0; i < fireballCount; i++){
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            

            float currentAngle = startAngle + angleIncrement * i;
            Vector2 shootDirection = Quaternion.Euler(0, 0, currentAngle) * directionToPlayer;

            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            if (projectileRb != null)
            {
                float projectileSpeed = 7f;
                projectileRb.velocity = shootDirection * projectileSpeed;
            }
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

    private IEnumerator DisableMovementDuringKnockback(PlayerControls movement, float duration){
    if (movement != null)
    {
        movement.enabled = false;  // Disable movement script
        yield return new WaitForSeconds(duration); // Wait for a moment (duration of knockback)
        movement.enabled = true;   // Re-enable movement script
    }

   
}
}
