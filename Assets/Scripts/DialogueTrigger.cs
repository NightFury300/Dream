using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogues;
    public Dialogue[] correctDialogues;
    public Dialogue[] wrongDialogues;

    public void TriggerDialogue()
    {
        DialogueManager.dm.StartDialogue(dialogues[0]);
    }

    public void TriggerCorrectDialogue()
    {
        DialogueManager.dm.StartDialogue(correctDialogues[0]);
    }

    public void TriggerWrongDialogue()
    {
        DialogueManager.dm.StartDialogue(wrongDialogues[0]);
    }
}
