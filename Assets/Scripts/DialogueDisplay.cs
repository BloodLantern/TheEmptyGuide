using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{
    private Dialogue dialogue;

    public Dialogue CurrentDialogue
    {
        get => dialogue;
        set
        {
            if (dialogue == value || visible || value?.Texts.Length == 0)
                return;
            
            dialogue = value;
            ResetTextPosition();
            DialogueTextIndex = 0;
            UpdateSprite();

            Show();
        }
    }
    
    private float textAdvanceTimer;

    public DialogueText CurrentText => CurrentDialogue.Texts[DialogueTextIndex];

    public int DisplayedCharacters { get; private set; }
    
    public int DialogueTextIndex { get; private set; }

    [SerializeField]
    private TextMeshProUGUI textMesh;

    private bool visible;
    
    private PlayerActions input;

    private Guide guide;

    [SerializeField]
    private Image characterImage;
    
    private Player player;

    private void Start()
    {
        input = new();
        input.Enable();
        guide = FindObjectOfType<Guide>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (dialogue is null)
        {
            Hide();
            return;
        }

        if (input.asset["Interact"].WasPerformedThisFrame() && DisplayedCharacters > 0)
        {
            // When interacting again while the dialogue is displayed
            int textLength = CurrentText.Text.Length;
            if (DisplayedCharacters >= textLength)
            {
                // If the text is already displayed in its entirety, check whether the dialogue is finished
                if (++DialogueTextIndex < dialogue.Texts.Length)
                {
                    // There is still more text to display, reset the current position and timer
                    ResetTextPosition();
                    UpdateSprite();
                }
                else
                {
                    // Unlock the information in the guide and close the dialogue
                    foreach (DialogueInfo info in dialogue.RewardInformation)
                        guide.UnlockInformation(info.Text, info.GatekeeperInformation);
                    Hide();
                    textMesh.text = string.Empty;
                    dialogue.OnDialogueEnd.Invoke();
                    dialogue = null;
                    return;
                }
            }
            else
            {
                // Otherwise, show the full dialogue text
                DisplayedCharacters = textLength;
                textAdvanceTimer = 0f;
            }
        }

        if (textAdvanceTimer <= 0f)
        {
            // Increase the displayed characters by one, making sure it is still less than or equal to the text length
            DisplayedCharacters = Math.Min(DisplayedCharacters + 1, CurrentText.Text.Length);
            textAdvanceTimer = Dialogue.TextAdvanceDelay;
            
            textMesh.text = CurrentText.Text[..DisplayedCharacters].Replace('\\', '\n');
        }
        
        textAdvanceTimer -= Time.deltaTime;
    }

    public void Show()
    {
        visible = true;
        player.SetModeDummy();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        visible = false;
        player.SetModeMove();
        gameObject.SetActive(false);
    }

    private void ResetTextPosition()
    {
        DisplayedCharacters = 0;
        textAdvanceTimer = 0f;
    }

    private void UpdateSprite()
    {
        if (dialogue is null)
            return;

        characterImage.sprite = dialogue.Texts[DialogueTextIndex].Sprite;
        if (!characterImage.sprite && dialogue.TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            characterImage.sprite = spriteRenderer.sprite;

            if (!characterImage.sprite)
                characterImage.sprite = dialogue.GetComponentInParent<SpriteRenderer>().sprite;
        }

        characterImage.color = characterImage.sprite ? Color.white : new(0f, 0f, 0f, 0f);
    }
}
