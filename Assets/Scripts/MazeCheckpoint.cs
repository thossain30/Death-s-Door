using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will do events at checkpoint
//For the maze, if the player enters the trigger, it will disable the wall
public class MazeCheckpoint : MonoBehaviour
{
    public GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        wall.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        wall.SetActive(false);
    }
}
