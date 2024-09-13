using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteract;

    [HideInInspector]
    public bool canInteract = true;

    public void Interact()
    {
        if (!canInteract)
            return;
        
        onInteract?.Invoke();
    }
}
