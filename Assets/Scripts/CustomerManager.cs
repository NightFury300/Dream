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

    /*[SerializeField]
    private GameObject startGame;//To be Removed in Release*/
    
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
                EnterNewCustomer();
            }
        }
    }

    public void EnterNewCustomer()
    {
        //ReadyForNextCustomer(false);
        if (customerIndex < customers.Length)
        {
            activeCustomer = customers[customerIndex++];
            wayPoint = entryTo.position;
            destinationReached = false;
        }
        else
        {
            FindObjectOfType<ChangeScene>().OnPlayButtonClicked();
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

    /*public void ReadyForNextCustomer(bool ready)
    {
        if(customerIndex < customers.Length)
            newCustomerButton.SetActive(ready);
    }*/

    public void KillCustomer()
    {
        if (activeCustomer.name.Equals("Customer1"))
        {
            int type = Random.Range(1, 4);
            activeCustomer.GetComponent<Animator>().SetInteger("DeathType", type);
        }
        else
        {
            activeCustomer.GetComponent<Animator>().SetInteger("DeathType", 1);
        }
        //StartCoroutine(WaitBeforeRestart());
    }
    //Using Animation events instead
    /*IEnumerator WaitBeforeRestart()
    {
        yield return new WaitForSecondsRealtime(1f);
        RepeatCustomer();
    }*/

    public void RepeatCustomer()
    {
        activeCustomer.GetComponent<Animator>().SetInteger("DeathType", 0);
        activeCustomer.transform.position = exitTo.position;
        customerIndex--;
        customerDie.runtimeValue = false;
        //ReadyForNextCustomer(true);
        EnterNewCustomer();
        activeCustomer.GetComponent<DialogueTrigger>().ResetIndices();
    }
}
