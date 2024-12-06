using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 8f;
    
    private float currentSpeed;


    private float speedX, speedY;
    private Rigidbody2D rb;
    private Vector2 movement;
    public Animator animator;
    public bool isStunned;
    

    //Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = speed;
    }

    //Update is called once per frame
    void Update()
    {
        if (isStunned == true)
        {
            animator.SetFloat("BaseSpeed", 0);
            return; 
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.magnitude > 1){

            movement = movement.normalized;
            
        }
    
        
        animator.SetFloat("BaseSpeed", Mathf.Abs(movement.y));
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
        

        
        if (movement.x != 0) //Checks if the player is moving horizontally
        {
            animator.SetFloat("BaseSpeed", Mathf.Abs(movement.x));
            //Flip the character based on the horizontal movement direction
            if (movement.x > 0) //Moving right
            {
                transform.localScale = new Vector3(-3, 3, 1);
            }
            else if (movement.x < 0) //Moving left
            {
            transform.localScale = new Vector3(3, 3, 1);
            }
        }
    }

    public void ApplySlowEffect(float slowMultiplier)
    {
        Debug.Log($"Applying slow effect. Multiplier: {slowMultiplier}");
        currentSpeed = speed * slowMultiplier; // Reduce speed
    }

    public void RemoveSlowEffect()
    {   
        Debug.Log("Removing slow effect.");
        currentSpeed = speed; // Restore speed
    }

    //Player cannot move
    public void applyStun(float stunDuration)
    {
        
        StartCoroutine(Stun(stunDuration));
       
    }

    private IEnumerator Stun(float stunDuration)
    {
        isStunned = true;
        float originalSpeed = currentSpeed; 
        currentSpeed = 0;
        animator.SetFloat("BaseSpeed", 0);

        yield return new WaitForSeconds(stunDuration); 

        currentSpeed = originalSpeed; 
         isStunned = false;
    }
}
