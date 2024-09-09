using System.Linq;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private string text = string.Empty;
    public string Text => text;

    public int DisplayedCharacters { get; private set; }

    public string DisplayedText { get; private set; } = string.Empty;

    private TextMeshPro textMesh;

    private void Start()
    {
        textMesh = FindObjectsOfType<TextMeshPro>().First(x => x.gameObject.name == "Dialogue");
    }
}
