using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideManager : MonoBehaviour
{
    [SerializeField] Button showGuideButton;
    [SerializeField] float hideX;
    [SerializeField] float showX;
    [SerializeField] float moveTime = 0.75f;
    [SerializeField] bool isStartShown;
    [SerializeField] List<GameObject> objectsToHide = new List<GameObject>();
    bool isShown;

    private void Start()
    {
        isShown = isStartShown;
        showGuideButton.onClick.AddListener(ShowGuide);
    }

    private void ShowGuide()
    {
        if(!isShown)
        {
            transform.DOMoveX(showX, moveTime);
            isShown = true;
            objectsToHide.ForEach(obj => obj.SetActive(false));
        }
        else if(isShown)
        {
            transform.DOMoveX(hideX, moveTime);
            isShown = false;
            objectsToHide.ForEach(obj => obj.SetActive(true));
        }
    }

    [Button]
    void GetPos()
    {
        Debug.Log(transform.position.x);
    }
}
