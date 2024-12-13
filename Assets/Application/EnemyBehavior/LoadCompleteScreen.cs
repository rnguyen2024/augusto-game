using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCompleteScreen : MonoBehaviour
{
    public GameObject levelCompleteScreen;

    private void Start()
    {
        levelCompleteScreen.SetActive(false);
    }

    private void OnDestroy()
    {
        levelCompleteScreen.SetActive(true);
    }
}
