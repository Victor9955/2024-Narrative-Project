using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuDrawer : MonoBehaviour, IPointerDownHandler
{
    public UIManager manager;

    public void OnPointerDown(PointerEventData eventData)
    {
        manager.OpenDrawer();
    }

}
