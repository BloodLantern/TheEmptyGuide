using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Information : MonoBehaviour
{
    [SerializeField] public bool isRight;
    private bool isAssumption;
    private bool isTruth;

    [HideInInspector] public string InformationText;
    private TextMeshProUGUI informationTextUI;

    Button trueButton;
    Button falseButton;

    private void Start()
    {
        informationTextUI = GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(informationTextUI.text);
    }
}
