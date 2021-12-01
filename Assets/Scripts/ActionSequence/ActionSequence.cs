using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionSequence : MonoBehaviour
{
    public List<ASAction> sequence = new List<ASAction>();
    private int sequenceIndex;
    private Coroutine delayedAction;
    void Start()
    {
        CheckSequence();
    }

    private void OnEnable()
    {
        GroundTile.onExitTile += OnExitRunnerTile;
        spawnTile.enterSpawn += onEnterSpawn;
        T1artifact.onConclusion += onConclusion;
        T2artifact.onConclusion += onConclusion;
        MazeCheckpoint.onCheckpoint += onCheckpoint;
        T3ButtonPuzzleManager.onPuzzleComplete += OnPuzzleComplete;
        DialogueManager.onDialogueComplete += OnDialogueComplete;
    }

    private void OnDisable()
    {
        GroundTile.onExitTile -= OnExitRunnerTile;
        spawnTile.enterSpawn -= onEnterSpawn;
        T1artifact.onConclusion -= onConclusion;
        T2artifact.onConclusion -= onConclusion;
        MazeCheckpoint.onCheckpoint -= onCheckpoint;
        T3ButtonPuzzleManager.onPuzzleComplete -= OnPuzzleComplete;
        DialogueManager.onDialogueComplete -= OnDialogueComplete;
    }

    void Update()
    {

    }
    private void CheckSequence()
    {
        if (sequenceIndex >= sequence.Count)
        {
            //Debug.Log("Sequence complete!");
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
        delayedAction = null;
        CheckSequence();
    }

    private void OnExitRunnerTile(object sender, GroundTile.RunnerTileArg e)
    {
        if (sequenceIndex < sequence.Count && sequence[sequenceIndex].trigger == ASAction.Trigger.OnExitRunnerTile)
        {
            if (e.tileCount >= sequence[sequenceIndex].atLeastNumTiles)
            {
                InvokeASAction(sequence[sequenceIndex]);
                sequenceIndex += 1;
                CheckSequence();
            }
        }
    }
    private void onConclusion(object sender, System.EventArgs e)
    {
        if (sequenceIndex < sequence.Count && sequence[sequenceIndex].trigger == ASAction.Trigger.onConclusion)
        {
            InvokeASAction(sequence[sequenceIndex]);
            sequenceIndex += 1;
            CheckSequence();
        }
    }
    private void onEnterSpawn(object sender, System.EventArgs e)
    {
        if (sequenceIndex < sequence.Count && sequence[sequenceIndex].trigger == ASAction.Trigger.onEnterSpawn)
        {
            InvokeASAction(sequence[sequenceIndex]);
            sequenceIndex += 1;
            CheckSequence();
        }
    }
    private void onCheckpoint(object sender, System.EventArgs e)
    {
        if (sequenceIndex < sequence.Count && sequence[sequenceIndex].trigger == ASAction.Trigger.OnCheckpoint)
        {
            InvokeASAction(sequence[sequenceIndex]);
            sequenceIndex += 1;
            CheckSequence();
        }
    }

    private void OnPuzzleComplete(object sender, System.EventArgs e)
    {
        if (sequence[sequenceIndex].trigger == ASAction.Trigger.OnPuzzleComplete) 
        {
            Debug.Log("Puzzle finished!");
            InvokeASAction(sequence[sequenceIndex]);
            sequenceIndex += 1;
            CheckSequence();
        }
    }

    private void OnDialogueComplete(object sender, System.EventArgs e)
    {
        if (sequenceIndex < sequence.Count && sequence[sequenceIndex].trigger == ASAction.Trigger.OnDialogueComplete)
        {
            InvokeASAction(sequence[sequenceIndex]);
            sequenceIndex += 1;
            CheckSequence();
        }
    }
    public void CompleteTrial()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void DebugLogString(string s)
    {
        Debug.Log(s);
    }
}
