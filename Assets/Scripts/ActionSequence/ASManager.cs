using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASManager : MonoBehaviour
{
    private int seqIndex;
    public List<ActionSequence> sequences = new List<ActionSequence>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GroundTile.complete)
        {
            sequences[0].enabled = false;
        }
    }
}
