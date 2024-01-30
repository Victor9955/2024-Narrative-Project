using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;

public class MovableUI : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler
{
    [Header("- SETUP -")]
    [SerializeField] private bool _zoomable;

    [SerializeField, ShowIf("_zoomable")] private float _maxScaleZoom;
    [SerializeField, ShowIf("_zoomable")] private float _zoomSpeed;

    [HideInInspector] public Vector3 oldTransformScale;
    private RawImage _image;
    private Vector2 _pos;


    private void Start()
    {
        oldTransformScale = transform.localScale;
        _image = GetComponent<RawImage>();
    }

    private void Update()
    {
        //zoom
        if (_zoomable && Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            UnityEngine.Touch touch0 = Input.GetTouch(0);
            UnityEngine.Touch touch1 = Input.GetTouch(1);

            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            float prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
            float currentMagnitude = (touch0.position - touch1.position).magnitude;
            float difference = currentMagnitude - prevMagnitude;


            transform.localScale += new Vector3(_zoomSpeed * difference, _zoomSpeed * difference, 0);
            transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, oldTransformScale.x, oldTransformScale.x * _maxScaleZoom), Mathf.Clamp(transform.localScale.y, oldTransformScale.y, oldTransformScale.y * _maxScaleZoom));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pos = transform.position - Camera.main.ScreenToWorldPoint(eventData.position);
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position) + _pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;
    }
}
