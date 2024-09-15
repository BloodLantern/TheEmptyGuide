using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GatekeeperReceiver : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private GatekeeperTrial GatekeeperTrial;
    
    public void OnDrop(PointerEventData eventData)
    {
        Image image = GetComponent<Image>();
        GameObject go = eventData.pointerDrag.gameObject;
        Information info = go.GetComponent<Information>();
        if (image.color == Color.green)
        {
            info.IsAssumption = true;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.yesDrop, transform.position);

        }
        else if (image.color == Color.red)
        {
            info.IsAssumption = false;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.noDrop, transform.position);

        }
        GatekeeperTrial.Information.Add(info);
        info.IsDropped = true;
        GatekeeperTrial.NumberOfInformationDropped++;
    }
}
