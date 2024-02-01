using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    [SerializeField] Button myButton;
    [SerializeField] bool canBeValid = false;
    [SerializeField] int day = 0;

    private void Start()
    {
        myButton.onClick.AddListener(OnClick);
        MapManager.ShowMap += MapManager_ShowMap;
        myButton.interactable = false;
    }

    private void MapManager_ShowMap()
    {
        myButton.interactable = true;
    }

    void OnClick()
    {
        if(canBeValid)
        {
            MapManager.InvokeOnClickLocation(day == GameManager.Instance.currentDay);
        }
        else
        {
            MapManager.InvokeOnClickLocation(false);
        }
    }
}
