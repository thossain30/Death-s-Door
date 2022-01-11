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
                spawnTile(false, false);
            }
            else
            {
                spawnTile(true, false);
            }
        }
    }
    public void spawnTile(bool spawnObs, bool spawnCoin)
    {
        GameObject temp2 = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp2.transform.GetChild(0).transform.position;
        if (spawnObs)
        {
            temp2.GetComponent<GroundTile>().spawnObstacle();
        }
        if (spawnCoin)
        {
            temp2.GetComponent<GroundTile>().spawnCoin();
        }
    }
    public void spawnEndTile()
    {
        //here to ensure only one endtile gets spawned (probably a better way to do this tbh)
        if (endTileCount <= 1)
        {
            Instantiate(endTile, nextSpawnPoint, Quaternion.identity);
            endTileCount++;
        }
        nextSpawnPoint = startingTile.transform.position;
    }
}
