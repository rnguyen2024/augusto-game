    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior4 : MonoBehaviour
{
    public float speed;
    public float chaseDistance;
    public float chargeSpeed;
    public float chargeDistance;
    private float distance;

    private Transform playerTransform;
    private Rigidbody2D rb2d;
    private Vector2 currentDirection;
    private bool isCharging = false;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true; //Prevents rotations when colliding
        
    }

    // Update is called once per frame
    void Update()
    {
        //Finds distance between two transforms in this case object with script and player
        distance = Vector2.Distance(transform.position, playerTransform.position);

        if (!isCharging)
        {
            if (distance < chargeDistance)
            {
                StartCoroutine(ChargeAtPlayer());
            }
            //Checks if distance is close enough for enemy to chase, if it is then it updates the position to the players position
            else if(distance < chaseDistance)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Checks if collision is with an object
        if (collision.collider.CompareTag("Objects"))
        {
            //Stop movements
            //rb2d.velocity = Vector2.zero;
        }
        //Checks if collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            //animator.SetTrigger("Attack");
        }
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

    private IEnumerator ChargeAtPlayer()
    {
        isCharging = true;
        float chargeDuration = 2f;
        float elapsedTime = 0f;
        //rb2d.mass = 51f;
        
        //Calculate direction to player
        Vector2 chargeDirection = (playerTransform.position - transform.position).normalized;

        //Flip enemy based on charge direction
        if (chargeDirection.x != 0) 
        {
            FlipEnemy(chargeDirection.x);
        }

        while (elapsedTime < chargeDuration)
        {
            animator.SetFloat("BaseSpeed", Mathf.Abs(chargeDirection.y));
            animator.SetFloat("BaseSpeed", Mathf.Abs(chargeDirection.x));
            rb2d.velocity = chargeDirection * chargeSpeed;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb2d.velocity = Vector2.zero; //Stop after charging
        animator.SetFloat("BaseSpeed", 0);
        rb2d.mass = 5000f;

        yield return new WaitForSeconds(1f); //Cooldown after charging
        animator.SetFloat("BaseSpeed", Mathf.Abs(chargeDirection.y));
        animator.SetFloat("BaseSpeed", Mathf.Abs(chargeDirection.x));
        isCharging = false;
        rb2d.mass = 51f;;
    }
}