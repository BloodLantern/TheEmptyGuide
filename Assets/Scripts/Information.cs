using UnityEngine;
using TMPro;

public class Information : MonoBehaviour
{
    public bool IsRight;
    private bool isAssumption;
    private bool isTruth;

    public string InformationText { get; set; }
    private TextMeshProUGUI informationTextUI;

    public void SetTextUI()
    {
        informationTextUI = GetComponentInChildren<TextMeshProUGUI>();
        informationTextUI.text = InformationText;
        SetVisible(false);
    }
    public void OnGreenButtonClick()
    {
        isAssumption = true;
    }
    public void OnRedButtonClick()
    {
        isAssumption = false;
    }

    public void SetVisible(bool visible)
    {
        informationTextUI.alpha = visible ? 1f : 0f;
    }
}
