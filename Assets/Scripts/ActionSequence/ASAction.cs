using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ASAction
{
    public enum Trigger
    {
        None,
        Delay,
        OnExitRunnerTile,
        OnPuzzleComplete,
        onConclusion,
        OnDialogueComplete,
    }
    [System.Serializable]
    public class DialogueEvent : UnityEvent<DialogueManager.DialogueParameters> { }

    public Trigger trigger;

    [Header("Delay")]
    [Header("On Delay")]
    public float delayDuration;

    [Header("On Runner")]
    public float atLeastNumTiles;

    [Header("Dialogue")]
    public DialogueManager.DialogueParameters dialogueParams;

    [Header("Events")]
    public UnityEvent onTrigger;
    public DialogueEvent onTriggerDialogue;
}
