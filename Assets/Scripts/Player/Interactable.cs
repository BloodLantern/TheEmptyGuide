using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteract;

    SpriteRenderer _renderer;
    Color baseColor;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        baseColor = _renderer.color;
    }

    public void Interact()
    {
        onInteract?.Invoke();
    }

    public void ActivateHighlight()
    {
        _renderer.color = Color.yellow;
    }

    public void DeactivateHighlight()
    {
        _renderer.color = baseColor;
    }
}
