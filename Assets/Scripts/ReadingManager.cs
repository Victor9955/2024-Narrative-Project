using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadingManager : MonoBehaviour
{

    public TextMeshProUGUI titre;
    public TextMeshProUGUI corps;
    public RectTransform image;

    public void ReadingLetter(Letter letter)
    {
        titre.text = letter.title;
        corps.text = letter.pages[0];
        image.GetComponent<Image>().sprite = letter.letterType;
    }

    private void Update()
    {
        if (isActiveAndEnabled)
        {
            if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                Debug.Log("ca marche");
            }
        }
    }
}
