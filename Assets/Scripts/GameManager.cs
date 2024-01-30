using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("- LETTERS -")]
    public List<GameObject> letterToCome = new List<GameObject>();
    public List<GameObject> letterDecrypted = new List<GameObject>();
    public List<GameObject> letterList = new List<GameObject>();

    [Header("- SETUP -")]
    [SerializeField] private GameObject _canvas;
    [SerializeField] private List<GameObject> _objectsToHide;

    //runtime
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

    private void HideObjects (bool isHiding)
    {
        foreach (GameObject toHide in _objectsToHide)
        {
            toHide.SetActive(!isHiding);
        }
    }

}
