using System;
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

    [SerializeField]
    private GameObject displayGuide;
    private bool visible = true;
    private Vector3 initialDisplayPosition;

    private void Start()
    {
        Information[] information = FindObjectsOfType<Information>();
        leftPageInformation = information.Where(x => !x.IsRight).ToArray();
        rightPageInformation = information.Where(x => x.IsRight).ToArray();
        ExtractInformationFromNpcs();
    }

    private void Awake()
    {
        initialDisplayPosition = displayGuide.transform.position;
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
        visible = !visible;
        displayGuide.transform.position = visible ? initialDisplayPosition : initialDisplayPosition + Vector3.down * 1500f;
    }

    public void UnlockInformation(string informationText, bool rightInfo)
    {
        foreach (Information info in rightInfo ? rightPageInformation : leftPageInformation)
        {
            if (info.InformationText == informationText)
                info.gameObject.SetActive(true);
        }
    }
}
