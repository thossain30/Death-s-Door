using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public Scene scene;
    public GameObject text;
    public GameObject compText;
    [SerializeField]
    public GameObject DoorLight;
    Light rad;
    public bool nextToDoor = false;
    // Start is called before the first frame update
    void Start()
    {
        rad = DoorLight.GetComponent<Light>();
        text.SetActive(false);
        compText.SetActive(false);
        rad.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && nextToDoor && !GroundTile.complete)
        {
            if (scene == null)
            {
                scene = SceneManager.GetActiveScene();
            }
            SceneManager.LoadScene("Trial1Copy");
        }
        if (GroundTile.complete) {
            rad.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            text.SetActive(true);
            nextToDoor = true;
            if (GroundTile.complete) {
                compText.SetActive(true);
                text.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            text.SetActive(false);
            nextToDoor = false;
            compText.SetActive(false);
        }
    }
}
