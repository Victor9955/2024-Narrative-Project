using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuDrawer : MonoBehaviour, IPointerDownHandler
{
    public GameObject drawer;
    public UIManager manager;


    public void OnPointerDown(PointerEventData eventData)
    {
        drawer.SetActive(true);
        manager.PopulateDrawer();
    }

}
