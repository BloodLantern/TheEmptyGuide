using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GatekeeperTrial : MonoBehaviour
{
    public List<Information> information;
    private int numberOfInformation;

    [SerializeField] TextMeshProUGUI leftValue;
    [SerializeField] TextMeshProUGUI rightValue;

    private Guide guide;

    [SerializeField]
    private Dialogue correctDialogue;

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

        gameObject.SetActive(false);
    }

    private void Update()
    {
        rightValue.text = numberOfInformation.ToString();
        if (information.Count == numberOfInformation)
        {
            Debug.Log(IsValidatedByGatekeeper());
            if (IsValidatedByGatekeeper())
            {
                guide.ToggleGuideDisplay();
                correctDialogue.Display();
            }
            else
            {
                foreach (Information information in information)
                {
                    information.gameObject.SetActive(true);
                }
                information.Clear();
            }
        }
    }
    private int CheckInformations()
    {
        int validInfo = 0;
        foreach (Information information in information)
        {
            if (information.IsTruth == information.IsAssumption)
                validInfo++;
        }
        return validInfo;
    }

    private bool IsValidatedByGatekeeper()
    {
        if (CheckInformations() == numberOfInformation)
            return true;
        return false;
    }
}
