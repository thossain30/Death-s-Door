using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static System.EventHandler<System.EventArgs> onDialogueComplete;


    private DialogueParameters currentParameters;

    public float typeDelay;
    private bool isTyping;
    private Coroutine typingCoroutine;


    [Header("References")]
    public GameObject dialoguePanel;
    public CanvasGroup dialogueAlpha;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descText;
    public Image image;


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
        if (Input.GetButtonDown("Fire1"))
        {
            AdvanceDialogue();
        }
    }

    public void SetDialogueActive(bool enabled)
    {
        dialoguePanel.SetActive(enabled);
    }

    public void StartDialogue(DialogueParameters parameters)
    {
        SetDialogueActive(true);
        SetDialogue(parameters);
        typingCoroutine = StartCoroutine(TypeText());
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
        if (isTyping)
        {
            TypeAllText();
        }
        else
        {
            SetDialogueActive(false);

            onDialogueComplete.Invoke(this, null);
        }
    }

    private IEnumerator TypeText()
    {
        isTyping = true;
        string s = "";

        for (int i = 0; i < currentParameters.desc.Length; i++)
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
    }

    private void TypeAllText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        isTyping = false;

        descText.text = currentParameters.desc;
    }
}
