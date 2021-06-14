using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 *WARNING:
 *Whenever creating a dialogue with no options just disable the Enable Options Checkbox.DO NOT change the Option Array
 *Size to 0, It should always remain 3 even when not using options.
 */

/*
 * Things to Do:
 * Each Customer will have entry dialogues,dialogues if they are healed or dialogues for their final words before
 * their demise and these conditional dialogues should be triggered preferably in OnCorrectAnswer(),OnWrongAnswer()
 * functions.I am not sure whats the best way to do it but here are some ugly ideas-
 * 
 * 1.We can just create two empty objects named customer1healed and customer1dead with dialogue trigger 
 * scripts attached to them and reference them in this class but that way we will need to have two extra GameObject
 * variable (for each customer) in here and this is very ugly if we are dealing with more customers.
 * 
 * 2.We implement conditional Dialogues in Dialogue Script(This is a very good approach) but I am not sure how 
 * to do that because I tested and apprently Inspector isn't detecting and letting us add two members of type
 * Dialogue in class Dialogue.But if some way this idea could be implemented it would be Neat.
 * 
 * 3.If you have a better idea you should definitely go with it,the above ideas arent that great anyways those were
 * the ideas that came first to my mind and thanks for reading this.
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
    private GameObject CorrectDialogue;

    [SerializeField]
    private GameObject WrongDialogue;

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
    }

    public void Option1Clicked()
    {
        if (CorrectOption == 1)
        {
            OnCorrectAnswer();
        }
        else
        {
            OnWrongAnswer();
        }
    }

    public void Option2Clicked()
    {
        if (CorrectOption == 2)
        {
            OnCorrectAnswer();
        }
        else
        {
            OnWrongAnswer();
        }
    }

    public void Option3Clicked()
    {
        if (CorrectOption == 3)
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
        CorrectDialogue.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    private void OnWrongAnswer()
    {
        EndDialogue();
        WrongDialogue.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    public void PlayActiveCustomerDialogue()
    {
        var activeCustomer = CustomerManager.cm.customer;
        var diaogueTrigger = activeCustomer.GetComponent<DialogueTrigger>();
        if (diaogueTrigger != null)
            diaogueTrigger.TriggerDialogue();
    }
}

