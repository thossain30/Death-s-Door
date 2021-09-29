using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3ButtonPuzzleManager : MonoBehaviour
{
    public State state;

    private static T3Button[,] buttonGrid = new T3Button[5, 5];
    [SerializeField]
    private List<int> currentSequence = new List<int>();

    // puzzle stuff
    public T3Puzzle currentPuzzle;
    private List<int> goalSequence = new List<int>();
    public bool canRepeat = false;


    // colors set in editor
    public Color inactiveColor;
    public Color activeBlankColor;
    public Color correctColor;
    public Color incorrectColor;
    public Color intangibleTouchColor;

    private static T3ButtonPuzzleManager _instance;

    public enum State
    {
        inactive,
        active,
        resetting,
        party,
    }


    private void Awake()
    {
        _instance = this;

        SetPuzzle(currentPuzzle);
    }


    public void SetState(State s)
    {
        switch (s)
        {
            case State.active:
                break;

            case State.inactive:
                break;

            case State.resetting:
                foreach (T3Button b in buttonGrid)
                {
                    Color c = b.GetColor();
                    if (c != correctColor && c != incorrectColor)
                    {
                        b.SetColor(inactiveColor);
                    }
                }

                break;

            case State.party:
                Debug.Log(buttonGrid.Length);
                foreach (T3Button b in buttonGrid)
                {
                    Debug.Log("chec!");
                    if (goalSequence.Contains(b.buttonID))
                    {
                        Debug.Log("Setting color!");
                        StartCoroutine(RecursiveSetButtonColor(b, correctColor, activeBlankColor, Random.Range(0.8f, 1.2f), 300));
                    }
                }
                break;

        }
        state = s;
    }

    private void ResetGrid()
    {
        SetState(State.active);

        currentSequence.Clear();

        StopAllCoroutines();
        foreach (T3Button b in buttonGrid)
        {
            b.SetColor(activeBlankColor);
        }
    }

    public void SetPuzzle(T3Puzzle puzzle)
    {
        currentPuzzle = puzzle;
        goalSequence.Clear();

        if (puzzle == null)
        {
            Debug.LogWarning("Tried setting current puzzle to null!");
        }

        if (currentPuzzle.puzzleType == T3Puzzle.PuzzleType.ordered)
        {
            Dictionary<int, int> index_to_id = new Dictionary<int, int>();

            for (int j = 0; j < Mathf.Pow(T3Puzzle.boardSize, 2); j++) 
            {
                index_to_id[currentPuzzle.orderedPuzzle[j]] = buttonGrid[j % 5, j / 5].buttonID;
            }

            int i = 1;
            while (index_to_id.ContainsKey(i))
            {
                goalSequence.Add(index_to_id[i]);
                i += 1;
            }
        }

        else if (currentPuzzle.puzzleType == T3Puzzle.PuzzleType.shape)
        {
            for (int i = 0; i < Mathf.Pow(T3Puzzle.boardSize, 2); i++)
            {
                if (currentPuzzle.shapePuzzle[i])
                {
                    goalSequence.Add(i);
                }
            }
        }
    }

    private bool CheckLegalState()
    {
        if (currentPuzzle == null)
        {
            return false;
        }

        if (currentSequence.Count > goalSequence.Count)
        {
            return false;
        }

        if (currentPuzzle.puzzleType == T3Puzzle.PuzzleType.ordered)
        {
            for (int i = 0; i < currentSequence.Count; i++)
            {
                if (goalSequence[i] != currentSequence[i])
                {
                    return false;
                }
            }
        }

        if (currentPuzzle.puzzleType == T3Puzzle.PuzzleType.shape)
        {
            for (int i = 0; i < currentSequence.Count; i++)
            {
                if (!goalSequence.Contains(currentSequence[i]))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool CheckGoalState()
    {
        if (currentPuzzle == null)
        {
            return false;
        }

        if (currentPuzzle.puzzleType == T3Puzzle.PuzzleType.ordered)
        {
            if (currentSequence.Count != goalSequence.Count)
            {
                return false;
            }

            for (int i = 0; i < goalSequence.Count; i++)
            {
                if (currentSequence[i] != goalSequence[i])
                {
                    return false;
                }
            }

            return true;
        }

        else if (currentPuzzle.puzzleType == T3Puzzle.PuzzleType.shape)
        {
            if (currentSequence.Count != goalSequence.Count)
            {
                return false;
            }

            for (int i = 0; i < goalSequence.Count; i++)
            {
                if (!goalSequence.Contains(currentSequence[i]))
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }

    private IEnumerator RecursiveSetButtonColor(T3Button button, Color c_odd, Color c_even, float delay, int n)
    {
        yield return new WaitForSeconds(delay);

        if (n % 2 == 0)
            button.SetColor(c_even);
        else
            button.SetColor(c_odd);

        if (n > 0)
            StartCoroutine(RecursiveSetButtonColor(button, c_odd, c_even, delay, n - 1));
    }



    public static void AddT3ButtonToGrid(T3Button button)
    {
        buttonGrid[button.x, button.y] = button;
    }

    public static void AddButtonToSequence(T3Button button)
    {
        _instance._AddButtonToSequence(button);
    }

    private void _AddButtonToSequence(T3Button button)
    {
        if (state != State.active)
        {
            return;
        }

        if (!canRepeat && currentSequence.Contains(button.buttonID))
        {
            // Stepped on already pressed button
            SetState(State.resetting);

            button.SetColor(incorrectColor);

            return;
        }

        currentSequence.Add(button.buttonID);

        if (!CheckLegalState())
        {
            // if illegal, set red

            SetState(State.resetting);

            button.SetColor(incorrectColor);

            return;
        }
        else
        {
            button.SetColor(correctColor);

            // check if matches goal
            if (CheckGoalState())
            {
                SetState(State.party);
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        ResetGrid();
    }
}
