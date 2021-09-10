using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    RunnerMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        movement = GameObject.FindObjectOfType<RunnerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision detected!");
        if (other.gameObject.tag == "Player")
        {
            //kills the player
            Debug.Log("collided with player");
            movement.Die();
        }
    }
}
