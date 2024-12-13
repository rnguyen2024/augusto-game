using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusic : MonoBehaviour
{
    public static MenuMusic Instance;
    public AudioClip menuMusic;
    public AudioClip normalLevelMusic;
    public AudioClip bossMusic;

    private AudioSource audioSource;

    void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetupAudio();
        }
        else
        {
            // Another instance is trying to spawn, destroy it
            Destroy(gameObject);
        }
    }

    void SetupAudio()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.spatialBlend = 0f;
        audioSource.volume = .4f;
        audioSource.clip = menuMusic;
    }

    void OnEnable()
{
    SceneManager.sceneLoaded += OnSceneLoaded;
}

void OnDisable()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;
}

void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    string sceneName = scene.name;
    AudioClip newClip = GetClipForScene(sceneName);

    if (audioSource.clip != newClip)
    {
        audioSource.clip = newClip;
        audioSource.Play();
    }
    else
    {
        // Same track, do nothing, so the music doesn't restart
    }
}

AudioClip GetClipForScene(string sceneName)
{
    if (sceneName == "Menu" || sceneName == "Level 2")
    {
        return menuMusic;
    }
    else if (sceneName == "Level1Room3" || sceneName == "Level2Room5")
    {
        return bossMusic;
    }
    else if(sceneName == "Level1Room1" || sceneName == "Level1Room2" || sceneName == "Level2Room1" || sceneName == "Level2Room2" || sceneName == "Level2Room3" || sceneName == "Level2Room4")
    {
        return normalLevelMusic;
    }
    else
    {
        return menuMusic;
    }
}
    
}

