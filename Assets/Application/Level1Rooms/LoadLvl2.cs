using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLvl2 : MonoBehaviour
{
    public int nextScene;
    public int currentScene;
    public GameObject GameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void startLevel2()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(6, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(5);
    }
}
