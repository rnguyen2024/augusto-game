using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;

    /// <summary>
    /// Loads Level 1, which is assigned to scene index 1 in the build settings.
    /// The menu scene is assigned to scene index 0.
    /// Level 2 will likely be assigned to scene index 2 when it gets implemented.
    /// </summary>
    public void startPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void quitPlay()
    {
        Application.Quit();
    }

}
