using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Information : MonoBehaviour
{
    public bool IsRight;
    private bool isAssumption;
    private bool isTruth;

    public string InformationText { get; set; }
    private TextMeshProUGUI informationTextUI;

    [SerializeField]
    private Button toggleButton;

    private void Start()
    {
        informationTextUI = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void SetUI()
    {
        informationTextUI = GetComponentInChildren<TextMeshProUGUI>();
        informationTextUI.text = InformationText;
        gameObject.SetActive(true);
    }

    public void ToggleButton()
    {
        isAssumption = !isAssumption;
        if (isAssumption)
            toggleButton.image.color = Color.green;
        else
            toggleButton.image.color = Color.red;
    }

    public void ToggleButtonUI()
    {
        toggleButton.gameObject.SetActive(!toggleButton.gameObject.activeSelf);
    }
}
