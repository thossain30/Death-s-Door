using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    public static int count = 0;
    public int max = 100;
    GroundSpawner ground;
    public static bool complete = false;
    public GameObject obstaclePrefab;
    // Start is called before the first frame update
    private void Start()
    {
        ground = GameObject.FindObjectOfType<GroundSpawner>();
    }
    private void Update()
    {
        if (count == max) {
            complete = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ground.spawnTile(true);
        Destroy(gameObject, 1);
        count++;
        Debug.Log(count);
    }
    public void spawnObstacle()
    {
        //index for whether obstacle spawns left, middle or right
        int obstacleSpawnIndex = Random.Range(2, 5);
        //basically a bool for whether an obstacle spawns or not
        int spawnPerhaps = Random.Range(0, 3);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        if (spawnPerhaps >= 1)
        {
            Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
