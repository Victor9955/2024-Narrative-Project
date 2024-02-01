using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PaperDesk : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject _liquidePrefab;
    private Color _colorFaded = new Color(255, 255, 255, 0);

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {

            Pipette pip = eventData.pointerDrag.transform.GetComponent<Pipette>();
            if (pip != null )
            {
                GameObject tache = Instantiate(_liquidePrefab, transform);

                tache.transform.DOScale(tache.transform.localScale * 2, 0.5f).OnComplete(() =>
                {
                    tache.GetComponent<RawImage>().DOColor(_colorFaded, 0.5f);
                    transform.DOScale(Vector3.zero, 0.5f).OnComplete(DestroyObject);
                });
            }
        }
        
    }


    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
