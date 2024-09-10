using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Flipper : MonoBehaviour
{
    Interactable interacter;
    bool flipped = false;

    // Start is called before the first frame update
    void Start()
    {
        interacter = GetComponent<Interactable>();
        interacter.onInteract.AddListener(Flip);
    }

    void Flip()
    {
        if (flipped)
        {
            transform.rotation = Quaternion.identity;
        }
        else
            transform.rotation = Quaternion.EulerRotation(0, 0, 90f);

        flipped = !flipped;
    }
}
