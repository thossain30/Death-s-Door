using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushesIntangibility : MonoBehaviour
{
    [Header("Objects")]
    //makes it so that you can only go intangible and pass after reaching checkpoint
    public Material[] materials;
    Color oldColor = new Color();
    Color oldoldColor = new Color();
    [Header("Opacity Values")]
    public float BlueTintValue = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Color oldColor = materials[0].color;
        Color oldoldColor = materials[1].color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Intangibility"))
        {
            ChangeAlpha(materials[0], BlueTintValue);//Make wall transparent
            ChangeAlpha(materials[1], BlueTintValue);//Make wall transparent
        }
        else if(Input.GetButtonUp("Intangibility"))
        {
            ChangeAlpha(materials[0], oldColor.b);//Make wall transparent
            ChangeAlpha(materials[1], oldoldColor.b);//Make wall transparent
        }
 
    }

    //Change opacity of wall
    void ChangeAlpha(Material mat, float blueVal)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, blueVal, oldColor.a);
        mat.SetColor("_Color", newColor);
    }
}
