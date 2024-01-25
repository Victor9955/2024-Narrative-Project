using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler
{
    RawImage image;
    Vector2 pos;

    private void Start()
    {
        image = GetComponent<RawImage>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pos = transform.position - Camera.main.ScreenToWorldPoint(eventData.position);
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position) + pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
    }
}
