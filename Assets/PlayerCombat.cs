using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCombat : MonoBehaviour
{
    
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;
    
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private PlayerControls playerControls;

    void Start()
    {
        playerControls = GetComponent<PlayerControls>(); 
    }
    void Update()
    {   
        if (playerControls.isStunned == false)
        {
            Debug.Log("Not stunned");

            if(Time.time >= nextAttackTime){
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
    }

    void Attack(){
        animator.SetTrigger("Attack");


        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies){
            EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(attackDamage);
            }

            Enemy2AI enemy2AI = enemy.GetComponent<Enemy2AI>();
            if (enemy2AI != null)
            {
                enemy2AI.TakeDamage(attackDamage);
            }
        }
    }
}
