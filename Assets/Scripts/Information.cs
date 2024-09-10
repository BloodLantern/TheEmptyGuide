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

    private void Start()
    {
        informationTextUI = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetTextUI()
    {
        informationTextUI.text = InformationText;
    }
    public void OnGreenButtonClick()
    {
        isAssumption = true;
    }
    public void OnRedButtonClick()
    {
        isAssumption = false;
    }
}
