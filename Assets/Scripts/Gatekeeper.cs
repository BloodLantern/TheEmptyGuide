using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gatekeeper : MonoBehaviour
{
    private Information[] informations;
    [SerializeField] int numberOfInformation;

    [SerializeField] TextMeshProUGUI leftValue;
    [SerializeField] TextMeshProUGUI rightValue;

    private void Update()
    {
        rightValue.text = numberOfInformation.ToString();
    }
    private int CheckInformations()
    {
        int validInfo = 0;
        foreach (Information information in informations)
        {
            if (information.isTruth == information.isAssumption)
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
