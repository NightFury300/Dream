using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 *WARNING:
 *Whenever creating a dialogue with no options just disable the Enable Options Checkbox.DO NOT change the Option Array
 *Size to 0, It should always remain 3 even when not using options.
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

    private int CorrectOption;

    [SerializeField]
    private GameObject correctDeafultDialogue;

    [SerializeField]
    private GameObject wrongDefaultDialogue;

    public static DialogueManager dm;

    private bool correctDefaultTrigger;
    private bool wrongDefaultTrigger;

    private void Awake()
    {
        if (dm == null)
        {
            dm = this;
        }
    }

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
        CorrectOption = dialogue.CorrectOption;
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
        nextButton.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            options[i].SetActive(false);
        }
        if(correctDefaultTrigger)
        {
            correctDeafultDialogue.GetComponent<DialogueTrigger>().TriggerDialogue();
            correctDefaultTrigger = false;
        }
        if (wrongDefaultTrigger)
        {
            wrongDefaultDialogue.GetComponent<DialogueTrigger>().TriggerDialogue();
            wrongDefaultTrigger = false;
        }
    }

    public void OptionClicked(int option)
    {
        if (CorrectOption == option)
        {
            OnCorrectAnswer();
        }
        else
        {
            OnWrongAnswer();
        }
    }

    private void OnCorrectAnswer()
    {
        EndDialogue();
        correctDefaultTrigger = true;
        CustomerManager.cm.customer.GetComponent<DialogueTrigger>().TriggerCorrectDialogue();
    }

    private void OnWrongAnswer()
    {
        EndDialogue();
        wrongDefaultTrigger = true;
        CustomerManager.cm.customer.GetComponent<DialogueTrigger>().TriggerWrongDialogue();
    }

    public void PlayActiveCustomerDialogue()
    {
        var activeCustomer = CustomerManager.cm.customer;
        var diaogueTrigger = activeCustomer.GetComponent<DialogueTrigger>();
        if (diaogueTrigger != null)
            diaogueTrigger.TriggerDialogue();
    }
}

