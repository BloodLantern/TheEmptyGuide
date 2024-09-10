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
            dialogue = value;
            textAdvanceTimer = 0f;
            DisplayedCharacters = 0;
        }
    }
    
    private float textAdvanceTimer;

    public int DisplayedCharacters { get; private set; }

    private TextMeshProUGUI textMesh;

    private void Start() => textMesh = GetComponent<TextMeshProUGUI>();

    private void Update()
    {
        if (dialogue is null)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            // When interacting again while the dialogue is displayed
            int textLength = dialogue.Text.Length;
            if (DisplayedCharacters >= textLength)
            {
                // If the text is already displayed in its entirety, close the dialogue
                dialogue = null;
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
}
