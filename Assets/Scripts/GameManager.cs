using DG.Tweening;
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

    [Header("- MOVEPOS -")]
    [SerializeField] private int desktX;
    [SerializeField] private int mapX;
    [SerializeField] private float moveTime;
    [SerializeField] private Transform rootRef;
    [SerializeField] private Button goToMapButton;
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
        CreateLetter();
        goToMapButton.onClick.AddListener(delegate { GoTo(mapX);});
        goToDeskButton.onClick.AddListener(delegate { GoTo(desktX);});
    }

    private void CreateLetter ()
    {
        GameObject letter = Instantiate(letterToCome[0], _letterParent.transform);
        _letterDesk = letter.GetComponent<MovableUI>();
        letter.transform.SetSiblingIndex(transform.GetSiblingIndex() - 1);
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
        _pipette.SetActive(true);
    }

    public void ClosingLetterDesk ()
    {
        _letterDesk.transform.localScale = _letterDesk.oldTransformScale;
        _letterDesk.gameObject.SetActive(false);
        _pipette.SetActive(false);
    }


    void GoTo(int x)
    {
        rootRef.DOMoveX(x, moveTime);
    }
}
