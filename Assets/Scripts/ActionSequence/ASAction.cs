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
        OnPuzzleComplete,
        OnDialogueComplete,
    }

    [System.Serializable]
    public class DialogueEvent : UnityEvent<DialogueManager.DialogueParameters> { }

    public Trigger trigger;

    [Header("Delay")]
    public float delayDuration;

    [Header("Dialogue")]
    public DialogueManager.DialogueParameters dialogueParams;

    [Header("Events")]
    public UnityEvent onTrigger;
    public DialogueEvent onTriggerDialogue;
}
