using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Lets the user navigate using the main menu and the level select menu. 
 * NOTE: Scene Index Values can be found in file>>build settings. 
 * Please do NOT change the index values.
 */

public class MainMenu : MonoBehaviour
{
    //The two menu objects
    public GameObject mainMenu, lvlSelectMenu;

    public void loadMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    //Loads Level 1
    public void startPlay()
    {
        SceneManager.LoadSceneAsync(1);
    }

    //Loads Level 2
    public void startLevel2()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void quitPlay()
    {
        Application.Quit();
    }

}
