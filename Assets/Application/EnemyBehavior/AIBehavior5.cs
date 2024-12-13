using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AIBehaavior5 : MonoBehaviour
{
    public float speed = 3f; 
    public float minimumDistance = 10f; //Min distance to maintain
    public float chaseDistance = 12f;
    public float shootInterval = 3f; // nterval between shots
    public GameObject projectilePrefab; //Projectile prefab
    public Transform shootPoint; //Point where projectiles spawn

    private Transform playerTransform;
    private Rigidbody2D rb2d;
    private bool aggro = false;

    public Animator animator;

    private bool isStopping = false;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;

        rb2d = GetComponent<Rigidbody2D>();

        StartCoroutine(ShootAtIntervals());
    }

    void Update()
    {
        if (!isStopping) //Only move if not in the middle of shooting
        {
            MaintainDistance();
        }

        animator.SetFloat("BaseSpeed", rb2d.velocity.magnitude);
    }

    private void MaintainDistance()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if(distance > chaseDistance + 3)
        {
            aggro = false;
        }

        if (distance <= chaseDistance || aggro)
        {
            aggro = true;
        if (distance < minimumDistance)
        {
            //Move away from the player
            Vector2 directionAwayFromPlayer = (transform.position - playerTransform.position).normalized;
            rb2d.velocity = directionAwayFromPlayer * speed;

            //Flip the enemy sprite
            FlipEnemy(directionAwayFromPlayer.x);
        }
        else if (distance > chaseDistance)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
                animator.SetFloat("BaseSpeed", Mathf.Abs(direction.y));
                rb2d.velocity = direction * speed;

                //Checks for horizontal movement and flips accordingly
                FlipEnemy(direction.x);
        }
        else
        {
            //Stop movement when the minimum distance is reached
            rb2d.velocity = Vector2.zero;
        }
        }
    }

    private IEnumerator ShootAtIntervals()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);

            float distance = Vector2.Distance(transform.position, playerTransform.position);
            if (!isStopping && distance <= chaseDistance) //Only shoot if not already shooting
            {
                StartCoroutine(ShootProjectile());
            }
        }
    }

    private IEnumerator ShootProjectile()
    {
        isStopping = true;
        rb2d.velocity = Vector2.zero; //Stop moving while shooting

        animator.SetTrigger("BowAttack2");
        yield return new WaitForSeconds(0.33f);

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Vector2 shootDirection = (playerTransform.position - shootPoint.position).normalized;

         FlipEnemy(shootDirection.x);

        //Set projectile velocity
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if (projectileRb != null)
        {
            projectileRb.velocity = shootDirection * 5f; // Adjust speed as needed
        }

        //Wait for the attack animation duration
        yield return new WaitForSeconds(0.6f);
        //animator.ResetTrigger("BowAttack");

        isStopping = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Objects"))
        {
            rb2d.velocity = Vector2.zero;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            //animator.SetTrigger("Attack");
        }
    }

    void FlipEnemy(float directionX)
    {
        // Flip the enemy sprite based on horizontal movement
        if (directionX > 0) // Moving right
        {
            transform.localScale = new Vector3(-3, 3, 1);
        }
        else if (directionX < 0) // Moving left
        {
            transform.localScale = new Vector3(3, 3, 1);
        }
    }
}
