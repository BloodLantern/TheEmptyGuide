using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Dialogue : MonoBehaviour
{
    [Tooltip("Dialogue text")]
    [SerializeField]
    private string text = string.Empty;
    public string Text => text;

    [Tooltip("Delay in seconds between each character")]
    [SerializeField]
    private float textAdvanceDelay = 0.05f;
    public float TextAdvanceDelay => textAdvanceDelay;

    [Tooltip("Whether this dialogue gives an information")]
    [SerializeField]
    private bool hasInformation = true;
    public bool HasInformation => hasInformation;

    [Header("Information data")]
    [Tooltip("If empty, will instead use the dialogue text")]
    [ShowIf("hasInformation")]
    [SerializeField]
    private string informationText = string.Empty;
    public string InformationText => informationText == string.Empty ? text : informationText;

    [Tooltip("Whether the information is true or false")]
    [ShowIf("hasInformation")]
    [SerializeField]
    private bool validInformation;
    public bool ValidInformation => validInformation;
    
    public bool GatekeeperInformation { get; private set; }

    private DialogueDisplay dialogueDisplay;

    private void Start() => dialogueDisplay = FindObjectOfType<DialogueDisplay>();

    private void Awake()
    {
        GatekeeperInformation = TryGetComponent<Gatekeeper>(out _);
        GetComponent<Interactable>().onInteract.AddListener(Display);
    }

    public void Display() => dialogueDisplay.CurrentDialogue = this;
}
