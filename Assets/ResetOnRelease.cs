using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetOnRelease: MonoBehaviour, IEndDragHandler
{
    [SerializeField] Transform a;
    Vector3 scale;

    private void Start()
    {
        scale = transform.localScale;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = a.position;
        transform.localScale = scale;
    }
}
