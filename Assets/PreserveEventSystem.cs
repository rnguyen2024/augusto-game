using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Lets the event system persist so that the menus can access it while they are loaded. 
 */

public class PreserveEventSystem : MonoBehaviour
{
    void Awake()
    {
    
        DontDestroyOnLoad(gameObject);
    }

   
    void Update()
    {
        
    }
}
