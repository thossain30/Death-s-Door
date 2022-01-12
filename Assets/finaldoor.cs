using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finaldoor : MonoBehaviour
{
    public GameObject text;
    private bool nextToDoor = false;
    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && nextToDoor)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            text.SetActive(true);
            nextToDoor = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            text.SetActive(false);
            nextToDoor = false;
        }
    }
}
