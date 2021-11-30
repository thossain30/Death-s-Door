using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class RunnerMovement : MonoBehaviour
{
    public spawnTile spawn;
    bool alive = true;
    public float speed;
    private float maxSpeed = 16f;
    public float gravity = -9.81f;
    Vector3 velocity;
    public CharacterController controller;
    public Animator anim1;
    public Animator dogAnim;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim1 = GetComponent<Animator>();
        dogAnim.SetBool("atSpawn", true);
        speed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //this if statement runs when player is not on spawn tile
        if (!spawn.onSpawn)
        {
            
            //breaks out of if statement if player dies
            if (!alive) return;
            //checks if trial has been completed
            if (GroundTile.complete)
            {
                SceneManager.LoadScene(0);
                return;
            }
            //sets the animators for the player and the dog to their running state once no longer in spawn
            anim1.SetBool("atSpawn", false);
            dogAnim.SetBool("atSpawn", false);

            //code responsible for moving player left and right!
            float horizontal = Input.GetAxis("Horizontal");

            //does the moving of the player 
            controller.Move(transform.forward * speed * Time.deltaTime + new Vector3(horizontal * 0.105f, 0, 0));
            velocity.y += (gravity * Time.deltaTime);
            controller.Move(velocity * Time.deltaTime);
        }
        if (speed < maxSpeed)
        {
            speed += 1 * Time.deltaTime;
        }
        //if (GroundTile.complete)
        //{
        //    SceneManager.LoadScene(0);
        //    return;
        //}
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
