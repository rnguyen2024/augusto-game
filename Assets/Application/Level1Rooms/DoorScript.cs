using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public int nextLevelIndex;
    public bool prevRoom;
    private RoomManager roomManager;
<<<<<<< HEAD
=======
    
>>>>>>> main

    void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
<<<<<<< HEAD
=======
        
>>>>>>> main
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //If player comes into contact with the "door" 
        //Calls loadRoom in room Manager to load the next scene
        if (other.CompareTag("Player"))
        {
<<<<<<< HEAD
            roomManager.loadRoom(nextLevelIndex, prevRoom);
=======

            roomManager.loadRoom(nextLevelIndex, prevRoom);
           
>>>>>>> main
        }
    }
}
