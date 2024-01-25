using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

public class ReadingManager : MonoBehaviour
{

    public TextMeshProUGUI titre;
    public TextMeshProUGUI corps;
    public RectTransform image;

    private Vector2 oldTransformScale;

    [SerializeField] private float zoomSpeed;
    [SerializeField] private float maxScaleZoom;

    [SerializeField] private Vector2 minMaxHorizontal;
    [SerializeField] private Vector2 minMaxVertical;

    public void ReadingLetter(Letter letter)
    {
        titre.text = letter.title;
        corps.text = letter.pages[0];
        image.GetComponent<Image>().sprite = letter.letterType;
    }

    public void EndReadingLetter ()
    {
        image.transform.position = Vector3.zero;
        image.transform.localScale = oldTransformScale;
    }

    private void Start()
    {
        oldTransformScale = image.transform.localScale;
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


                image.transform.localScale = image.transform.localScale + new Vector3(zoomSpeed * difference, zoomSpeed * difference, 0);
                image.transform.localScale = new Vector3(Mathf.Clamp(image.transform.localScale.x, oldTransformScale.x, oldTransformScale.x * maxScaleZoom), Mathf.Clamp(image.transform.localScale.y, oldTransformScale.y, oldTransformScale.y * maxScaleZoom));
            }
            else if (Input.touchCount == 1)
            {
                Vector2 deltaTouch;
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    deltaTouch = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - Input.GetTouch(0).position);
                    image.transform.position = image.transform.position - new Vector3(deltaTouch.x * 0.001f, deltaTouch.y * 0.001f, 0);
                    image.transform.localPosition = new Vector3(Mathf.Clamp(image.transform.localPosition.x, minMaxHorizontal.x, minMaxHorizontal.y), Mathf.Clamp(image.transform.localPosition.y, minMaxVertical.x, minMaxVertical.y), image.transform.localPosition.z);
                }
            }
        }
    }
}
