using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class T3ActionSequence : MonoBehaviour
{
    public List<T3Action> sequence = new List<T3Action>();
    private int sequenceIndex;

    private Coroutine delayedAction;

    void Start()
    {
        CheckSequence();
    }

    private void OnEnable()
    {
        T3ButtonPuzzleManager.onPuzzleComplete += OnPuzzleComplete;
    }

    private void OnDisable()
    {
        T3ButtonPuzzleManager.onPuzzleComplete -= OnPuzzleComplete;
    }

    void Update()
    {
        
    }

    private void CheckSequence()
    {
        if (sequenceIndex >= sequence.Count)
        {
            Debug.Log("Sequence complete!");
            return;
        }

        switch (sequence[sequenceIndex].trigger)
        {
            case T3Action.Trigger.None:
                sequence[sequenceIndex].onTrigger.Invoke();
                sequenceIndex += 1;
                CheckSequence();
                break;
            case T3Action.Trigger.Delay:
                if (delayedAction == null)
                {
                    float delay = sequence[sequenceIndex].delayDuration;
                    delayedAction = StartCoroutine(InvokeEventAfterDelay(delay));
                }
                break;
        }
    }

    private IEnumerator InvokeEventAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        sequence[sequenceIndex].onTrigger.Invoke();
        sequenceIndex += 1;
        CheckSequence();

        delayedAction = null;
    }

    private void OnPuzzleComplete(object sender, System.EventArgs e)
    {
        if (sequence[sequenceIndex].trigger == T3Action.Trigger.OnPuzzleComplete)
        {
            sequence[sequenceIndex].onTrigger.Invoke();
            sequenceIndex += 1;
            CheckSequence();
        }
    }


    public void CompleteTrial()
    {
        SceneManager.LoadScene(0);
    }

    public void DebugLogString(string s)
    {
        Debug.Log(s);
    }
}
