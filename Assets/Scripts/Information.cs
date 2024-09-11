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
    private Button trueButton;
    [SerializeField]
    private Button falseButton;

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
    public void OnGreenButtonClick()
    {
        isAssumption = true;
        trueButton.image.color = Color.white; // selected color
        falseButton.image.color = Color.red; // base color
    }
    public void OnRedButtonClick()
    {
        isAssumption = false;
        falseButton.image.color = Color.white; // selected color
        trueButton.image.color = Color.green; // base color
    }

    public void ToggleButtonUI()
    {
        trueButton.gameObject.SetActive(!trueButton.gameObject.activeSelf);
        falseButton.gameObject.SetActive(!falseButton.gameObject.activeSelf);
    }
}
