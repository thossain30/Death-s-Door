using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    Vector3 nextSpawnPoint;
    //number of tiles to spawn ahead of player
    private int length = 10;
    private float randomValue = 0.01f;
    private float startTime;
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

        if (spawnObs) {
            temp2.GetComponent<GroundTile>().spawnObstacle();
        }
    }
}
