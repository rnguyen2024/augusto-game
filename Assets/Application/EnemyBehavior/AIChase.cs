using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public float speed;
    public float wanderSpeed = .2f;
    public float chaseDistance;
    private float distance;

    private Transform playerTransform;
    private Rigidbody2D rb2d;
    private Vector2 currentDirection;
    private bool isWandering = false;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true; //Prevents rotations when colliding

        StartCoroutine(RandomWandering());
        
    }

    // Update is called once per frame
    void Update()
    {
        //Finds distance between two transforms in this case object with script and player
        distance = Vector2.Distance(transform.position, playerTransform.position);

        if(!isWandering){
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
            animator.SetTrigger("Attack");
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

    private IEnumerator RandomWandering()
    {
        float wanderRange = 5f;
        yield return new WaitForSeconds(Random.Range(0f, 2f)); //Random delay to desync enemies

        while(true)
        {
            yield return new WaitForSeconds(Random.Range(4f, 6f));

            distance = Vector2.Distance(transform.position, playerTransform.position);
            if (distance < wanderRange)
            {
            isWandering = true; 
            Vector2 directionAwayFromPlayer = (transform.position - playerTransform.position).normalized;
            animator.speed = 0.45f;
    
            Vector2 backingOffDirection =  new Vector2(directionAwayFromPlayer.x * 0.7f, directionAwayFromPlayer.y).normalized;
            float wanderDuration = 0.7f;
            float elapsedTime = 0f;

            while (elapsedTime < wanderDuration)
            {
                rb2d.velocity = backingOffDirection * wanderSpeed;
                animator.SetFloat("BaseSpeed", Mathf.Abs(backingOffDirection.magnitude));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            animator.speed = 1f;
            isWandering = false;
            rb2d.velocity = Vector2.zero;
            animator.SetFloat("BaseSpeed", 0);
        }
        }
    }
}
