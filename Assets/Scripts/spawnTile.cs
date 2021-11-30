using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTile : MonoBehaviour
{
    public bool onSpawn;
    // Start is called before the first frame update
    public static System.EventHandler<System.EventArgs> enterSpawn;

    public void setonSpawn(bool running)
    {
        onSpawn = running;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor") 
        {
            setonSpawn(true);
            EnterSpawn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
            setonSpawn(false);
        }
    }
    public void EnterSpawn()
    {
        enterSpawn?.Invoke(this, new System.EventArgs());
    }
}
