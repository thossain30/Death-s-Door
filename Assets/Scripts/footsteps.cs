using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;
    private AudioSource source;
    // Start is called before the first frame update
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void leftStep()
    {
        AudioClip step1 = clips[0];
        source.PlayOneShot(step1);
    }
    private void rightStep()
    {
        AudioClip step2 = clips[1];
        source.PlayOneShot(step2);
    }
}
