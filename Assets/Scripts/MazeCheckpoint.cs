using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will do events at checkpoint
public class MazeCheckpoint : MonoBehaviour
{
    public static System.EventHandler<System.EventArgs> onCheckpoint;
    public List<GameObject> walls;
    public bool passed;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject w in walls) {
            w.GetComponent<MazeWallsIntangibility>().passable = false;
            passed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            foreach (GameObject w in walls)
            {
                w.GetComponent<MazeWallsIntangibility>().passable = true;
                if (!passed)
                {
                    TriggerDialogue();
                }
            }
            passed = true;
        }
    }

    public void TriggerDialogue()
    {
        onCheckpoint?.Invoke(this, new System.EventArgs());
    }
}
