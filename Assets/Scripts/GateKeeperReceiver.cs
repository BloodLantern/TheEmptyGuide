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
        if (image.color == Color.green)
        {
            Debug.Log("Green");
            GameObject go = eventData.pointerDrag.gameObject;
            Information info = go.GetComponent<Information>();
            info.IsAssumption = true;
            GatekeeperTrial.information.Add(info);
            info.IsDropped = true;
        }
        else if (image.color == Color.red)
        {
            Debug.Log("Red");
            GameObject go = eventData.pointerDrag.gameObject;
            Information info = go.GetComponent<Information>();
            info.IsAssumption = false;
            GatekeeperTrial.information.Add(info);
            info.IsDropped = true;
        }
    }
}
