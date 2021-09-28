using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3Button : MonoBehaviour
{
    public int buttonID = -1;
    public int x = -1;
    public int y = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("banana");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("mango");
    }
}
