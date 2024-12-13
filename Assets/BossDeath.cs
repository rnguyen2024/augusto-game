using UnityEngine;

public class BossDeath : MonoBehaviour
{
    public GameObject deathScreen; // Assign your UI screen in the Inspector

    // This function is called when the enemy dies
    public void OnEnemyKilled()
    {
        if (deathScreen != null)
        {
            deathScreen.SetActive(true); // Show the screen
        }
    }
}