using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class Guide : MonoBehaviour
{
    [SerializeField] UInt16 numberOfInformationsPerPage;
    private UInt16 numberOfInformations = 9; // link with pnj

    private Image image;
    private float width, height;

    private Page[] pages;

    private void Start()
    {
        image = FindObjectsOfType<Image>().First(x => x.gameObject.name == "GuideImage");

        if (numberOfInformations % numberOfInformationsPerPage == 0)
            pages = new Page[(numberOfInformations / numberOfInformationsPerPage)];
        else
            pages = new Page[(numberOfInformations / numberOfInformationsPerPage) + 1];

        AddInformationsToPages();

        Debug.Log(pages.Length);
        width = image.rectTransform.rect.width;
        height = image.rectTransform.rect.height;
    }

    void Update()
    {
        DisplayPages();
    }

    private void AddInformationsToPages()
    {
        foreach (Page page in pages)
        {
            page.informations.Add(new());
        }
    }

    private void DisplayPages()
    {
        if (pages != null)
        {
            foreach (Page page in pages)
            {
                //page.DisplayInformations(numberOfInformations);
            }
        }
    }

    private void ClearPages()
    {
        System.Array.Clear(pages, 0, pages.Length);
    }
}
