using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicManager : MonoBehaviour
{
    [SerializeField] Button micButton;

    private void Start()
    {
        micButton.onClick.AddListener(ShowFinishedChoice);
    }

    void ShowFinishedChoice()
    {

    }
}
