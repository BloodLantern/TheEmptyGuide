using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GatekeeperReceiver : MonoBehaviour, IDropHandler
{
    private int infoIndex = 0;
    [SerializeField] private Gatekeeper gatekeeper;
    public void OnDrop(PointerEventData eventData)
    {
        Image image = GetComponent<Image>();
        if (image.color == Color.green)
        {
            Debug.Log("Green");
            GameObject go = eventData.pointerDrag.gameObject;
            Information info = go.GetComponent<Information>();
            info.IsAssumption = true;
            gatekeeper.informations.Add(info);
            info.IsDropped = true;
        }
        else if (image.color == Color.red)
        {
            Debug.Log("Red");
            GameObject go = eventData.pointerDrag.gameObject;
            Information info = go.GetComponent<Information>();
            info.IsAssumption = false;
            gatekeeper.informations.Add(info);
            info.IsDropped = true;
        }
        infoIndex++;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
