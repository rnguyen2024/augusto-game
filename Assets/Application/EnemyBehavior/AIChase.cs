using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public float speed, chaseDistance, randomMovementInterval, randomMovementDistance;

    private float distance, randomMovementTimer;
    private Transform playerTransform;
    private Rigidbody2D rb2d;
    private Vector2 currentDirection;
    private bool randomMove = false;

    // Start is called before the first frame update
    void Start()
    {
        //Because player and enemy are no longer in the same scene heirarchy, enemy now references the player by
        //the "player" tag instead of it being assigned in the inspector 
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true; //Prevents rotations when colliding

        randomMovementTimer = randomMovementInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if(!randomMove)//Checks if the RandomCautiousMovement subroutine in works
        {
            //Finds distance between two transforms in this case object with script and player
            distance = Vector2.Distance(transform.position, playerTransform.position);

            //Checks if distance is close enough for enemy to chase, if it is then it updates the position to the players position
            if(distance < chaseDistance)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                currentDirection = direction; //Save direction to use if collision occurs
                rb2d.velocity = direction * speed;

                //Checks for horizontal movement and flips accordingly
                if(direction.x != 0)
                {
                    FlipEnemy(direction.x);
                }
            }
            else
            {
                rb2d.velocity = Vector2.zero; //Stops movement if not chasing
            }
        }

        randomMovementTimer -= Time.deltaTime;
        if (randomMovementTimer <= 0 && distance < chaseDistance && !randomMove)
        {
            StartCoroutine(RandomCautiousMovement());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Checks if collision is with an object
        if (collision.collider.CompareTag("Objects"))
        {
            //Stop movements
            rb2d.velocity = Vector2.zero;
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

    IEnumerator RandomCautiousMovement()
    {
        randomMove = true;

        //Debug.Log("Random movement triggered!");
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb2d.velocity = randomDirection * randomMovementDistance;

        yield return new WaitForSeconds(1f); //Duration of random movements
        
        //Debug.Log("Random movement ended!");
        rb2d.velocity = currentDirection * speed; //Goes back to chasing player
        randomMove = false;
        randomMovementTimer = randomMovementInterval;
    }

}

    