using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AIBehaavior6 : MonoBehaviour
{
    public float speed;
    public float chaseDistance;
    private float distance;

    private Transform playerTransform;
    private Rigidbody2D rb2d;
    private Vector2 currentDirection;

    private bool isAttacking = false;

    public float retreatDistance = 10f; //Distance ran after attack
    public float retreatSpeed = 10;

    //Damage over time settings
    public int dotDamage = 4; //Damage per tick
    public float dotDuration = 3f; //Duration of DoT
    public float dotInterval = 1f; //Time between ticks

    private Coroutine damageCoroutine; //Ensures only one DoT instance is running

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;

        rb2d = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking) return;

        //Finds distance between two transforms in this case object with script and player
        distance = Vector2.Distance(transform.position, playerTransform.position);

            //Checks if distance is close enough for enemy to chase, if it is then it updates the position to the players position
            if(distance < chaseDistance)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                currentDirection = direction; //Save direction to use if collision occurs
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
                rb2d.velocity = Vector2.zero; //Stops movement if not chasing
                animator.SetFloat("BaseSpeed", 0);
            }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isAttacking)
        {
            StartCoroutine(AttackRoutine(collision.gameObject));
        }
        
    }

    private IEnumerator AttackRoutine(GameObject player)
    {
        isAttacking = true;
        rb2d.velocity = Vector2.zero;

        animator.SetTrigger("AttackNormal"); //Trigger attack animation

        //Wait for the attack animation to finish
        yield return new WaitForSeconds(0.8f);

        //Apply DoT to the player
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            StartCoroutine(ApplyDamageOverTime(playerHealth));
        }


        Vector2 directionAwayFromPlayer = (transform.position - playerTransform.position).normalized;
        float targetDistance = retreatDistance;

        //Run away logic
        while (targetDistance > 0)
        {
            float moveDistance = retreatSpeed * Time.deltaTime;
            rb2d.velocity = directionAwayFromPlayer * retreatSpeed;

            //Reduce the target distance by the amount moved
            targetDistance -= moveDistance;

            animator.SetFloat("BaseSpeed", Mathf.Abs(rb2d.velocity.magnitude));
            FlipEnemy(directionAwayFromPlayer.x);

            yield return null;
        }

        rb2d.velocity = Vector2.zero;
        animator.SetFloat("BaseSpeed", 0);

        isAttacking = false;
    }

     void FlipEnemy(float directionX)
    {
        //Flip the character based on the horizontal movement direction
        if (directionX > 0) //Moving right
        {
            transform.localScale = new Vector3(-3, 3, 1);
        }
        else if (directionX < 0) //Moving left
        {
            transform.localScale = new Vector3(3, 3, 1);
        }
    }

    private IEnumerator ApplyDamageOverTime(PlayerHealth playerHealth)
    {
        float elapsedTime = 0f;
        playerHealth.isPoisoned = true;

        while (elapsedTime < dotDuration)
        {
            playerHealth.takeDamage(dotDamage);
            yield return new WaitForSeconds(dotInterval);
            elapsedTime += dotInterval;
        }
        playerHealth.isPoisoned = false;

    }
}