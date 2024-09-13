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

    [SerializeField] Information rightInfo1;
    [SerializeField] Information rightInfo2;
    [SerializeField] Information rightInfo3;
    [SerializeField] Information rightInfo4;

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

        rightInfo1.IsRight = true;
        rightInfo1.IsTruth = false; // change according to the lore
        rightInfo1.InformationText = "mais je vous rassure, il a été bien accueilli";
        rightInfo1.informationTextUI.text = rightInfo1.InformationText;

        rightInfo2.IsRight = true;
        rightInfo2.IsTruth = false; // change according to the lore
        rightInfo2.InformationText = "je peux vous dire qu'il va bien, il est heureux de son séjour ici.";
        rightInfo2.informationTextUI.text = rightInfo2.InformationText;

        rightInfo3.IsRight = true;
        rightInfo3.IsTruth = false; // change according to the lore
        rightInfo3.InformationText = "Votre camarade se balade tranquillement au village";
        rightInfo3.informationTextUI.text = rightInfo3.InformationText;

        rightInfo4.IsRight = true;
        rightInfo4.IsTruth = false; // change according to the lore
        rightInfo4.InformationText = "Il est parti profiter d'un délicieux repas, une spécialité d'ici";
        rightInfo4.informationTextUI.text = rightInfo4.InformationText;

        guide.rightPageInformation = new Information[4];
        guide.rightPageInformation[0] = rightInfo1;
        guide.rightPageInformation[1] = rightInfo2;
        guide.rightPageInformation[2] = rightInfo3;
        guide.rightPageInformation[3] = rightInfo4;

        guide.DisplayDiscoveredRightPageInfo();
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
