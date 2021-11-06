using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{

    public Transform player;
    public Rigidbody follower;
    public float smooth = 20.0F;

    // Start is called before the first frame update
    void Start()
    {
        follower = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 distance = player.position - transform.position;
        // float rotation = Input.GetAxis("Horizontal")
        // chaser.Rotate(0, 0, 0);
        // chaser.LookAt(player, Vector3.forward);
        var lookPos = player.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smooth);
        if (distance.sqrMagnitude > 15) {
            follower.AddForce(distance.x, 0, distance.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
