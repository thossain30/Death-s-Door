using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will do events at checkpoint
public class MazeCheckpoint : MonoBehaviour
{
    public static System.EventHandler<System.EventArgs> onCheckpoint;
    public GameObject wall;
    public bool passed;
    // Start is called before the first frame update
    void Start()
    {
        wall.GetComponent<MazeWallsIntangibility>().passable = false;
        passed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            wall.GetComponent<MazeWallsIntangibility>().passable = true;
            if (!passed) {
                TriggerDialogue();
            }
            passed = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Left trigger");
    }

    public void TriggerDialogue()
    {
        onCheckpoint?.Invoke(this, new System.EventArgs());
    }
}
