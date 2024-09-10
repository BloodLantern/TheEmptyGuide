using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Information : MonoBehaviour
{
    private bool isAssumption;
    private bool isTruth;

    [SerializeField] string informationText;
    private TextMeshPro informationTextUI;

    Button trueButton;
    Button falseButton;


    private void Start()
    {
        //informationTextUI = new TextMeshPro("test");
        //if (information.Length == 0)
            // information = current dialogue string
    }
}
