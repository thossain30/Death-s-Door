using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenIntangibility : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy regen sphere if player colliding
        if (other.gameObject.tag == "Player")
        {
            
            float IntaStamina = other.GetComponent<Intangibility>().intaStamina;
            float MaxStamina = other.GetComponent<Intangibility>().maxIntaStamina;
            //Regen player if he isnt max health
            if(IntaStamina < MaxStamina)
            {
                other.GetComponent<Intangibility>().intaStamina += 20 ;
                Destroy(gameObject);
            } else
            {
                IntaStamina += 0;
            }
        }
    }
}
