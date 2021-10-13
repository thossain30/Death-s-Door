using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3artifact : MonoBehaviour
{
    public GameObject text;
    // Update is called once per frame
    void Start()
    {
        text.SetActive(false);
    }

    [System.Obsolete]
    void Update()
    {
        if (this.gameObject.active)
        {
            if (Input.GetKey(KeyCode.E))
            {
                T3ButtonPuzzleManager.complete = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        text.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }
}