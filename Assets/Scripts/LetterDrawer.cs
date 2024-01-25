using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterDrawer : MonoBehaviour, IPointerClickHandler
{
    public Letter letter;
    public UIManager manager;

    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Image image;

    public void Setup (UIManager managerScript)
    {
        textMeshPro.text = letter.title;
        image.sprite = letter.letterType;
        manager = managerScript;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        manager.ReadLetter(letter);
    }
}
