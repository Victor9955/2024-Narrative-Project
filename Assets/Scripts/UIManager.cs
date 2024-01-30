using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("- SETUP DRAWER -")]
    [SerializeField] private GameObject _menuDrawer;
    [SerializeField] private GameObject _letterTemplate;
    [SerializeField] private Transform _drawerLocation;
    [SerializeField] private float _maxScrollRight;
    [SerializeField] private float _speedDrawer;

    [Header("- SETUP READING -")]
    [SerializeField] private ReadingManager _readingManager;

    //runtime
    private float maxScrollLeft;

    public void ReadLetter(GameObject letter)
    {
        _readingManager.gameObject.SetActive(true);
        CloseDrawer(false);
        _readingManager.ReadingLetter(letter);
    }

    public void CloseLetter ()
    {
        _readingManager.gameObject.SetActive(false);
        _readingManager.EndReadingLetter();

        OpenDrawer();
    }

    public void Update ()
    {
        if (_menuDrawer.activeSelf && Input.touchCount == 1)
        {
            float deltaTouch;

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                deltaTouch = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - Input.GetTouch(0).position).x;
                _drawerLocation.position = _drawerLocation.position - new Vector3(deltaTouch * _speedDrawer, 0, 0);
                _drawerLocation.localPosition = new Vector3(Mathf.Clamp(_drawerLocation.localPosition.x, maxScrollLeft, _maxScrollRight), _drawerLocation.position.y, _drawerLocation.position.z);
            }
        }
    }

    public void OpenDrawer ()
    {
        _menuDrawer.SetActive(true);
        GameManager.Instance.ClosingLetterDesk();

        List<GameObject> listLetters = GameManager.Instance.letterList;

        //setup a quel point on peut scroll vers la gauche
        maxScrollLeft = ((listLetters.Count - 1) * -500) + 1500;
        maxScrollLeft = Mathf.Clamp(maxScrollLeft, -10000000, 0);

        //remplis le tiroir de lettres
        for (int i = 0; i < listLetters.Count; i++)
        {
            GameObject letterDrawer = Instantiate(_letterTemplate, _drawerLocation.position, Quaternion.identity, _drawerLocation);
            LetterDrawer letter = letterDrawer.GetComponent<LetterDrawer>();

            letter.letterPrefab = listLetters[i];
            letter.Setup(this);
        }
    }

    public void CloseDrawer (bool isEnding)
    {
        _menuDrawer.SetActive(false);

        foreach (Transform child in _drawerLocation)
        {
            Destroy(child.gameObject);
        }

        if (isEnding)
        {
            GameManager.Instance.ReadingLetterDesk();
        }
    }

}
