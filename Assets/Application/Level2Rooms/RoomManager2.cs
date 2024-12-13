using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager2 : MonoBehaviour
{
    private int sceneIndex;
    private bool prevRoom;
    private loadscreeen loadScreen;

    private void Start()
    {
        loadScreen = FindObjectOfType<loadscreeen>();

        Debug.Log("Loading lvl2");

        if (SceneManager.GetActiveScene().buildIndex != 0 && !SceneManager.GetSceneByBuildIndex(2).isLoaded)
        {
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }

        sceneIndex = 6;
        prevRoom = false;
        //loadRoom(sceneIndex, prevRoom);
    }

    public void loadRoom(int sceneIndex, bool prevRoom)
    {
        loadScreen.Transition();

        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        if (sceneIndex > 6 && !prevRoom)
        {
            SceneManager.UnloadSceneAsync(sceneIndex - 1);
        }
        else if (prevRoom)
        {
            SceneManager.UnloadSceneAsync(sceneIndex + 1);
        }

    }
}
