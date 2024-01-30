using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

public class ReadingManager : MonoBehaviour
{

    private GameObject currentLetter;
    private Vector2 oldTransformScale;

    [SerializeField] private float zoomSpeed;
    [SerializeField] private float maxScaleZoom;

    [SerializeField] private Vector2 minMaxHorizontal;
    [SerializeField] private Vector2 minMaxVertical;

    public void ReadingLetter(GameObject letter)
    {
        GameObject temp = Instantiate(letter, transform);
        currentLetter = temp;
    }

    public void EndReadingLetter ()
    {
        Destroy(currentLetter);
    }

    private void Start()
    {
        oldTransformScale = currentLetter.transform.localScale;
    }

    private void Update()
    {
        if (isActiveAndEnabled)
        {
            if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                UnityEngine.Touch touch0 = Input.GetTouch(0);
                UnityEngine.Touch touch1 = Input.GetTouch(1);

                Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
                Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

                float prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
                float currentMagnitude = (touch0.position - touch1.position).magnitude;
                float difference = currentMagnitude - prevMagnitude;


                currentLetter.transform.localScale = currentLetter.transform.localScale + new Vector3(zoomSpeed * difference, zoomSpeed * difference, 0);
                currentLetter.transform.localScale = new Vector3(Mathf.Clamp(currentLetter.transform.localScale.x, oldTransformScale.x, oldTransformScale.x * maxScaleZoom), Mathf.Clamp(currentLetter.transform.localScale.y, oldTransformScale.y, oldTransformScale.y * maxScaleZoom));
            }
            else if (Input.touchCount == 1)
            {
                Vector2 deltaTouch;
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    deltaTouch = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - Input.GetTouch(0).position);
                    currentLetter.transform.position = currentLetter.transform.position - new Vector3(deltaTouch.x * 0.001f, deltaTouch.y * 0.001f, 0);
                    currentLetter.transform.localPosition = new Vector3(Mathf.Clamp(currentLetter.transform.localPosition.x, minMaxHorizontal.x, minMaxHorizontal.y), Mathf.Clamp(currentLetter.transform.localPosition.y, minMaxVertical.x, minMaxVertical.y), currentLetter.transform.localPosition.z);
                }
            }
        }
    }
}
