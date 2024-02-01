using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    public GameObject anchor;
    [HideInInspector] public GameObject button;

    private void Start()
    {
        if (button != null)
        {
            button.GetComponent<Button>().onClick.AddListener(DeleteJournal);
        }
    }

    public void DeleteJournal ()
    {
        GameManager.Instance.ArchiveJournal(gameObject);
        button.GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
