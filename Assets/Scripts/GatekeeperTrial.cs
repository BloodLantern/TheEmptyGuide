using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GatekeeperTrial : MonoBehaviour
{
    public List<Information> Information;
    public List<Information> RightInformation;
    private int numberOfInformation;
    private const int numberOfRightInformation = 4;
    [HideInInspector] public int NumberOfInformationDropped;

    [SerializeField] TextMeshProUGUI leftValue;
    [SerializeField] TextMeshProUGUI rightValue;

    private Guide guide;

    [SerializeField] private Dialogue correctDialogue;

    private void Start()
    {
        foreach (Dialogue dialogue in FindObjectsOfType<Dialogue>())
        {
            foreach (DialogueInfo info in dialogue.RewardInformation)
            {
                if (!info.GatekeeperInformation)
                    numberOfInformation++;
            }
        }
        guide = FindObjectOfType<Guide>();
    }

    private void Update()
    {
        rightValue.text = numberOfInformation.ToString();
        leftValue.text = NumberOfInformationDropped.ToString();
        if (Information.Count == numberOfInformation)
        {
            if (IsValidatedByGatekeeper())
            {
                guide.ToggleGuideDisplay();
                correctDialogue.Display();
            }
            else
            {
                foreach (Information information in Information)
                {
                    information.gameObject.SetActive(true);
                }
                Information.Clear();
            }
        }
    }
    private int CheckInformations()
    {
        int validInfo = 0;
        foreach (Information information in Information)
        {
            if (information.IsTruth == information.IsAssumption)
                validInfo++;
        }
        Debug.Log(validInfo);
        return validInfo;
    }

    private bool IsValidatedByGatekeeper()
    {
        NumberOfInformationDropped = 0;
        if (CheckInformations() == numberOfInformation)
        {
            return true;
        }
        return false;
    }
}
