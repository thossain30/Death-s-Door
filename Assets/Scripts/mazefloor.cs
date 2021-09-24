using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazefloor : MonoBehaviour
{
    private GameObject partSys;
    // Start is called before the first frame update
    void Start()
    {
        partSys = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            partSys.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            partSys.SetActive(false);
        }
    }
}
