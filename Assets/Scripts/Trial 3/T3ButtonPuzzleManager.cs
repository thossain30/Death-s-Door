using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3ButtonPuzzleManager : MonoBehaviour
{
    public Animator cameraAnim;
    private thirdPersonMovement character;

    public static System.EventHandler<System.EventArgs> onPuzzleComplete;
    public static System.EventHandler<System.EventArgs> onPuzzleFail;

    public State state;

    private static T3Button[,] buttonGrid = new T3Button[5, 5];
    [SerializeField]
    private List<int> currentSequence = new List<int>();

    // puzzle stuff
    public T3Puzzle currentPuzzle;
    private List<int> goalSequence = new List<int>();
    public bool canRepeat = false;
    public static bool complete = false;


    //private Coroutine crunchCoroutine;
    public List<T3Puzzle> randomPuzzlePool = new List<T3Puzzle>();
    private bool isCrunching;
    private float crunchDeadline;
    private int crunchTasksComplete;


    public UI_T3DisplayManager displayManager;
    public UI_T3CrunchBoard crunchBoard;

    public AudioSource correctSoundSource;
    public AudioSource incorrectSoundSource;

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
        startCrunch,
    }


    private void Awake()
    {
        _instance = this;
        character = FindObjectOfType<thirdPersonMovement>();
        //SetPuzzle(currentPuzzle);
    }


    private void Update()
    {
        if (cameraAnim != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (cameraAnim.GetBool("IsOnPlayer") == true)
                {
                    cameraAnim.SetBool("IsOnPlayer", false);
                } else if (cameraAnim.GetBool("IsOnPlayer") == false)
                {
                    cameraAnim.SetBool("IsOnPlayer", true);
                }
            }
        }
        if (isCrunching)
        {
            //increases player speed when entering crunch state
            character.speed = 11.5f;
            crunchBoard.SetScore(crunchTasksComplete);
            if (Time.time >= crunchDeadline)
            {
                SetState(State.inactive);
            }
        }
    }

    public void SetState(State s)
    {
        state = s;

        switch (s)
        {
            case State.active:
                //if (complete)
                //{
                //    SceneManager.LoadScene(0);
                //}
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
                    //Debug.Log("chec!");
                    if (goalSequence.Contains(b.buttonID))
                    {
                        //Debug.Log("Setting color!");
                        StartCoroutine(RecursiveSetButtonColor(b, correctColor, activeBlankColor, Random.Range(0.8f, 1.2f), 300));
                    }
                }
                //complete = true;
                break;

            case State.startCrunch:

                //if (crunchCoroutine == null)
                if (!isCrunching)
                {
                    isCrunching = true;
                    crunchDeadline = Time.time + 60;
                    crunchTasksComplete = 0;

                    crunchBoard.StartTimer(crunchDeadline);

                    ResetGrid();

                    SetRandomPuzzle();
                }

                SetState(State.active);

                break;

        }
    }

    public void SetStateStartCrunch() // for unity events
    {
        SetState(State.startCrunch);
    }

    public void ResetGrid()
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

        displayManager.DisplayPuzzle(puzzle);
    }

    private void SetRandomPuzzle()
    {
        int i = Random.Range(0, randomPuzzlePool.Count);
        while (randomPuzzlePool[i] == currentPuzzle)
        {
            i = Random.Range(0, randomPuzzlePool.Count);
        }
        SetPuzzle(randomPuzzlePool[i]);
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
            onPuzzleFail?.Invoke(this, new System.EventArgs());
            incorrectSoundSource.Play();

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
                if (isCrunching)
                {
                    crunchTasksComplete += 1;
                    SetRandomPuzzle();
                }
                else
                {
                    onPuzzleComplete?.Invoke(this, new System.EventArgs());
                    correctSoundSource.Play();
                    SetState(State.party);
                }
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        ResetGrid();
    }
}
