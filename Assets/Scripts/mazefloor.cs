using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mazefloor : MonoBehaviour
{
    private GameObject partSys;
    public static bool complete = false;
    // Start is called before the first frame update
    void Start()
    {
        partSys = transform.GetChild(0).gameObject;
        Debug.Log(partSys);
        partSys.SetActive(false);
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
            Debug.Log(complete);
            if (transform.gameObject.name == "FinalFloor")
            {
                complete = true;
                if (complete)
                {
                    SceneManager.LoadScene(0);
                }
            }
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
