using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<GameObject> letterToCome = new List<GameObject>();
    public List<GameObject> journalToCome = new List<GameObject>();

    public List<GameObject> letterDecrypted = new List<GameObject>();
    public List<GameObject> letterList = new List<GameObject>();

    private MovableUI _currentJournal;

    [Header("- SETUP -")]
    [SerializeField] private GameObject _letterParent;
    [SerializeField] private GameObject _deleteButtonPrefab;

    private MovableUI _letterDesk;

    [SerializeField] private List<GameObject> _objectsToHide;


    [Header("- MOVEPOS -")]
    [SerializeField] private int desktX;
    [SerializeField] private int mapX;
    [SerializeField] private float moveTime;
    [SerializeField] private Transform rootRef;
    public Button goToMapButton;
    [SerializeField] private Button goToDeskButton;

    [HideInInspector] public GameObject beingDragged;

    public int currentDay = 0;

    private static GameManager _instance;
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
        Application.targetFrameRate = 1000;
        SpawnNewsAndLetter();
        MapManager.OnClickLocation += MapManager_OnClickLocation; ;
        goToMapButton.onClick.AddListener(delegate { GoTo(mapX);});
        goToDeskButton.onClick.AddListener(delegate { GoTo(desktX);});
    }

    private void MapManager_OnClickLocation(bool obj)
    {
        SpawnNewsAndLetter();
        currentDay++;
        if (currentDay == 3)
        {
            if (obj)
            {

            }
            else
            {

            }
        }
    }

    public void SpawnNewsAndLetter ()
    {
        GameObject letter = Instantiate(letterToCome[0], _letterParent.transform);
        Journal journal = Instantiate(journalToCome[0], _letterParent.transform).GetComponent<Journal>();

        _letterDesk = letter.GetComponent<MovableUI>();
        _currentJournal = journal.GetComponent<MovableUI>();

        GameObject button = Instantiate(_deleteButtonPrefab, journal.anchor.transform);
        journal.button = button;

        letter.transform.SetAsLastSibling();
        journal.transform.SetAsLastSibling();

        letterToCome.RemoveAt(0);
    }

    public void ArchiveJournal(GameObject journal)
    {
        letterList.Add(journalToCome[0]);
        journalToCome.RemoveAt(0);
        Destroy(journal);
    }

    public void EndEnigme ()
    {
        if (_currentJournal != null)
        {
            ArchiveJournal(_currentJournal.gameObject);
        }
        if (_letterDesk != null)
        {
            Destroy(_letterDesk.gameObject);
        }

        letterList.Add(letterDecrypted[0]);
        letterDecrypted.RemoveAt(0);
    }

    public void ReadingLetterDesk ()
    {
        if (_currentJournal != null)
        {
            _currentJournal.gameObject.SetActive(true);
        }

        _letterDesk.gameObject.SetActive(true);
        HideObjects(false);
    }

    public void ClosingLetterDesk ()
    {
        if (_currentJournal != null)
        {
            _currentJournal.gameObject.SetActive(false);
            _currentJournal.transform.localScale = _currentJournal.oldTransformScale;
        }

        _letterDesk.transform.localScale = _letterDesk.oldTransformScale;
        _letterDesk.gameObject.SetActive(false);
        HideObjects(true);
    }


    void GoTo(int x)
    {
        rootRef.DOMoveX(x, moveTime);
    }

    private void HideObjects(bool isHiding)
    {
        foreach (GameObject toHide in _objectsToHide)
        {
            toHide.SetActive(!isHiding);
        }
    }
}
