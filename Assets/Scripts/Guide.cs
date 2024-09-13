using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Guide : MonoBehaviour
{
    private Information[] leftPageInformation;
    public Information[] rightPageInformation;

    [SerializeField]
    private GameObject guideDisplay;
    public bool Visible { get; private set; } = true;
    public bool IsOnGateKeeperTrial;
    public bool IsOnFinalGateKeeperTrial;
    [SerializeField]
    private GatekeeperTrial GatekeeperTrial;

    private void Awake()
    {
        List<Information> information = FindObjectsOfType<Information>().ToList();
        information.Sort((x, y) => x.transform.position.y < y.transform.position.y ? 1 : -1);
        leftPageInformation = information.Where(x => !x.IsRight).ToArray();
        ExtractInformationFromNpcs();
        GatekeeperTrial.Init();
        ToggleGuideDisplay();
        foreach (Information info in leftPageInformation)
        {
            info.gameObject.SetActive(false);
        }
    }

    private void ExtractInformationFromNpcs()
    {
        Dialogue[] npcs = FindObjectsOfType<Dialogue>();

        int leftInfos = 0;

        foreach (Dialogue p in npcs)
        {
            foreach (DialogueInfo i in p.RewardInformation)
            {
                if (!i.GatekeeperInformation)
                {
                    Information info = leftPageInformation[leftInfos++];
                    info.InformationText = i.Text;
                    info.IsTruth = i.Truth;
                    info.SetUI();
                }
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
        IsOnGateKeeperTrial = !IsOnGateKeeperTrial;
        GatekeeperTrial.gameObject.SetActive(!GatekeeperTrial.gameObject.activeSelf);
        if (!IsOnFinalGateKeeperTrial)
        {
            foreach (Information info in leftPageInformation)
            {
                info.IsDraggable = !info.IsDraggable;
            }
        }
        else
        {
            foreach (Information info in rightPageInformation)
            {
                info.IsDraggable = !info.IsDraggable;
            }
        }
    }

    public void UnlockInformation(string informationText, bool rightInfo)
    {
        foreach (Information info in rightInfo ? rightPageInformation : leftPageInformation)
        {
            if (info.InformationText == informationText)
            {
                info.gameObject.SetActive(true);
            }
        }
    }

    public void DisplayDiscoveredRightPageInfo()
    {
        for (int i = 0; i < 4; i++)
        {
            if (char.GetNumericValue(SceneManager.GetActiveScene().name[^1]) > i)
            {
                rightPageInformation[i].gameObject.SetActive(true);
            }
            else
            {
                rightPageInformation[i].gameObject.SetActive(false);
            }
        }
    }
}
