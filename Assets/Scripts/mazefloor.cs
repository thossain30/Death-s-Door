using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mazefloor : MonoBehaviour
{
    private GameObject partSys;
    public GameObject artifact;
    public GameObject text;
    public static bool complete = false;
    // Start is called before the first frame update
    void Start()
    {
        partSys = transform.GetChild(0).gameObject;
        Debug.Log(partSys);
        partSys.SetActive(false);
        artifact.SetActive(false);
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (complete)
        {
            SceneManager.LoadScene("Lobby");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            partSys.SetActive(true);
            if (transform.gameObject.name == "FinalFloor")
            {
                artifact.SetActive(true);
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
