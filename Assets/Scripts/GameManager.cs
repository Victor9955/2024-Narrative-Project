using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<GameObject> letterToCome = new List<GameObject>();

    public List<GameObject> letterDecrypted = new List<GameObject>();
    public List<GameObject> letterList = new List<GameObject>();

    [Header("- SETUP -")]
    [SerializeField] private float _movingSpeed;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _pipette;

    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _maxScaleZoom;
    private MovableUI _letterDesk;


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
    }

    private void CreateLetter ()
    {
        GameObject letter = Instantiate(letterToCome[0], _canvas.transform);
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

}
