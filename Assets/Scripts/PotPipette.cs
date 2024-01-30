using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PotPipette : MonoBehaviour
{

    public Transform potAnchor;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Pipette pip = eventData.pointerDrag.transform.GetComponent<Pipette>();
            if (pip != null)
            {
                pip.transform.position = potAnchor.position;
            }
        }

    }
}
