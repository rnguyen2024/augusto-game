using System.Collections;
using UnityEngine;

public class PlayerFireballAbility : MonoBehaviour
{
    public GameObject fireballPrefab; 
    public Transform fireballSpawnPoint; 
    public float fireballSpeed = 10f;
    public int fireballImpactDamage = 20;
    public int fireballDamageOverTime = 5;
    public float fireballDOTDuration = 2f;
    public float fireballLifetime = 5f;
    public float fireballCooldown = 10f;

    private bool canShootFireball = true;

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canShootFireball)
        {
            ShootFireball();
        }
    }

    private void ShootFireball()
    {
        //Get mouse position in world coordinates
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 directionToCursor = (worldMousePosition - fireballSpawnPoint.position).normalized;

        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);

        //Initialize the fireball's direction and properties
        PlayerFireball fireballScript = fireball.GetComponent<PlayerFireball>();
        if (fireballScript != null)
        {
            fireballScript.Initialize(directionToCursor, fireballSpeed, fireballImpactDamage, fireballDamageOverTime, fireballDOTDuration, fireballLifetime);
        }

        StartCoroutine(FireballCooldown());
    }

    private IEnumerator FireballCooldown()
    {
        canShootFireball = false;
        yield return new WaitForSeconds(fireballCooldown);
        canShootFireball = true;
    }
}
