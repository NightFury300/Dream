using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 *When Checking Options Make Sure to End Dialogue after option is chosen and access a preset dialogue based on
 *correct/wrong answer.Make Sure to Enable the next Button.
 */
public class DialogueManager : MonoBehaviour
{
    private Queue<string> Sentences;

    [SerializeField]
    private Text Name;
    [SerializeField]
    private Text Sentence;

    [SerializeField]
    private Text Option1;
    [SerializeField]
    private Text Option2;
    [SerializeField]
    private Text Option3;

    public Animator animator;

    public GameObject[] options = new GameObject[3];

    private bool enableOptions = false;

    [SerializeField]
    private GameObject nextButton;

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
        animator.SetBool("IsDialogueOpen", true);
        Name.text = dialogue.Name;        
        Option1.text = dialogue.Option[0];
        Option2.text = dialogue.Option[1];
        Option3.text = dialogue.Option[2];
        enableOptions = dialogue.EnableOptions;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(Sentences.Count == 0)
        {
            if (enableOptions)
            {
                nextButton.SetActive(false);
                DisplayOptions();
            }
            else
            {
                EndDialogue();              
            }
            return;
        }
        string sentence = Sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayOptions()
    {
        for(int i = 0;i<3;i++)
        {
            options[i].SetActive(true);
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        Sentence.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            Sentence.text += letter;
            yield return new WaitForSeconds(0.06f);
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsDialogueOpen", false);
    }
}

