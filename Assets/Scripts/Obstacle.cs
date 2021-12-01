using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    RunnerMovement movement;
    public GameObject player;
    //public bool IntaOn;
    // Start is called before the first frame update
    void Start()
    {
        movement = GameObject.FindObjectOfType<RunnerMovement>();
    }
    public void Despawn()
    {
        Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        //bool isInit = player.GetComponent<Intangibility>().IntaOn;
       
        if (other.gameObject.tag == "Player")
        {
            bool IntaOn = other.GetComponent<Intangibility>().IntaOn;
            //Slow player if he hits obstacle without intangibility
            if (!IntaOn)
            {
                movement.speed -= 1f;
            //Dont slow him if he has it on
            } else if(IntaOn)
            {
                movement.speed -= 0f;
            }
            
        }
    }
}
