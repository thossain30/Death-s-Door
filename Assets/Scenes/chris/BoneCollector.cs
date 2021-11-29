using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneCollector : MonoBehaviour
{

    public static int bonesCollected = 0;
    public float radius = 2;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] nearby = Physics.OverlapSphere(player.position, radius);
        foreach (var collider in nearby) {
            if (collider.tag == "Bone") {
                Destroy (collider.gameObject);
                ++bonesCollected;
            }
        }
    }
}
