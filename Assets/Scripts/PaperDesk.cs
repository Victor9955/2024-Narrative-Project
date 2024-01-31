using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PaperDesk : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {

            Pipette pip = eventData.pointerDrag.transform.GetComponent<Pipette>();
            if (pip != null)
            {
                transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
                {
                    Destroy(gameObject);
                });
            }
        }
        
    }
}
