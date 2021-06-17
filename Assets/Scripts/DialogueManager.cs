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
    private Queue<string> sentences;

    [SerializeField]
    private Text name;
    [SerializeField]
    private Text sentence;

    [SerializeField]
    private Text option1;
    [SerializeField]
    private Text option2;
    [SerializeField]
    private Text option3;

    public Animator animator;

    public GameObject[] options = new GameObject[3];

    private bool enableOptions = false;

    [SerializeField]
    private GameObject nextButton;

    private int correctOption;

    public static DialogueManager dm;

    private void Awake()
    {
        if (dm == null)
        {
            dm = this;
        }
    }

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        FindObjectOfType<AudioManager>().Play("DialoguePop");
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        animator.SetBool("IsDialogueOpen", true);
        name.text = dialogue.heading;        
        option1.text = dialogue.options[0];
        option2.text = dialogue.options[1];
        option3.text = dialogue.options[2];
        enableOptions = dialogue.enableOptions;
        correctOption = dialogue.correctOption;
        if (sentences.Count != 0)
            nextButton.SetActive(true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            if (enableOptions)
            {
                nextButton.SetActive(false);
                DisplayOptions();
            }
            else
            {
                if (!CustomerManager.cm.customerDie.runtimeValue)
                    CustomerManager.cm.activeCustomer.GetComponent<DialogueTrigger>().TriggerAppropriateDialogue(0);
                else
                    EndDialogue();
            }
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayOptions(bool display = true)
    {
        for(int i = 0; i < 3; i++)
        {
            options[i].SetActive(display);
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        this.sentence.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            this.sentence.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsDialogueOpen", false);
        nextButton.SetActive(true);

        DisplayOptions(false);
        CustomerManager.cm.SelectNextOrCurrentCustomer();
    }

    public void OptionClicked(int option)
    {
        //(correctOption == option) ? OnCorrectAnswer() : OnWrongAnswer();
        DisplayOptions(false);
        if (correctOption == option)
        {
            OnCorrectAnswer();
        }
        else
        {
            OnWrongAnswer(option);
        }
    }

    private void OnCorrectAnswer()
    {
        FindObjectOfType<AudioManager>().Play("Correct");
        CustomerManager.cm.activeCustomer.GetComponent<DialogueTrigger>().TriggerAppropriateDialogue(1);
    }

    private void OnWrongAnswer(int option)
    {
        FindObjectOfType<AudioManager>().Play("Wrong");
        int i = 1;
        for(; i <=3;i++)
        {
            if(!(correctOption == i))
            {
                break;
            }
        }
        if (i == option)
        {
            CustomerManager.cm.activeCustomer.GetComponent<DialogueTrigger>().TriggerAppropriateDialogue(2);
        }
        else
        {
            CustomerManager.cm.activeCustomer.GetComponent<DialogueTrigger>().TriggerAppropriateDialogue(3);
        }
        CustomerManager.cm.customerDie.runtimeValue = true;
    }
}

