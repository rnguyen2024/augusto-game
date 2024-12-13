using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{

    private int sceneIndex;
    private bool prevRoom;
    private loadscreeen loadScreen;

    private void Start()
    {
        loadScreen = FindObjectOfType<loadscreeen>();

        if (SceneManager.GetActiveScene().buildIndex != 0 && !SceneManager.GetSceneByBuildIndex(1).isLoaded)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }

        sceneIndex = 3;
        prevRoom = false;
        loadScreen.Transition();
        //loadRoom(sceneIndex, prevRoom);
    }

    public void loadRoom(int sceneIndex, bool prevRoom)
    {
        loadScreen.Transition();
       
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        if (sceneIndex > 3 && !prevRoom)
        {
            SceneManager.UnloadSceneAsync(sceneIndex - 1);
        }
        else if (prevRoom)
        {
            SceneManager.UnloadSceneAsync(sceneIndex + 1);
        }

    }
}


