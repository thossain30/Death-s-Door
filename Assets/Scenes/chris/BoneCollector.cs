using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneCollector : MonoBehaviour
{

    public static int bonesCollected = 0;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<RunnerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            bonesCollected++;
        }
    }
}
