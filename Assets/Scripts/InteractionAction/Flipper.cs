using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Flipper : MonoBehaviour
{
    private Interactable interacter;
    private bool flipped = false;

    // Start is called before the first frame update
    private void Start()
    {
        interacter = GetComponent<Interactable>();
        interacter.onInteract.AddListener(Flip);
    }

    private void Flip()
    {
        transform.rotation = flipped ? Quaternion.identity : Quaternion.Euler(0, 0, 90f);

        flipped = !flipped;
    }
}
