using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float chaseDistance;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Finds distance between two transforms in this case object with script and player
        distance = Vector2.Distance(transform.position, player.transform.position);

        //Checks if distance is close enough for enemy to chase, if it is then it updates the position to the players position
        if(distance < chaseDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}
