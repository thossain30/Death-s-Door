using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    public class RunnerTileArg : System.EventArgs
    {
        public int tileCount;
    }

    public static System.EventHandler<RunnerTileArg> onExitTile;

    public static int count = 0;
    private int max = 4;
    GroundSpawner ground;
    //bool which will change when player collects artifact
    public static bool complete = false;
    //bool which will determine when endTile will spawn
    public static bool end;
    public GameObject obstaclePrefab;
    public GameObject bonePrefab;
    GameObject ob;
    // Start is called before the first frame update
    private void Start()
    {
        ground = GameObject.FindObjectOfType<GroundSpawner>();
        end = false;
    }
    private void Update()
    {
        if (BoneCollector.bonesCollected > max)
        {
            end = true;
            Debug.Log("End? " + end);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(gameObject, 1);
        Destroy(ob);
        if (!end)
        {
            if (GroundTile.count < 50)
            {
                ground.spawnTile(true, false);
                Debug.Log("less than 50");
            }
            if (GroundTile.count >= 50)
            {
                ground.spawnTile(true, true);
                Debug.Log("more than 50");
            }
            Debug.Log(count);
        }
        if (end)
        {
            ground.spawnEndTile();
            Obstacle[] Obstacles = GameObject.FindObjectsOfType<Obstacle>();
            foreach (Obstacle ob in Obstacles)
            {
                ob.Despawn();
            }
            count = 0;
        }
        count++;
        OnExitTile();
    }

    private void OnExitTile()
    {
        //print(count);
        onExitTile?.Invoke(this, new RunnerTileArg { tileCount = count });
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
            ob = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity);
        }
    }
    public void spawnCoin()
    {
        //index for whether obstacle spawns left, middle or right
        int obstacleSpawnIndex = Random.Range(7, 10);
        //basically a bool for whether an obstacle spawns or not
        int spawnPerhaps = Random.Range(0, 3);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        if (spawnPerhaps >= 1)
        {
            ob = Instantiate(bonePrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
