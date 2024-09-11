using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Information : MonoBehaviour
{
    public bool IsRight;
    public bool isAssumption;
    public bool isTruth;
    public Vector3 initialPosition;
    public string InformationText { get; set; }
    private TextMeshProUGUI informationTextUI;

    [SerializeField]
    private Button toggleButton;

    private void Start()
    {
        informationTextUI = GetComponentInChildren<TextMeshProUGUI>();
        initialPosition = transform.localPosition;
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
