using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3RestartToPoint : MonoBehaviour
{
    public GameObject objToReset;
    public Transform point;

    public T3ButtonPuzzleManager puzzleManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartToPoint(objToReset);
        }
    }

    public void RestartToPoint(GameObject g)
    {
        //Debug.Log("Restarting!");

        CharacterController cc = g.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;
        g.transform.position = point.transform.position;
        if (cc != null) cc.enabled = true;
        g.transform.rotation = point.transform.rotation;

        puzzleManager.ResetGrid();
    }
}
