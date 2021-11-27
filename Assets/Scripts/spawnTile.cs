using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTile : MonoBehaviour
{
    public bool onSpawn;
    public static System.EventHandler<System.EventArgs> onRunning;
    // Start is called before the first frame update

    public void setonSpawn(bool running)
    {
        onSpawn = running;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor") 
        {
            setonSpawn(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
            setonSpawn(false);
            onRunning.Invoke(this, null);
        }
    }
}
