using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogues;
    public Dialogue[] correctDialogues;
    public Dialogue[] wrongDialogues1;
    public Dialogue[] wrongDialogues2;

    int currentDialogueIndex;
    int currentCorrectDialogueIndex;
    int currentWrongDialogueIndex1;
    int currentWrongDialogueIndex2;
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

    public void TriggerWrongDialogue(int option)
    {
        switch (option)
        {
            case 1:
                DialogueManager.dm.StartDialogue(wrongDialogues1[currentWrongDialogueIndex1]);
                currentWrongDialogueIndex1++;
                break;
            case 2:
                DialogueManager.dm.StartDialogue(wrongDialogues2[currentWrongDialogueIndex2]);
                currentWrongDialogueIndex2++;
                break;
        }
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
            if (currentWrongDialogueIndex1 >= wrongDialogues1.Length)
            {
                DialogueManager.dm.EndDialogue();
                return;
            }
            else
            {
                TriggerWrongDialogue(1);
            }
        }
        else if (dialogueType == 3)
        {
            if (currentWrongDialogueIndex1 >= wrongDialogues1.Length)
            {
                DialogueManager.dm.EndDialogue();
                return;
            }
            else
            {
                TriggerWrongDialogue(2);
            }

        }
    }

        public void ResetIndices()
        {
            currentDialogueIndex = 0;
            currentCorrectDialogueIndex = 0;
            currentWrongDialogueIndex1 = 0;
            currentWrongDialogueIndex2 = 0;
        }

        //Writing this here because no other way to use AnimationEvent which is better than using corutine to respawn
        public void Respawn()
        {
            FindObjectOfType<CustomerManager>().RepeatCustomer();
        }
    }

