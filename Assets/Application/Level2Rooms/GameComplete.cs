using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameComplete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void returnToMain()
    {
        SceneManager.UnloadSceneAsync(10);
        SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadScene(0);
    }
}
