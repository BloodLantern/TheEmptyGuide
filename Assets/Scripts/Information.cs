using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Information : MonoBehaviour
{
    public bool IsRight;
    public bool IsAssumption;
    public bool IsTruth;
    public bool IsDraggable;
    public Vector3 initialPosition;

    public bool IsDropped = false;
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
        if (toggleButton.image.color == Color.green)
            toggleButton.image.color = Color.red;
        else
            toggleButton.image.color = Color.green;
    }

    public void ToggleButtonUI()
    {
        toggleButton.gameObject.SetActive(!toggleButton.gameObject.activeSelf);
    }
}
