using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trial1Dog : MonoBehaviour
{
    public spawnTile spawn;
    public RunnerMovement movement;
    public Animator dogAnim;
    public Transform player;
    public AudioSource source;
    public float MoveSpeed;
    private bool playerpos;
    // Start is called before the first frame update
    void Start()
    {
        dogAnim.SetBool("atSpawn", true);
        playerpos = true;
    }

    // Update is called once per frame
    void Update()
    {
        int barkPerhaps = Random.Range(0, 4);
        if (!spawn.onSpawn)
        {
            dogAnim.SetBool("atSpawn", false);
            transform.rotation = Quaternion.identity;
            if (GroundTile.count % 10 == 0)
            {
                source.Play();
            }
            if (playerpos)
            {
                transform.position += new Vector3(player.position.x - transform.position.x, 0, 3f);
            }
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            transform.position += new Vector3(player.position.x - transform.position.x, 0, 0);
            playerpos = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            movement.Die();
        }
    }
}
