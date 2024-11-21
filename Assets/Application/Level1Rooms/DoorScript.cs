using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public int nextLevelIndex; 
    private RoomManager roomManager;

    void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            roomManager.loadRoom(nextLevelIndex);
        }
    }
}
