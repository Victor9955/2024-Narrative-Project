using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterDrawer : MonoBehaviour, IPointerClickHandler
{
    public GameObject letter;
    public UIManager manager;

    [SerializeField] private RawImage image;

    public void Setup (UIManager managerScript)
    {
        image.texture = letter.GetComponent<RawImage>().texture;
        manager = managerScript;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        manager.ReadLetter(letter);
    }
}
