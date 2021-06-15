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

    private Vector2 WayPoint;
    [SerializeField]
    private Vector2 ExitTo;
    [SerializeField]
    private Vector2 EntryTo;
    [SerializeField]
    private float speed = 5.0f;

    private int customerIndex = 0;

    private bool destinationReached = true;

    [SerializeField]
    private GameObject NewCustomerButton;
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
        Vector2 distance =   WayPoint - currentLoc;

        if (!(Mathf.Abs(distance.magnitude) < 2f))
        {
            activeCustomer.GetComponent<Rigidbody2D>().velocity = new Vector2(distance.normalized.x * speed,
                                                                        distance.normalized.y * speed);
        }
        else
        {
            activeCustomer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            destinationReached = true;
            if (WayPoint.Equals(EntryTo))
            {
                activeCustomer.GetComponent<DialogueTrigger>().TriggerDialogue();
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
            WayPoint = EntryTo;
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
        WayPoint = ExitTo;
        destinationReached = false;
    }

    public void ReadyForNextCustomer(bool ready)
    {
        NewCustomerButton.SetActive(ready);
    }
}
