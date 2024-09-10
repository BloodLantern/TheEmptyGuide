using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DialogueDisplay : MonoBehaviour
{
    private Dialogue dialogue;

    public Dialogue CurrentDialogue
    {
        get => dialogue;
        set
        {
            if (dialogue == value || visible)
                return;
            
            dialogue = value;
            textAdvanceTimer = 0f;
            DisplayedCharacters = 0;

            Show();
        }
    }
    
    private float textAdvanceTimer;

    public int DisplayedCharacters { get; private set; }

    private TextMeshProUGUI textMesh;

    private bool visible;
    
    private Vector3 initialParentPosition;

    private void Start() => textMesh = GetComponent<TextMeshProUGUI>();

    private void Awake()
    {
        initialParentPosition = transform.parent.position;
        ResetPosition();
    }

    private void Update()
    {
        if (dialogue is null)
        {
            Hide();
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && DisplayedCharacters > 0)
        {
            // When interacting again while the dialogue is displayed
            int textLength = dialogue.Text.Length;
            if (DisplayedCharacters >= textLength)
            {
                // If the text is already displayed in its entirety, close the dialogue
                dialogue = null;
                textMesh.text = string.Empty;
                return;
            }
            
            // Otherwise, show the full dialogue text
            DisplayedCharacters = textLength;
            textAdvanceTimer = 0f;
        }

        if (textAdvanceTimer <= 0f)
        {
            // Increase the displayed characters by one, making sure it is still less than or equal to the text length
            DisplayedCharacters = Math.Min(DisplayedCharacters + 1, dialogue.Text.Length);
            textAdvanceTimer = dialogue.TextAdvanceDelay;
            
            textMesh.text = dialogue.Text[..DisplayedCharacters];
        }
        
        textAdvanceTimer -= Time.deltaTime;
    }

    public void Show()
    {
        visible = true;
        transform.parent.position = initialParentPosition;
    }

    public void Hide()
    {
        visible = false;
        ResetPosition();
    }
    
    private void ResetPosition() => transform.parent.position = initialParentPosition + Vector3.down * 150f;
}
