using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pipette : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    [Header("- SETUP -")]
    [SerializeField] private Transform _parent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent = _parent;
        transform.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 scale = transform.localScale;
        transform.DOScale(scale/ 1.5f, 0.2f).OnComplete(() =>
        {
            transform.DOScale(scale, 0.2f);
        });
    }
}
