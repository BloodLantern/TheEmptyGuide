using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;



public class Guide : MonoBehaviour
{
    private const int maxNumberOfInformationLeftPage = 10;
    private int numberOfInformationLeft; // link with npc
    private const int maxNumberOfInformationRightPage = 4;
    private int numberOfInformationRight = 4; // link with npc

    private Image image;
    private float width, height;

    private Information[] leftPageInformations;
    private Information[] rightPageInformations;

    [SerializeField] private GameObject displayGuide;

    private void Start()
    {
        image = FindObjectsOfType<Image>().First(x => x.gameObject.name == "GuideImage");
        Information[] information = FindObjectsOfType<Information>();
        leftPageInformations = information.Where(x => !x.isRight).ToArray();
        rightPageInformations = information.Where(x => x.isRight).ToArray();
        ExtractInformationFromNPCs();

        width = image.rectTransform.rect.width;
        height = image.rectTransform.rect.height;
    }

    private void ExtractInformationFromNPCs()
    {
        Dialogue[] npcs = FindObjectsOfType<Dialogue>();

        int validatedDialogues = 0;

        foreach (Dialogue p in npcs)
        {
            if (p.HasInformation)
            {
                Information info;
                if (p.GatekeeperInformation)
                    info = rightPageInformations[validatedDialogues];
                else
                    info = leftPageInformations[validatedDialogues];

                info.InformationText = p.InformationText;

                info.SetTextUI();

                validatedDialogues++;
            }
        }
    }
    //private void ClearLeftPage() => leftPageInformations.;

    public void ToggleGuideDisplay()
    {
        displayGuide.SetActive(!displayGuide.activeSelf);
    }
}
