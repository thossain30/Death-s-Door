using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_T3DisplayManager : MonoBehaviour
{
    public UI_T3Display[] displays;
    public int boardSize = 5;

    public GameObject player;
    
    [Header("Display Parameters")]
    public Color correctColor;

    public float rotationDelay;

    private Coroutine orderedCoroutine;
    private Coroutine orderedSubcoroutine;
    public float orderedInitDelay;
    public float orderedDelay;
    public float pingDuration;
    public AnimationCurve pingCurve;

    private void Update()
    {
        Vector3 playerPos = player.transform.position - transform.position;
        float newRotation = 270 - (Mathf.Atan2(playerPos.z, playerPos.x) * Mathf.Rad2Deg);
        Debug.Log(newRotation);
        StartCoroutine(SetRotation(newRotation, rotationDelay));
    }

    private IEnumerator SetRotation(float rotation, float delay)
    {
        yield return new WaitForSeconds(delay);

        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }

    public void DisplayPuzzle(T3Puzzle puzzle)
    {
        switch (puzzle.puzzleType)
        {
            case T3Puzzle.PuzzleType.ordered:
                DisplayOrderedPuzzle(puzzle);
                break;
            case T3Puzzle.PuzzleType.shape:
                DisplayShapePuzzle(puzzle);
                break;
        }
    }

    private void DisplayOrderedPuzzle(T3Puzzle puzzle)
    {
        orderedCoroutine = StartCoroutine(AnimateOrderedPuzzle(puzzle));
    }

    private IEnumerator AnimateOrderedPuzzle(T3Puzzle puzzle)
    {
        orderedSubcoroutine = StartCoroutine(AnimateOrderedPath(puzzle, orderedInitDelay));

        yield return new WaitForSeconds(orderedDelay * 3);

        while (true)
        {
            orderedSubcoroutine = StartCoroutine(AnimateOrderedPath(puzzle, orderedDelay));

            yield return new WaitForSeconds(orderedDelay * 5);
        }
    }

    private IEnumerator AnimateOrderedPath(T3Puzzle puzzle, float dT)
    {
        int currentIndex = 1;
        while (true && currentIndex < boardSize * boardSize)
        {
            int index = Array.IndexOf(puzzle.orderedPuzzle, currentIndex);
            if (index == -1)
                yield break;
            displays[index].Ping(correctColor, pingDuration, pingCurve);

            currentIndex += 1;

            yield return new WaitForSeconds(dT);
        }
    }

    private void DisplayShapePuzzle(T3Puzzle puzzle)
    {
        for (int i = 0; i < boardSize * boardSize; i++)
        {
            if (puzzle.shapePuzzle[i])
            {
                displays[i].SetColor(correctColor);
            }
        }
    }

    public void ResetDisplay()
    {
        StopCoroutine(orderedCoroutine);
        StopCoroutine(orderedSubcoroutine);

        for (int i = 0; i < displays.Length; i++)
        {
            displays[i].ResetColor();
        }
    }
}
