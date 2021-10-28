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
        if (other.gameObject.tag == "Player")
        {
            movement.speed -= 1.75f;
        }
    }
}
