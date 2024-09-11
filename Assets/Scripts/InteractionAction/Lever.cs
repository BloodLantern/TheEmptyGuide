using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public enum LeverType
{
    None,
    Door,
    Color
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

    public bool EnabledState { get; private set; }

    private bool DoorType => type == LeverType.Door;
    private bool ColorType => type == LeverType.Color;
    
    [ShowIf("ColorType")]
    [SerializeField]
    private Color newColor;

    private SpriteRenderer objRenderer;
    private Color oldColor;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.onInteract.AddListener(Toggle);

        if (obj && obj.TryGetComponent(out objRenderer))
            oldColor = objRenderer.color;
    }

    private void Toggle()
    {
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
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
