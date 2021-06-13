using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> Sentences;

    [SerializeField]
    private Text Name;
    [SerializeField]
    private Text Sentence;

    void Start()
    {
        Sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Sentences.Clear();
        foreach(string sentence in dialogue.Sentences)
        {
            Sentences.Enqueue(sentence);
        }
        Name.text = dialogue.Name;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(Sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = Sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        Sentence.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            Sentence.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        Debug.Log("End of the Conversation.");
    }
}

