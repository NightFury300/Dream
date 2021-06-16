using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager cm;

    [SerializeField]
    public GameObject[] customers;

    [HideInInspector]
    public GameObject activeCustomer;

    private Vector2 wayPoint;
    [SerializeField]
    private Transform exitTo;
    [SerializeField]
    private Transform entryTo;
    [SerializeField]
    private float speed = 5.0f;

    private int customerIndex = 0;

    private bool destinationReached = true;

    [SerializeField]
    private GameObject newCustomerButton;
    
    public BooleanVariable customerDie;

    private void Awake()
    {
        if(cm == null)
        {
            cm = this;
        }
    }

    private void Update()
    {
        if (!destinationReached)
        {
            MoveToWayPoint();
            if (activeCustomer != null)
                activeCustomer.GetComponent<Animator>().SetBool("Kneel", false);
        }
        else
        {
            if(activeCustomer != null)
                 activeCustomer.GetComponent<Animator>().SetBool("Kneel", true);
        }
        
    }

    private void MoveToWayPoint()
    {
        Vector2 currentLoc = activeCustomer.transform.position;
        Vector2 distance =   wayPoint - currentLoc;

        if (!(Mathf.Abs(distance.magnitude) < 0.1f))
        {
            activeCustomer.GetComponent<Rigidbody2D>().velocity = new Vector2(distance.normalized.x * speed,
                                                                        distance.normalized.y * speed);
            if(activeCustomer.GetComponent<Rigidbody2D>().velocity.x > 0.0f)
            {
                activeCustomer.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (activeCustomer.GetComponent<Rigidbody2D>().velocity.x < 0.0f)
            {
                activeCustomer.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else
        {
            activeCustomer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            destinationReached = true;
            if (wayPoint.Equals(entryTo.position))
            {
                activeCustomer.GetComponent<DialogueTrigger>().TriggerAppropriateDialogue(0);
            }
            else
            {
                ReadyForNextCustomer(true);
            }
        }
    }

    public void EnterNewCustomer()
    {
        ReadyForNextCustomer(false);
        if (customerIndex < customers.Length)
        {
            activeCustomer = customers[customerIndex++];
            wayPoint = entryTo.position;
            destinationReached = false;
        }
        else
        {
            //Back To Main Menu or some credits/game over screen maybe?
            Debug.Log("No More Customers!");
        }
    }

    public void SelectNextOrCurrentCustomer()
    {
        if (customerDie.runtimeValue)
            KillCustomer();
        else
            ExitCurrentCustomer();
    }

    public void ExitCurrentCustomer()
    {
        wayPoint = exitTo.position;
        destinationReached = false;
    }

    public void ReadyForNextCustomer(bool ready)
    {
        if(customerIndex < customers.Length)
            newCustomerButton.SetActive(ready);
    }

    public void KillCustomer()
    {
        Debug.Log("Play Kill Animation Here");
        StartCoroutine(WaitBeforeRestart());
    }

    IEnumerator WaitBeforeRestart()
    {
        yield return new WaitForSecondsRealtime(1f);
        RepeatCustomer();
    }

    public void RepeatCustomer()
    {
        activeCustomer.transform.position = exitTo.position;
        customerIndex--;
        customerDie.runtimeValue = false;
        ReadyForNextCustomer(true);
        activeCustomer.GetComponent<DialogueTrigger>().ResetIndices();
    }
}
