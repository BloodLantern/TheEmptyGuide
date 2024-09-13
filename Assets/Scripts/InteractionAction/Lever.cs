using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public enum LeverType
{
    None,
    Door,
    Color,
    Sprite
}

[RequireComponent(typeof(Interactable))]
public class Lever : MonoBehaviour
{
    private Interactable interactable;

    [SerializeField]
    private LeverType type;
    public LeverType Type => type;

    [SerializeField]
    private GameObject obj;
    public GameObject Obj => obj;

    [SerializeField]
    private bool multipleActivations;

    public bool EnabledState { get; private set; }

    public bool DoorType => type == LeverType.Door;
    public bool ColorType => type == LeverType.Color;
    public bool SpriteType => type == LeverType.Sprite;
    
    [ShowIf("ColorType")]
    [SerializeField]
    private Color newColor;
    
    [ShowIf("SpriteType")]
    [SerializeField]
    private Sprite newSprite;

    private SpriteRenderer objRenderer;
    
    private Color oldColor;
    private Sprite oldSprite;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.onInteract.AddListener(Toggle);

        if (obj && obj.TryGetComponent(out objRenderer))
        {
            oldColor = objRenderer.color;
            oldSprite = objRenderer.sprite;
        }
    }

    private void Toggle()
    {
        if (EnabledState && !multipleActivations)
            return;
        
        EnabledState = !EnabledState;
        
        switch (type)
        {
            case LeverType.None:
                Debug.LogWarning("Lever with type None toggled");
                break;
            
            case LeverType.Door:
                obj.SetActive(!obj.activeSelf);
                break;
            
            case LeverType.Color:
                objRenderer.color = EnabledState ? newColor : oldColor;
                break;
            
            case LeverType.Sprite:
                objRenderer.sprite = EnabledState ? newSprite : oldSprite;
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
