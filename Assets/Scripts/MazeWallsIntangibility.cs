using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWallsIntangibility : MonoBehaviour
{
    [Header("Objects")]
    public GameObject wall;
    public GameObject player;
    //makes it so that you can only go intangible and pass after reaching checkpoint
    public bool passable;
    public Material material;
    Color oldColor = new Color();
    [Header("Opacity Values")]
    public float trans = 0.3f;
    public float BlueTintValue = 1f;
    float blueTint;
    float normal = 1f;
    bool IntaOn = false;
    // Start is called before the first frame update
    void Start()
    {
        Color oldColor = material.color;
        passable = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool IntaOn = player.GetComponent<Intangibility>().IntaOn;
        if (passable)
        {
            if (IntaOn)
            {
                wall.GetComponent<BoxCollider>().enabled = false;//Dissable Collider
                blueTint = BlueTintValue;
                ChangeAlpha(wall.GetComponent<MeshRenderer>().material, trans);//Make wall transparent
            }
            else
            {
                wall.GetComponent<BoxCollider>().enabled = true;//Enable Collider
                blueTint = oldColor.b;
                ChangeAlpha(wall.GetComponent<MeshRenderer>().material, normal);//Reset Transparancy
            }
        }
    }

    //Change opacity of wall
    void ChangeAlpha(Material mat, float alphaVal)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, blueTint, alphaVal);
        mat.SetColor("_Color", newColor);
    }
}
