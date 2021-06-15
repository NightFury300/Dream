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
    private Vector2 exitFrom;
    [SerializeField]
    private Vector2 entryTo;
    [SerializeField]
    private float speed = 5.0f;

    private int customerIndex = 0;

    private bool destinationReached = true;

    [SerializeField]
    private GameObject newCustomerButton;
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
        }
        else
        {
            activeCustomer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            destinationReached = true;
            if (wayPoint.Equals(entryTo))
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
            wayPoint = entryTo;
            destinationReached = false;
        }
        else
        {
            //Back To Main Menu or some credits/game over screen maybe?
            Debug.Log("No More Customers!");
        }
    }

    public void ExitCurrentCustomer()
    {
        wayPoint = exitFrom;
        destinationReached = false;
    }

    public void ReadyForNextCustomer(bool ready)
    {
        if(customerIndex < customers.Length)
            newCustomerButton.SetActive(ready);
    }
}
