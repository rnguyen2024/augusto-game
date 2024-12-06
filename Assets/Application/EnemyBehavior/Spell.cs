using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OnChainStudios.FileTemplates.VisualScriptingTemplateFactory.MenuItemPaths.Variables;

public class Spell : MonoBehaviour
{
    private float distance;
    public float attackDistance;
    private Transform playerTransform;
    private Rigidbody2D rb2d;
    private Vector2 currentDirection;
    public Transform shootPoint;
    public float castDuration;
    public float castInterval;
    public GameObject circlePrefab;
    public float chaseDistance;
    public Animator animator;
    public float speed;

    private bool isCasting = false;

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
        distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance < chaseDistance)
        {
            //Cast spell if player is in range 
            if (distance < attackDistance && !isCasting)
            {
                StartCoroutine(castSpellRoutine());
            }
        }
    }

    private IEnumerator castSpellRoutine()
    {
        isCasting = true;
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        rb2d.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.5f);

        float timer = 0f;

        while (timer < castDuration)
        {
            castSpell(); 
            yield return new WaitForSeconds(castInterval);
            timer += castInterval;
        }
        isCasting = false;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    private void castSpell()
    {
        animator.SetTrigger("Attack");
        GameObject magicCircle = Instantiate(circlePrefab, shootPoint.position, Quaternion.identity);
        magicCircle.GetComponent<magicCircle>().Initialize(transform);
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

    public void stopMovement()
    {
        rb2d.bodyType = RigidbodyType2D.Static;
    }

    public void resumeMovement()
    {
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

}

