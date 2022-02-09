using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CutsceneDialogue : MonoBehaviour
{
    [Header("Dialogue Text Variables")]
    public TMP_Text dialogueText;
    public int currentLine;
    public DialogueManager dialogue;
    public bool isTyping;
    public float timeBetweenLetters;

    [Header("Dialogue Box Variables")]
    public GameObject dialogueBox;
    public GameObject[] dialogueBoxes;
    public int currentDialogueBox;
    public bool finishedDialogue;

    [Header("Skip Variables")]
    public GameObject skipButton;

    void Start()
    {
        dialogue = FindObjectOfType<DialogueManager>();
        currentDialogueBox = 0;
        currentLine = 0;
    }

    void Update()
    {
        if (!finishedDialogue)
        {
            //Updating What the Current Text Box Is
            if (currentDialogueBox < 0)
                return;

            if (currentDialogueBox > dialogueBoxes.Length - 1)
                currentDialogueBox = dialogueBoxes.Length - 1;

            if (CutsceneController.instance.dialogueHasStarted)
            {
                if (!dialogueBox.activeInHierarchy)
                {
                    StartDialogue();

                    skipButton.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(skipButton);

                    Debug.Log("HasStarted");
                }
            }

            if (dialogueBox.activeInHierarchy)
            {
                if (Input.GetButtonDown("Continue") && !isTyping)
                {
                    if (currentDialogueBox >= 0 && currentDialogueBox < dialogueBoxes.Length - 1)
                    {
                        ContinueDialogue();
                        Debug.Log("Dialogue Working");

                        dialogueBoxes[currentDialogueBox].SetActive(false);
                        currentDialogueBox++;
                        dialogueBoxes[currentDialogueBox].SetActive(true);
                    }
                    else
                    {
                        EndDialogue();
                    }
                }
            }
        }
    }

    public void StartDialogue()
    {
        dialogueBox.SetActive(true);
        if (currentLine == 0)
        {
            StartCoroutine(ShowDialogueCO(dialogue));
        }
    }

    public IEnumerator ShowDialogueCO(DialogueManager dialog)
    {
        this.dialogue = dialog;
        yield return new WaitForEndOfFrame();
        StartCoroutine(TypeDialogueCO(dialog.Lines[0]));
    }

    public void ContinueDialogue()
    {
        ++currentLine;

        if (currentLine < dialogue.Lines.Count)
        {
            StartCoroutine(TypeDialogueCO(dialogue.Lines[currentLine]));
        }
    }

    public IEnumerator TypeDialogueCO(string lineToType)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (var letter in lineToType.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
        isTyping = false;
    }

    public void EndDialogue()
    {
        finishedDialogue = true;
        AudioManager.instance.PlaySFX(0);
        skipButton.SetActive(false);
        dialogueBox.SetActive(false);
    }
}
