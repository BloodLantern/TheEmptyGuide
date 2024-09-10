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

    public List<Information> leftPageInformations;
    public List<Information> rightPageInformations;

    private void Start()
    {
        image = FindObjectsOfType<Image>().First(x => x.gameObject.name == "GuideImage");
        
        ExtractInformationFromNPCs();

        AddInformationToPages();

        width = image.rectTransform.rect.width;
        height = image.rectTransform.rect.height;
    }

    private void ExtractInformationFromNPCs()
    {
        Dialogue[] npcs = FindObjectsOfType<Dialogue>();

        foreach (Dialogue p in npcs)
        {
            if (p.HasInformation)
            {
                Information info = gameObject.AddComponent<Information>();
                info.InformationText = p.InformationText;
                //informations.Add(info);
            }
        }
    }
    private void AddInformationToPages()
    {
        //foreach (Page page in pages)
            //page.informations.Add(new());
    }

  

    private void ClearLeftPage() => leftPageInformations.Clear();
    private void ClearRightPage() => rightPageInformations.Clear();
}
