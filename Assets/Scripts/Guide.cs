using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Guide : MonoBehaviour
{
    private Information[] leftPageInformation;
    private Information[] rightPageInformation;

    [SerializeField]
    private GameObject displayGuide;
    private bool visible = true;
    private Vector3 initialDisplayPosition;

    private void Start()
    {
        List<Information> information = FindObjectsOfType<Information>().ToList();
        information.Sort((x, y) => x.transform.position.y < y.transform.position.y ? 1 : -1);
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
            foreach (DialogueInfo i in p.RewardInformation)
            {
                Information info = i.GatekeeperInformation ? rightPageInformation[rightInfos++] : leftPageInformation[leftInfos++];
                info.InformationText = i.Text;
                info.SetUI();
            }
        }
    }

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
