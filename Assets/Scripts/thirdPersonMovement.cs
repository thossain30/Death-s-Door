using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdPersonMovement : MonoBehaviour
{
    private spawnTile spawn;
    public CharacterController controller;
    public Transform cam;

    public float speed = 8f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;
    Vector3 velocity;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Animator anim;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        spawn = GetComponent<spawnTile>();
        spawn.onSpawn = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (spawn.onSpawn)
        {
            canMove();
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }
    private void canMove()
    {
        anim.SetBool("atSpawn", true);
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //controls logic for walking
        if (direction.magnitude >= 0.1f && !DialogueManager.IsDialogueOpen())
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            anim.SetBool("isWalking", true);
        }
        //sets character to idle state if no movement
        else if (direction.magnitude == 0)
        {
            anim.SetBool("isWalking", false);
        }
        //if character is on ground, set gravitational fall to 0
        if (controller.isGrounded)
        {
            velocity.y = 0f;
            anim.SetBool("isGrounded", true);
            anim.SetFloat("velocityY", 0);
        }
        //makes character fall
        velocity.y += (gravity * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }
}
