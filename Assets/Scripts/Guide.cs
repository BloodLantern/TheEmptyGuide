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
    [FormerlySerializedAs("numberOfInformationsPerPage")] [SerializeField]
    private int numberOfInformationPerPage;
    private int numberOfInformation = 6; // link with npc

    private Image image;
    private float width, height;

    private Page[] pages;
    private List<Information> informations;

    private void Start()
    {
        image = FindObjectsOfType<Image>().First(x => x.gameObject.name == "GuideImage");
        
        ExtractInformationFromNPCs();

        int informationRatio = numberOfInformation / numberOfInformationPerPage;
        pages = new Page[numberOfInformation % numberOfInformationPerPage == 0 ? informationRatio : informationRatio + 1];

        AddInformationToPages();

        width = image.rectTransform.rect.width;
        height = image.rectTransform.rect.height;
    }

    private void Update() => DisplayPages();


    private void ExtractInformationFromNPCs()
    {
        Dialogue[] PNJS = FindObjectsOfType<Dialogue>();

        int counter = 0;
        int informationRatio;

        foreach (Dialogue p in PNJS)
        {
            if (p.HasInformation)
            {
                counter++;
                Information info = gameObject.AddComponent<Information>();
                info.InformationText = p.InformationText;
                informationRatio = counter / numberOfInformationPerPage;
                pages[numberOfInformation % numberOfInformationPerPage == 0 ? informationRatio : informationRatio + 1].informations.Add(info);
                informations.Add(info);
            }
        }
    }
    private void AddInformationToPages()
    {
        //foreach (Page page in pages)
            //page.informations.Add(new());
    }

    private void DisplayPages()
    {
        if (pages == null)
            return;
        
        foreach (Page page in pages)
        {
            //page.DisplayInformation(numberOfInformation);
        }
    }

    private void ClearPages() => Array.Clear(pages, 0, pages.Length);
}
