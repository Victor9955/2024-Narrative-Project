using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<GameObject> letterToCome = new List<GameObject>();

    public List<GameObject> letterDecrypted = new List<GameObject>();
    public List<GameObject> letterList = new List<GameObject>();

    [Header("- SETUP -")]
    [SerializeField] private GameObject _letterParent;
    [SerializeField] private GameObject _pipette;

    private MovableUI _letterDesk;

    [SerializeField] private List<GameObject> _objectsToHide;


    [Header("- MOVEPOS -")]
    [SerializeField] private int desktX;
    [SerializeField] private int mapX;
    [SerializeField] private float moveTime;
    [SerializeField] private Transform rootRef;
    public Button goToMapButton;
    [SerializeField] private Button goToDeskButton;


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
        CreateLetter();
        goToMapButton.onClick.AddListener(delegate { GoTo(mapX);});
        goToDeskButton.onClick.AddListener(delegate { GoTo(desktX);});
    }

    private void CreateLetter ()
    {
        GameObject letter = Instantiate(letterToCome[0], _letterParent.transform);
        _letterDesk = letter.GetComponent<MovableUI>();
        letter.transform.SetAsLastSibling();
        letterToCome.RemoveAt(0);

        //enlever ici et mettre lorsque l'on lance le dialogue
        EndLetterEnigme();
    }

    public void EndLetterEnigme ()
    {
        letterList.Add(letterDecrypted[0]);
        letterDecrypted.RemoveAt(0);
    }

    public void ReadingLetterDesk ()
    {
        _letterDesk.gameObject.SetActive(true);
        HideObjects(false);
    }

    public void ClosingLetterDesk ()
    {
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
