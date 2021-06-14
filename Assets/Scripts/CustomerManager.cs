using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager cm;
    public GameObject customer;
    private void Awake()
    {
        if(cm == null)
        {
            cm = this;
        }
    }
}
