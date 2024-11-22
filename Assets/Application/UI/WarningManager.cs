using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningManager : MonoBehaviour
{
    public GameObject warning;
    public bool collision;
    void Start()
    {
        if (collision==false)
        {
            warning.SetActive(false);
        
        }
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            warning.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            warning.SetActive(false);

        }
    }
}
