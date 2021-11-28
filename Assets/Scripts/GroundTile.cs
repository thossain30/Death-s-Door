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
    private int max = 20;
    GroundSpawner ground;
    //bool which will change when player collects artifact
    public static bool complete = false;
    //bool which will determine when endTile will spawn
    public static bool end;
    public GameObject obstaclePrefab;
    GameObject ob;
    // Start is called before the first frame update
    private void Start()
    {
        ground = GameObject.FindObjectOfType<GroundSpawner>();
        end = false;
    }
    private void Update()
    {
        if (count >= max) {
            end = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!end)
        {
            ground.spawnTile(true);
            count++;
        } else
        {
            ground.spawnEndTile();
            //count = 0;
            count++;
        }
        Destroy(gameObject, 1);
        if (ob != null)
        {
            Destroy(ob);
        }

        OnExitTile();
    }

    private void OnExitTile()
    {
        print(count);
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
}
