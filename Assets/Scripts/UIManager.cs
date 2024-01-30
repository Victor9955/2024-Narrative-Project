using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public ReadingManager readingManager;
    public Transform drawerLocation;

    public GameObject drawer;
    public GameObject reading;

     private float maxScrollLeft;
    [SerializeField] private float maxScrollRight;

    [SerializeField] private float speedDrawer;
    [SerializeField] private GameObject letterTemplate;

    public void ReadLetter(GameObject letter)
    {
        readingManager.gameObject.SetActive(true);
        CloseDrawer(false);
        readingManager.ReadingLetter(letter);
    }

    public void CloseLetter ()
    {
        reading.SetActive(false);
        readingManager.EndReadingLetter();

        OpenDrawer();
    }

    public void Update ()
    {
        if (drawer.activeSelf && Input.touchCount == 1)
        {
            float deltaTouch;

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                deltaTouch = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - Input.GetTouch(0).position).x;
                drawerLocation.position = drawerLocation.position - new Vector3(deltaTouch * speedDrawer, 0, 0);
                drawerLocation.localPosition = new Vector3(Mathf.Clamp(drawerLocation.localPosition.x, maxScrollLeft, maxScrollRight), drawerLocation.position.y, drawerLocation.position.z);
            }
        }
    }

    public void OpenDrawer ()
    {
        drawer.SetActive(true);
        GameManager.Instance.ClosingLetterDesk();

        List<GameObject> listLetters = GameManager.Instance.letterList;

        //setup a quel point on peut scroll vers la gauche
        maxScrollLeft = ((listLetters.Count - 1) * -500) + 1500;
        maxScrollLeft = Mathf.Clamp(maxScrollLeft, -10000000, 0);

        //remplis le tiroir de lettres
        for (int i = 0; i < listLetters.Count; i++)
        {
            GameObject letterDrawer = Instantiate(letterTemplate, drawerLocation.position, Quaternion.identity, drawerLocation);
            LetterDrawer letter = letterDrawer.GetComponent<LetterDrawer>();

            letter.letter = listLetters[i];
            letter.Setup(this);
        }
    }

    public void CloseDrawer (bool isEnding)
    {
        drawer.SetActive(false);

        foreach (Transform child in drawerLocation)
        {
            Destroy(child.gameObject);
        }

        if (isEnding)
        {
            GameManager.Instance.ReadingLetterDesk();
        }
    }

}
