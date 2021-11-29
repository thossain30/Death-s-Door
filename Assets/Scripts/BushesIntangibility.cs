using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushesIntangibility : MonoBehaviour
{
    [Header("Objects")]
    public GameObject wall;
    public GameObject player;
    //makes it so that you can only go intangible and pass after reaching checkpoint
    public Material[] materials;
    Color oldColor = new Color();
    Color oldoldColor = new Color();
    [Header("Opacity Values")]
    public float BlueTintValue = 1f;
    public float transparency = 0.5f;
    bool IntaOn = false;
    // Start is called before the first frame update
    void Start()
    {
        Color oldColor = materials[0].color;
        Color oldoldColor = materials[1].color;
    }

    // Update is called once per frame
    void Update()
    {
        bool IntaOn = player.GetComponent<Intangibility>().IntaOn;
    
        if (IntaOn)
        {
            Debug.Log("Pressed");
            ChangeAlpha(wall.GetComponent<MeshRenderer>().materials[0], transparency, BlueTintValue);//Make wall transparent
            ChangeAlpha(wall.GetComponent<MeshRenderer>().materials[1], transparency, BlueTintValue);//Make wall transparent
        }
        else
        {
            ChangeAlpha(wall.GetComponent<MeshRenderer>().materials[0], 255f, oldColor.b);//Make wall transparent
            ChangeAlpha(wall.GetComponent<MeshRenderer>().materials[1], 255f ,oldoldColor.b);//Make wall transparent
        }
 
    }

    //Change opacity of wall
    void ChangeAlpha(Material mat, float alphaVal, float blueVal)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, blueVal, alphaVal);
        mat.SetColor("_Color", newColor);
    }
}
