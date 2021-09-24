using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class RunnerMovement : MonoBehaviour
{
    public spawnTile spawn;
    bool alive = true;
    public float speed = 7.0f;
    public float gravity = -9.81f;
    Vector3 velocity;
    public CharacterController controller;
    public Animator anim1;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim1 = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawn.onSpawn)
        {
            if (!alive) return;
            if (GroundTile.complete)
            {
                SceneManager.LoadScene(0);
                return;
            }
            anim1.SetBool("atSpawn", false);

            float horizontal = Input.GetAxis("Horizontal");

            controller.Move(transform.forward * speed * Time.deltaTime + new Vector3(horizontal * 0.12f, 0, 0));
            velocity.y += (gravity * Time.deltaTime);
            controller.Move(velocity * Time.deltaTime);
        }
    }
    //This should eventually spawn the player to the last checkpoint (or spawn) instead of Lobby!
    public void Die() {
        alive = false;
        GroundTile.count = 0;
        //restarts game
        SceneManager.LoadScene(0);
        //SceneManager.GetActiveScene().name
    }
}
