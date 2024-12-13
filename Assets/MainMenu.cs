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
        SceneManager.LoadScene(0);
    }

    //Loads Level 1
    public void startPlay()
    {
        SceneManager.LoadScene(1);
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }

    //Loads Level 2
    public void startLevel2()
    {
        SceneManager.LoadScene(1);
        SceneManager.LoadScene(6, LoadSceneMode.Additive);
    }

    public void quitPlay()
    {
        Application.Quit();
    }

}
