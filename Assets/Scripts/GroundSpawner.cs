using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject startingTile;
    public GameObject groundTile;
    public GameObject endTile;
    static int endTileCount = 0;
    private bool spawn = false;
    public Vector3 nextSpawnPoint;
    //number of tiles to spawn ahead of player
    private int length = 10;
    public float timeOffset = 0.4f;

    private void Start()
    {
        for (int i = 0; i < length; i++)
        {
            if (i < 5)
            {
                spawnTile(false);
            }
            else
            {
                spawnTile(true);
            }
        }
    }
    public void spawnTile(bool spawnObs)
    {
        GameObject temp2 = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp2.transform.GetChild(0).transform.position;
        if (spawnObs)
        {
            temp2.GetComponent<GroundTile>().spawnObstacle();
        }
    }
    public void spawnEndTile()
    {
        //here to ensure only one endtile gets spawned (probably a better way tbh)
        if (endTileCount <= 1)
        {
            Instantiate(endTile, nextSpawnPoint, Quaternion.identity);
            endTileCount++;
        }
        nextSpawnPoint = startingTile.transform.position;
    }
}
