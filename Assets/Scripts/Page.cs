using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    [SerializeField] UInt16 numberOfInformations;
    private Information[] informations;

    private void Start()
    {
        informations = new Information[numberOfInformations];
    }
    public void DisplayInformations()
    {
        if (informations == null)
        {
            // display informations
        }
    }
}
