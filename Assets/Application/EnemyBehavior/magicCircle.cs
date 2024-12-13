using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    public float lifeTime;
    private Transform caster;
    public float stunDuration;
    private Spell enemy2;

    void Start()
    {
        Destroy(gameObject, lifeTime);

        if (caster != null)
        {
            enemy2 = caster.GetComponent<Spell>();

            if (enemy2 != null)
            {
                enemy2.stopMovement();
            }
        }
    }

    void OnDestroy()
    {
        if (enemy2 != null)
        {
            enemy2.resumeMovement();
        }
    }


    public void Initialize(Transform caster)
    {
        this.caster = caster;

        if (caster != null)
        {
            transform.position = caster.position;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the AoE is the player
        //Paralize Player
        if (other.CompareTag("Player"))
        {
            PlayerControls playerControls = other.GetComponent<PlayerControls>();
            if (playerControls != null)
            {
                playerControls.applyStun(stunDuration);
                Debug.Log("Player stunned");
            }
        }
    }
}
