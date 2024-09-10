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
    private int numberOfInformation = 9; // link with npc

    private Image image;
    private float width, height;

    private Page[] pages;

    private void Start()
    {
        image = FindObjectsOfType<Image>().First(x => x.gameObject.name == "GuideImage");

        int informationCount = numberOfInformation / numberOfInformationPerPage;
        pages = new Page[numberOfInformation % numberOfInformationPerPage == 0 ? informationCount : informationCount + 1];

        AddInformationToPages();

        Debug.Log(pages.Length);
        width = image.rectTransform.rect.width;
        height = image.rectTransform.rect.height;
    }

    private void Update() => DisplayPages();

    private void AddInformationToPages()
    {
        foreach (Page page in pages)
            page.informations.Add(new());
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
