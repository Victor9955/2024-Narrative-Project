using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicManager : MonoBehaviour
{
    [SerializeField] Button micButton;
    [SerializeField] Button buttonOui;
    [SerializeField] Button buttonNon;
    [SerializeField] GameObject micWindow;
    [SerializeField] List<GameObject> objectsToHide;
    bool _isShown = false;

    private void Start()
    {
        micButton.onClick.AddListener(ShowFinishedChoice);
        buttonOui.onClick.AddListener(delegate { Answer(true); });
        buttonNon.onClick.AddListener(delegate { Answer(false); });
        DialogueManager.OnFinishedDialogue += DialogueManager_OnFinishedDialogue;
    }

    private void DialogueManager_OnFinishedDialogue()
    {
        foreach (var item in objectsToHide)
        {
            item.SetActive(true);
        }
    }

    void ShowFinishedChoice()
    {
        if(!_isShown)
        {
            _isShown = true;
            micWindow.SetActive(true);
        }
        else
        {
            _isShown = false;
            micWindow.SetActive(false);
        }
    }

    void Answer(bool choice)
    {
        if(choice)
        {
            _isShown = false;
            micWindow.SetActive(false);
            foreach (var item in objectsToHide)
            {
                item.SetActive(false);
            }
            DialogueStarter.instance.StartTalking(GameManager.Instance.currentDay);
            GameManager.Instance.EndEnigme();
        }
        else
        {
            _isShown = false;
            micWindow.SetActive(false);
        }
    }
}
