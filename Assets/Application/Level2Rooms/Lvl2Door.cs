using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Lvl2Door : MonoBehaviour
{
    public GameObject levelComplete;
    void OnTriggerEnter2D(Collider2D other)
    {
        levelComplete.SetActive(true);

    }
}
