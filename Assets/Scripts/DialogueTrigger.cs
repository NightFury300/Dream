using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue correctDialogue;
    public Dialogue wrongDialogue;

    public void TriggerDialogue()
    {
        DialogueManager.dm.StartDialogue(dialogue);
    }

    public void TriggerCorrectDialogue()
    {
        DialogueManager.dm.StartDialogue(correctDialogue);
    }

    public void TriggerWrongDialogue()
    {
        DialogueManager.dm.StartDialogue(wrongDialogue);
    }
}
