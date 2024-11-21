using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    private Transform playerTransform;
    private Transform camTransform;
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;

        Camera cam = Camera.main;
        camTransform = cam.transform;

        playerTransform.position = transform.position;
        camTransform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
