using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*This is a temporary script only used to allow the player to navigate back to the home menu
 * from the current level 2 filler screen. This will be replaced and deleted once level 2 actually 
 * gets implemented.
 */

public class Level2Script : MonoBehaviour
{
    //Loads the main menu when the back button is pushed. 
    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
