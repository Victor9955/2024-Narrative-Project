using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Movable : MonoBehaviour
{
    [HideInInspector] public bool isMoving;
    [HideInInspector] public Vector2 oldTransformScale;

    [SerializeField] private bool _zoomable;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _maxScaleZoom;
    [SerializeField] private float _movingSpeed;

    private bool _canMove = true;


    private bool CheckIfInsideObject(SpriteRenderer obj, Vector3 point)
    {
        point = Camera.main.ScreenToWorldPoint(point);
        point = new Vector3(point.x, point.y, 0);
        return obj.bounds.Contains(point);
    }

    private void Start()
    {
        oldTransformScale = transform.localScale;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
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

            //movement
            else if (Input.touchCount == 1 && !CheckIfInsideObject(GetComponent<SpriteRenderer>(), Input.GetTouch(0).position) && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _canMove = false;
            }

            else if (_canMove)
            {
                Vector2 deltaTouch;
                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    deltaTouch = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - Input.GetTouch(0).position);
                    transform.position = transform.position - new Vector3(deltaTouch.x * _movingSpeed, deltaTouch.y * _movingSpeed, 0);
                    //letterDesk.transform.localPosition = new Vector3(Mathf.Clamp(letterDesk.transform.localPosition.x, minMaxHorizontal.x, minMaxHorizontal.y), Mathf.Clamp(letterDesk.transform.localPosition.y, minMaxVertical.x, minMaxVertical.y), letterDesk.transform.localPosition.z);
                }
            }

            if (Input.touchCount == 1 && _canMove == false && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _canMove = true;
            }
        }
    }

}
