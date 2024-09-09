using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;



public class Guide : MonoBehaviour
{
    [SerializeField] Image image;

    private float width, height;
    private Page[] pages;
    private void Start()
    {
        width = image.rectTransform.rect.width;
        height = image.rectTransform.rect.height;
    }
    void Update()
    {
        DisplayPages();
    }
    private void DisplayPages()
    {
        foreach (Page page in pages)
        {
            page.DisplayInformations();
        }
    }
    private void ClearPages()
    {
        System.Array.Clear(pages, 0, pages.Length);
    }
}
