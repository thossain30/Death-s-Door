using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class RunnerMovement : MonoBehaviour
{
    public spawnTile spawn;
    private bool turnLeft, turnRight;
    bool alive = true;
    public float speed = 7.0f;
    public float gravity = -9.81f;
    Vector3 velocity;
    //accesses mouse position
    Vector3 mousePos;
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
            anim1.SetBool("atSpawn", false);

            //turnLeft = Input.GetKeyDown(KeyCode.A);
            //turnRight = Input.GetKeyDown(KeyCode.D);
            mousePos = Input.mousePosition;
            //moves player left and right on tile
            Vector3 groundPos = new Vector3((mousePos.x - (Screen.width / 2)) / 11020, 0, 0);


            //if (turnLeft)
            //    transform.Rotate(new Vector3(0f, -90f, 0f));
            //else if (turnRight)
            //    transform.Rotate(new Vector3(0f, 90f, 0f));
            //if (controller.isGrounded) {
            //    velocity.y = 0;
            //}
            //controller.SimpleMove(new Vector3(0f, 0f, 0f));
            controller.Move(transform.forward * speed * Time.deltaTime + new Vector3(groundPos.x, 0, 0));
            velocity.y += (gravity * Time.deltaTime);
            controller.Move(velocity * Time.deltaTime);
        }
    }
    public void Die() {
        alive = false;
        //restarts game
        SceneManager.LoadScene(0);
        //SceneManager.GetActiveScene().name
    }
}
