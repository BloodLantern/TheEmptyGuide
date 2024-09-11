using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct DialogueText
{
    [SerializeField]
    private string text;
    public string Text => text;
    
    [Tooltip("Sprite of the character that is currently talking. Set to the current SpriteRenderer if null")]
    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite => sprite;
}

[Serializable]
public struct DialogueInfo
{
    [SerializeField]
    private string text;
    public string Text => text;

    [SerializeField]
    private bool truth;
    public bool Truth => truth;
        
    [Tooltip("Whether the information comes from a gatekeeper and should be on the right page of the guide")]
    [SerializeField]
    private bool gatekeeperInformation;
    public bool GatekeeperInformation => gatekeeperInformation;
}

[RequireComponent(typeof(Interactable))]
public class Dialogue : MonoBehaviour
{
    public const float TextAdvanceDelay = 0.05f;
    
    [SerializeField]
    private DialogueText[] texts;
    public DialogueText[] Texts => texts;
    
    [SerializeField]
    private DialogueInfo[] rewardInformation;
    public DialogueInfo[] RewardInformation => rewardInformation;

    private DialogueDisplay dialogueDisplay;

    private void Start() => dialogueDisplay = FindObjectOfType<DialogueDisplay>();

    private void Awake() => GetComponent<Interactable>().onInteract.AddListener(Display);

    public void Display() => dialogueDisplay.CurrentDialogue = this;
}
