using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogues;
    public Dialogue[] correctDialogues;
    public Dialogue[] wrongDialogues;

    int currentDialogueIndex;
    int currentCorrectDialogueIndex;
    int currentWrongDialogueIndex;
    public void TriggerDialogue()
    {
        DialogueManager.dm.StartDialogue(dialogues[currentDialogueIndex]);
        currentDialogueIndex++;
    }

    public void TriggerCorrectDialogue()
    {
        DialogueManager.dm.StartDialogue(correctDialogues[currentCorrectDialogueIndex]);
        currentCorrectDialogueIndex++;
    }

    public void TriggerWrongDialogue()
    {
        DialogueManager.dm.StartDialogue(wrongDialogues[currentWrongDialogueIndex]);
        currentWrongDialogueIndex++;
    }

    public void TriggerAppropriateDialogue(int dialogueType)
    {
        if (dialogueType == 0)
        {
            if (currentDialogueIndex >= dialogues.Length)
            {
                DialogueManager.dm.EndDialogue();
                return;
            }
            else
                TriggerDialogue();
        }
        else if (dialogueType == 1)
        {
            if (currentCorrectDialogueIndex >= correctDialogues.Length)
            {
                DialogueManager.dm.EndDialogue();
                return;
            }
            else
                TriggerCorrectDialogue();
        }
        else if (dialogueType == 2)
        {
            if (currentWrongDialogueIndex >= wrongDialogues.Length)
            {
                DialogueManager.dm.EndDialogue();
                return;
            }
            else
                TriggerWrongDialogue();
        }
    }
}
