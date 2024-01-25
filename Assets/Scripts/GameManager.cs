using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<Letter> letterList = new List<Letter>();

    public bool isReadingLetter;
    public Transform letterDesk;

    private static GameManager _instance;
    [SerializeField] private float zoomSpeed;
    private Vector2 oldTransformScale;
    [SerializeField] private float maxScaleZoom;

    bool canMove = true;

    public static GameManager Instance
    {
        get { 
            if (_instance == null)
            {
                Debug.LogError("game manager est nul");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        oldTransformScale = letterDesk.transform.localScale;
    }

    private bool CheckIfInsideObject (SpriteRenderer letter, Vector3 point)
    {
        point = Camera.main.ScreenToWorldPoint(point);
        point = new Vector3(point.x, point.y, 0);
        return letter.bounds.Contains(point);
    }


    public void ReadingLetterDesk ()
    {
        isReadingLetter = true;
        letterDesk.gameObject.SetActive(true);
    }

    public void ClosingLetterDesk ()
    {
        isReadingLetter = false;
        letterDesk.transform.localScale = oldTransformScale;
        letterDesk.gameObject.SetActive(false);
    }

    private void Update()
    {

        if (isReadingLetter)
        {
            //pinch
            if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                UnityEngine.Touch touch0 = Input.GetTouch(0);
                UnityEngine.Touch touch1 = Input.GetTouch(1);

                Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
                Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

                float prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
                float currentMagnitude = (touch0.position - touch1.position).magnitude;
                float difference = currentMagnitude - prevMagnitude;


                letterDesk.transform.localScale += new Vector3(zoomSpeed * difference, zoomSpeed * difference, 0);
                letterDesk.transform.localScale = new Vector3(Mathf.Clamp(letterDesk.transform.localScale.x, oldTransformScale.x, oldTransformScale.x * maxScaleZoom), Mathf.Clamp(letterDesk.transform.localScale.y, oldTransformScale.y, oldTransformScale.y * maxScaleZoom));
            }

            else if (Input.touchCount == 1 && !CheckIfInsideObject(letterDesk.GetComponent<SpriteRenderer>(), Input.GetTouch(0).position) && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                canMove = false;
            }

            else if (canMove)
            {
                Vector2 deltaTouch;
                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    deltaTouch = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - Input.GetTouch(0).position);
                    letterDesk.transform.position = letterDesk.transform.position - new Vector3(deltaTouch.x * 0.001f, deltaTouch.y * 0.001f, 0);
                    //letterDesk.transform.localPosition = new Vector3(Mathf.Clamp(letterDesk.transform.localPosition.x, minMaxHorizontal.x, minMaxHorizontal.y), Mathf.Clamp(letterDesk.transform.localPosition.y, minMaxVertical.x, minMaxVertical.y), letterDesk.transform.localPosition.z);
                }
            }

            if (canMove == false && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                canMove = true;
            }
        }
    }
}
