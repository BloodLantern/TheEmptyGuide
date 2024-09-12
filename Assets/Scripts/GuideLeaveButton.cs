using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideLeaveButton : MonoBehaviour
{
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite cursorOnSprite;
    [SerializeField] private Sprite cursorOnClickSprite;
    [SerializeField] private Sprite closeSprite;

    private Guide guide;

    private void Start()
    {
        guide = FindObjectOfType<Guide>();
    }
    public void Clicked()
    {
        if (guide.IsOnGateKeeperTrial)
            guide.ToggleGatekeeperTrialDisplay();
        guide.ToggleGuideDisplay();
    }
}
