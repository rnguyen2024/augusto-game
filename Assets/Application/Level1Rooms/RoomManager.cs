using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{

    private int sceneIndex;
    public bool touchingDoor;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && !SceneManager.GetSceneByBuildIndex(1).isLoaded)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }

        sceneIndex = 3;
        loadRoom(sceneIndex);
    }

    public void loadRoom(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);

        if (sceneIndex > 3)
        {
            SceneManager.UnloadSceneAsync(sceneIndex - 1);
        }
        
    }

    public void loadNext()
    {
        loadRoom(sceneIndex + 1);
    }

}
