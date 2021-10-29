using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTile : MonoBehaviour
{
    public bool onSpawn;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor") 
        { 
            onSpawn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
            onSpawn = false;
        }
    }
}
