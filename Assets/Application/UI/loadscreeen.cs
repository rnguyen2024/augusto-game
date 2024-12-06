using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadscreeen : MonoBehaviour
{
    public GameObject loadingScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Transition()
    {
        StartCoroutine(loadRoom());
    }

    private IEnumerator loadRoom()
    {
          loadingScreen.SetActive( true );
          yield return new WaitForSeconds(0.5f); // Allow room to load completely
          loadingScreen.SetActive(false);
      
    }
}
