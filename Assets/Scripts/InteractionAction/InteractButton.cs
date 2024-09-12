using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractButton : MonoBehaviour
{
    bool isOn = false;
    Interactable interactable;

    [SerializeField] GameObject on, off;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.onInteract.AddListener(Toggle);
        Refresh();
    }

    void Toggle()
    {
        isOn = !isOn;
        Refresh();
    }

    void Refresh()
    {
        if (isOn)
        {
            off.SetActive(false);
            on.SetActive(true);
        }
        else
        {
            off.SetActive(true);
            on.SetActive(false);
        }
    }
}
