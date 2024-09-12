using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Guide : MonoBehaviour
{
    private Information[] leftPageInformation;
    private static Information[] rightPageInformation = new Information[4];

    [SerializeField]
    private GameObject guideDisplay;
    public bool Visible { get; private set; } = true;

    [SerializeField]
    private GatekeeperTrial GatekeeperTrial;

    private void Start()
    {
        List<Information> information = FindObjectsOfType<Information>().ToList();
        information.Sort((x, y) => x.transform.position.y < y.transform.position.y ? 1 : -1);
        leftPageInformation = information.Where(x => !x.IsRight).ToArray();
        rightPageInformation = information.Where(x => x.IsRight).ToArray();
        ExtractInformationFromNpcs();
        ToggleGuideDisplay();
        foreach (Information info in leftPageInformation)
        {
            info.gameObject.SetActive(false);
        }
    }

    private void ExtractInformationFromNpcs()
    {
        Dialogue[] npcs = FindObjectsOfType<Dialogue>();

        int leftInfos = 0, rightInfos = 0;

        foreach (Dialogue p in npcs)
        {
            foreach (DialogueInfo i in p.RewardInformation)
            {
                Information info = i.GatekeeperInformation ? rightPageInformation[rightInfos++] : leftPageInformation[leftInfos++];
                info.InformationText = i.Text;
                info.IsTruth = i.Truth;
                info.SetUI();
            }
        }
    }

    public void ToggleGuideDisplay()
    {
        Visible = !Visible;
        guideDisplay.gameObject.SetActive(!guideDisplay.gameObject.activeSelf);

        if (!Visible && GatekeeperTrial.gameObject.activeSelf)
            GatekeeperTrial.gameObject.SetActive(false);
        
    }
    public void ToggleGatekeeperTrialDisplay()
    {
        GatekeeperTrial.gameObject.SetActive(!GatekeeperTrial.gameObject.activeSelf);
        foreach (Information info in leftPageInformation)
        {
            info.IsDraggable = !info.IsDraggable;
        }
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
