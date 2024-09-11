using System.Linq;
using UnityEngine;

public class Guide : MonoBehaviour
{
    private const int MaxInformationLeftPage = 10;
    private int numberOfInformationLeft; // link with npc
    private const int MaxInformationRightPage = 4;
    private int numberOfInformationRight = 4; // link with npc

    private Information[] leftPageInformation;
    private Information[] rightPageInformation;

    [SerializeField] private GameObject displayGuide;

    private void Start()
    {
        Information[] information = FindObjectsOfType<Information>();
        leftPageInformation = information.Where(x => !x.isRight).ToArray();
        rightPageInformation = information.Where(x => x.isRight).ToArray();
        ExtractInformationFromNpcs();

        ToggleGuideDisplay();
    }

    private void ExtractInformationFromNpcs()
    {
        Dialogue[] npcs = FindObjectsOfType<Dialogue>();

        int leftInfos = 0, rightInfos = 0;

        foreach (Dialogue p in npcs)
        {
            if (!p.HasInformation)
                continue;

            Information info = p.GatekeeperInformation ? rightPageInformation[rightInfos++] : leftPageInformation[leftInfos++];
            info.InformationText = p.InformationText;
            info.SetUI();
        }
    }
    //private void ClearLeftPage() => leftPageInformation.;

    public void ToggleGuideDisplay()
    {
        displayGuide.SetActive(!displayGuide.activeSelf);
    }
}
