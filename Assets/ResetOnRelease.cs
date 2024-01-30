using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetOnRelease: MonoBehaviour, IEndDragHandler
{
    Vector3 pos;
    Vector3 scale;

    private void Start()
    {
        pos = transform.position;
        scale = transform.localScale;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = pos;
        transform.localScale = scale;
    }
}
