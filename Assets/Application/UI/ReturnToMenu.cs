using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public GameObject pause; 
    public KeyCode pauseButton = KeyCode.Escape; 
    private bool paused = false; 

    void Start()
    {
        if (pause != null)
        {
            pause.SetActive(false);
        }
        paused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            if (paused)
            {
                ResumeGame();         
            }
            else
            {
                PauseGame();               
            }
        }
    }

    void PauseGame()
    {
        pause.SetActive(true); 
        Time.timeScale = 0f;   
        paused = true;           
    }

    public void ResumeGame()
    {
       pause.SetActive(false); 
       Time.timeScale = 1f;   
       paused = false;               
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(0);
    }
}
