using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static System.EventHandler<System.EventArgs> onDialogueComplete;


    public static bool isDialogueOpen;
    private DialogueParameters currentParameters;

    public float typeDelay;
    private bool isTyping;
    private bool isTypingComplete;
    private Coroutine typingCoroutine;


    [Header("References")]
    public GameObject dialoguePanel;
    public CanvasGroup dialogueAlpha;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descText;
    public Image image;

    public CanvasGroupAnimator fadeAnimator;


    [System.Serializable]
    public class DialogueParameters
    {
        public string name;
        [TextArea(1, 5)]
        public string desc;
        public Sprite image;
    }


    void Start()
    {
        
    }

    void Update()
    {
        //Debug.Log(IsDialogueOpen() + " " + PauseMenu.IsUIOpen() + " " + Input.GetKeyDown(KeyCode.Tab));
        if (IsDialogueOpen() && !PauseMenu.IsUIOpen() && (Input.GetKeyDown(KeyCode.Tab) || Input.GetButtonDown("Fire2")))
        {
            AdvanceDialogue();
        }
    }


    public static bool IsDialogueOpen()
    {
        return isDialogueOpen;
    }

    public void SetDialogueActive(bool enabled)
    {
        if (enabled)
            EnableDialogue();
        else
            DisableDialogue();
    }

    private void EnableDialogue(System.Action callback=null)
    {
        Debug.Log("active?: " + dialoguePanel.activeInHierarchy);
        dialoguePanel.SetActive(true);
        Debug.Log("active again?: " + dialoguePanel.activeInHierarchy);
        Debug.Log("I am active now: " + gameObject.activeInHierarchy);
        isDialogueOpen = true;
        fadeAnimator.StartFadeIn(callback);
    }

    private void DisableDialogue()
    {
        isDialogueOpen = false;
        fadeAnimator.StartFadeOut(() =>
        {
            if (dialogueAlpha.alpha == 0) dialoguePanel.SetActive(false);
        });
    }


    public void StartDialogue(DialogueParameters parameters)
    {
        SetDialogueActive(true);
        SetDialogue(parameters);

        descText.text = "";
        isTypingComplete = false;

        EnableDialogue(StartTypingText);
    }

    public void SetDialogue(DialogueParameters parameters)
    {
        currentParameters = parameters;
        nameText.text = parameters.name;
        descText.text = parameters.desc;
        image.sprite = parameters.image;
    }

    public void AdvanceDialogue()
    {
        if (!isTypingComplete)
        {
            TypeAllText();
        }
        else
        {
            DisableDialogue();

            onDialogueComplete.Invoke(this, null);
        }
    }

    private void StartTypingText()
    {
        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        isTyping = true;
        string s = "";

        for (int i = 0; i < currentParameters.desc.Length && !isTypingComplete; i++)
        {
            // to properly type, set texts alpha to 0 at start and to 1 char by char
            s += currentParameters.desc[i];
            descText.text = s;

            if (",.?!".Contains(currentParameters.desc[i].ToString()))
            {
                yield return new WaitForSeconds(2 * typeDelay);
            }
            else
            {
                yield return new WaitForSeconds(typeDelay);
            }
        }

        descText.text = currentParameters.desc;
        isTyping = false;
        isTypingComplete = true;
    }

    private void TypeAllText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        isTyping = false;
        isTypingComplete = true;

        descText.text = currentParameters.desc;
    }
    public void debuglogdialogue(DialogueParameters param)
    {
        Debug.Log(param.desc);
    }
}
