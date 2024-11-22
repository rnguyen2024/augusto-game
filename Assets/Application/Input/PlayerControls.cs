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
    

    //Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = speed;
    }

    //Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("BaseSpeed", Mathf.Abs(movement.x));
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);

        

        if (movement.x != 0) //Checks if the player is moving horizontally
        {
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
    
}
