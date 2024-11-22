using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Script to disable game objects until their scene is loaded. 
 * While this should usually happen by default, this is here in case of any unwanted exceptions. 
 */

public class ForceHide : MonoBehaviour
{
    //The scene that the object belongs to. Scene index numbers can be found in file>>build settings.
    //Set to 0 by default but the actual value is determined in the game object's inspector window.
    public int targetScene = 0;

    private void Start()
    {
        //Sets the object to disabled by default.
        gameObject.SetActive(false);
        showObject(targetScene);
    }

    public void showObject(int targetScene)
    {
        //If the correct scene is loaded, enables the object. 
        if(SceneManager.GetActiveScene().buildIndex == targetScene)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
