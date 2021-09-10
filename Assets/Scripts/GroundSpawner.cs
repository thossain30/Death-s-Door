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
    private Vector3 direction, mainDirection = new Vector3(0, 0, 1), otherDirection = new Vector3(1, 0, 0);

    private void Start()
    {
        for (int i = 0; i < length; i++)
        {
            if (i < 3)
            {
                spawnTile(false);
            }
            else
            {
                spawnTile(true);
            }
        }
    }
    private void Update()
    {
        if (Time.time - startTime > timeOffset) {
            if (Random.value < randomValue)
            {
                direction = mainDirection;
            }
            else
            {
                Vector3 temp = direction;
                direction = otherDirection;
                mainDirection = direction;
                otherDirection = temp;
            } 
        }
    }
    public void spawnTile(bool spawnObs)
    {
        GameObject temp2 = Instantiate(groundTile, nextSpawnPoint, Quaternion.Euler(0, 0, 0));
        nextSpawnPoint = temp2.transform.GetChild(0).transform.position;

        if (spawnObs) {
            temp2.GetComponent<GroundTile>().spawnObstacle();
        }
    }
}
