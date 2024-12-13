using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoad : MonoBehaviour
{
public void LoadMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(0);
    }
}