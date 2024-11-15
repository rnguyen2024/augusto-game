using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreserveEventSystem : MonoBehaviour
{
    void Awake()
    {
        var eventSystems = FindObjectsOfType<UnityEngine.EventSystems.EventSystem>();

        DontDestroyOnLoad(gameObject);

        // Destroy any duplicates
        for (int i = 0; i < eventSystems.Length; i++)
        {
            if (i > 0) // Keep the first one
            {
                Destroy(eventSystems[i].gameObject);
            }
        }

    }

   
    void Update()
    {
        
    }
}
