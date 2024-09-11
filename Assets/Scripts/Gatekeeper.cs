using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Gatekeeper : MonoBehaviour
{
    public List<Information> informations;
    [SerializeField] public int numberOfInformation;

    [SerializeField] TextMeshProUGUI leftValue;
    [SerializeField] TextMeshProUGUI rightValue;

    private void Update()
    {
        rightValue.text = numberOfInformation.ToString();
        if (informations.Count == numberOfInformation)
        {
            Debug.Log(IsValidatedByGatekeeper());
            if (IsValidatedByGatekeeper())
            {
                // TODO dialogue sugared + load scene after
            }
            else
            {
                foreach (Information information in informations)
                {
                    information.gameObject.SetActive(true);
                }
                informations.Clear();
            }
        }
    }
    private int CheckInformations()
    {
        int validInfo = 0;
        foreach (Information information in informations)
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
