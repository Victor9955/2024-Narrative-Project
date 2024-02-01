using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterDrawer : MonoBehaviour, IPointerClickHandler
{
    public GameObject letterPrefab;
    [SerializeField] private UIManager _manager;
    [SerializeField] private RawImage _image;

    public void Setup (UIManager managerScript)
    {
        Debug.Log(letterPrefab);
        _image.texture = letterPrefab.GetComponent<RawImage>().texture;
        _manager = managerScript;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _manager.ReadLetter(letterPrefab);
    }
}
