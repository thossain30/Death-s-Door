using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3ButtonGridManager : MonoBehaviour
{
    private static T3Button[,] buttonGrid;

    private static T3ButtonGridManager _instance;

    private void Awake()
    {
        _instance = this;
    }


    public static void AddT3ButtonToGrid(T3Button button)
    {
        buttonGrid[button.x, button.y] = button;
    }
}
