using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3Button : MonoBehaviour
{
    public int buttonID = -1;
    public int x = -1;
    public int y = -1;

    public bool isPressed = false;

    private Color currentColor;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        T3ButtonPuzzleManager.AddT3ButtonToGrid(this);
    }


    public Color GetColor()
    {
        return currentColor;
    }

    public void SetColor(Color c)
    {
        currentColor = c;
        meshRenderer.material.color = c;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("banana");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Button pressed!");
        if (other.CompareTag("Player")) // && not intangible
        {
            T3ButtonPuzzleManager.AddButtonToSequence(this);
        }
    }
}
