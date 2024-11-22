using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{

    private int sceneIndex;
    private bool prevRoom;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && !SceneManager.GetSceneByBuildIndex(1).isLoaded)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }

        sceneIndex = 3;
        prevRoom = false;
        loadRoom(sceneIndex, prevRoom);
    }

    public void loadRoom(int sceneIndex, bool prevRoom)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);

        if (sceneIndex > 3 && prevRoom == false)
        {
            SceneManager.UnloadSceneAsync(sceneIndex - 1);
        }

        if (prevRoom == true)
        {
            SceneManager.UnloadSceneAsync(sceneIndex + 1);
        }
        
    }



}
