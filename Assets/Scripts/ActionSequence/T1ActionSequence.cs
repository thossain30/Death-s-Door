using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class T1ActionSequence : MonoBehaviour
{
    public List<ASAction> sequence = new List<ASAction>();
    public spawnTile spawn;
    private int sequenceIndex;

    private Coroutine delayedAction;

    void Start()
    {
        CheckSequence();
    }

    private void OnEnable()
    {
        spawnTile.onRunning += OnRunning;
        DialogueManager.onDialogueComplete += OnDialogueComplete;
    }

    private void OnDisable()
    {
        spawnTile.onRunning -= OnRunning;
        DialogueManager.onDialogueComplete -= OnDialogueComplete;
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
            case ASAction.Trigger.None:
                InvokeASAction(sequence[sequenceIndex]);
                sequenceIndex += 1;
                CheckSequence();
                break;
            case ASAction.Trigger.Delay:
                if (delayedAction == null)
                {
                    float delay = sequence[sequenceIndex].delayDuration;
                    delayedAction = StartCoroutine(InvokeEventAfterDelay(delay));
                }
                break;
            case ASAction.Trigger.OffSpawn:
                InvokeASAction(sequence[sequenceIndex]);
                sequenceIndex += 1;
                CheckSequence();
                break;
        }
    }

    private void InvokeASAction(ASAction action)
    {
        if (action.onTrigger != null)
            action.onTrigger.Invoke();
        if (action.onTriggerDialogue != null)
            action.onTriggerDialogue.Invoke(action.dialogueParams);
    }

    private IEnumerator InvokeEventAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        InvokeASAction(sequence[sequenceIndex]);
        sequenceIndex += 1;
        CheckSequence();

        delayedAction = null;
    }


    private void OnDialogueComplete(object sender, System.EventArgs e)
    {
        if (sequence[sequenceIndex].trigger == ASAction.Trigger.OnDialogueComplete)
        {
            InvokeASAction(sequence[sequenceIndex]);
            sequenceIndex += 1;
            CheckSequence();
        }
    }
    private void OnRunning(object sender, System.EventArgs e)
    {
        if (sequence[sequenceIndex].trigger == ASAction.Trigger.OffSpawn)
        {
            Debug.Log("No longer on spawn!");
            InvokeASAction(sequence[sequenceIndex]);
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
