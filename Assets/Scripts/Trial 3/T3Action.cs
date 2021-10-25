using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class T3Action
{
    public enum Trigger
    {
        None,
        Delay,
        OnPuzzleComplete,
    }

    public Trigger trigger;
    public float delayDuration;

    public UnityEvent onTrigger;
}
