using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLvl2 : MonoBehaviour
{
    public int nextScene;
    public int currentScene;
    public GameObject GameObject;
    private loadscreeen loadScreen;

    // Start is called before the first frame update
    void Start()
    {
        loadScreen = FindObjectOfType<loadscreeen>();
    }

    // Update is called once per frame
    public void startLevel2()
    {
        gameObject.SetActive(false);
        
        SceneManager.UnloadSceneAsync(5);
        loadScreen.Transition();
        SceneManager.LoadScene(6, LoadSceneMode.Additive);
    }
}
