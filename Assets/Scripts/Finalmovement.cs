using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finalmovement : MonoBehaviour
{
    public static spawnTile spawn;
    // Start is called before the first frame update
    void Start()
    {
        spawn = GetComponent<spawnTile>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
            spawn.onSpawn = false;
        }
    }
}
