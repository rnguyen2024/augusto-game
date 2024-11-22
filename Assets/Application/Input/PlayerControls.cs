using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed;

    private float speedX, speedY;
    private Rigidbody2D rb;
    public Animator animator;
    

    //Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal");
        speedY = Input.GetAxisRaw("Vertical");
        animator.SetFloat("BaseSpeed", Mathf.Abs(speedX));
        rb.velocity = new Vector2(speedX, speedY).normalized * speed;

        

        if (speedX != 0) //Checks if the player is moving horizontally
        {
            //Flip the character based on the horizontal movement direction
            if (speedX > 0) //Moving right
            {
                transform.localScale = new Vector3(-3, 3, 1);
            }
            else if (speedX < 0) //Moving left
            {
            transform.localScale = new Vector3(3, 3, 1);
            }
        }
    }
}
