using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Trial 3/Puzzle"), System.Serializable]
public class T3Puzzle : ScriptableObject
{
    public enum PuzzleType
    {
        ordered,
        shape,
    }

    public const int boardSize = 5;

    public PuzzleType puzzleType;

    public int[] orderedPuzzle = new int[boardSize * boardSize];

    public bool[] shapePuzzle = new bool[boardSize * boardSize];
}
