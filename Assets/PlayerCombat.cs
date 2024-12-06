using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using UnityEngine.UIElements;
>>>>>>> main

public class PlayerCombat : MonoBehaviour
{
    
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;
    
    public float attackRate = 2f;
    float nextAttackTime = 0f;
<<<<<<< HEAD

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime){
            if(Input.GetKeyDown(KeyCode.Mouse0)){
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
=======
    private bool attackBoost = false;

    private PlayerControls playerControls;
    private GameObject atkBuffIcon;

    public AudioClip attackSFX;
    private AudioSource sfxSource;


    void Start()
    {
        playerControls = GetComponent<PlayerControls>();
        atkBuffIcon = GameObject.Find("AttackUpStatus");
        atkBuffIcon.SetActive(false);

        sfxSource = GetComponent<AudioSource>();
    }
    void Update()
    {   
        if (playerControls.isStunned == false)
        {
            //Debug.Log("Not stunned");

            if(Time.time >= nextAttackTime){
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
>>>>>>> main
            }
        }
    }

    void Attack(){
        animator.SetTrigger("Attack");

<<<<<<< HEAD
=======
        if (attackSFX != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(attackSFX);
        }
>>>>>>> main

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies){
<<<<<<< HEAD
            enemy.GetComponent<EnemyScript>().TakeDamage(attackDamage);
        }
    }
=======
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
    public void applyAtackBuff(int bonus)
    {
        StartCoroutine(attackBonus(bonus));
    }

    private IEnumerator attackBonus(int bonus)
    {
        Debug.Log("bonus start now");
        attackBoost = true;
        atkBuffIcon.SetActive(true);
        int originalDamage = attackDamage;
        attackDamage = attackDamage + bonus;

        Debug.Log(attackDamage);
        yield return new WaitForSeconds(15f);

        attackDamage = originalDamage;
        attackBoost = false;
        atkBuffIcon.SetActive(false);
        Debug.Log("Bonus over");
    }
>>>>>>> main
}
