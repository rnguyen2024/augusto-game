using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private float distance;
    private float attackDistance;
    private Transform playerTransform;
    private Rigidbody2D rb2d;
    private Vector2 currentDirection;
    public Transform shootPoint;
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
        
    }

    private IEnumerator castSpell()
    {
        Vector2 shootDirection = (playerTransform.position - shootPoint.position).normalized;
        while (true)
        {

        }    
    }

    
}

