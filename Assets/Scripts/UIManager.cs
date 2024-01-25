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

    [SerializeField] private GameObject letterTemplate;

    public void ReadLetter(Letter letter)
    {
        readingManager.gameObject.SetActive(true);
        CloseDrawer();
        readingManager.ReadingLetter(letter);
    }

    public void CloseDrawer ()
    {
        drawer.SetActive(false);
        DestroyDrawer();
    }

    public void CloseLetter ()
    {
        reading.SetActive(false);
    }

    public void Update ()
    {
        if (drawer.activeSelf)
        {

        }
    }

    private void DestroyDrawer ()
    {
        foreach (Transform child in drawerLocation)
        {
            Destroy(child.gameObject);
        }
    }

    public void PopulateDrawer ()
    {
        List<Letter> listLetters = GameManager.Instance.letterList;

        for (int i = 0; i < listLetters.Count; i++)
        {
            GameObject letterDrawer = Instantiate(letterTemplate, drawerLocation.position, Quaternion.identity, drawerLocation);
            LetterDrawer letter = letterDrawer.GetComponent<LetterDrawer>();

            letter.letter = listLetters[i];
            letter.Setup(this);
        }
    }
}
