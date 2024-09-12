using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;
    
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private Information info;

    private void Awake()
    {
        info = GetComponent<Information>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!info.IsDraggable)
            return;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!info.IsDraggable)
            return;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor*1.5f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!info.IsDraggable)
            return;

        canvasGroup.blocksRaycasts = true;
        transform.localPosition = info.initialPosition;
        if (info.IsDropped)
        {
            info.IsDropped = false;
            info.gameObject.SetActive(false);
        }
    }
}
