using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    DialogueScriptableObject current;
    [SerializeField] DialogueScriptableObject test;


    [Header("References")]

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] Image characterIcon;
    [SerializeField] Button goodChoice;
    [SerializeField] Button badChoice;
    [SerializeField] CanvasGroup group;
    [SerializeField] float showSpeed = 2f;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        goodChoice.onClick.AddListener(GoodResponse);
        badChoice.onClick.AddListener(BadResponse);
    }

    [Button]
    void TestDialogue()
    {
        BeginDialogue(test);
    }


    public void BeginDialogue(DialogueScriptableObject m_dialogue)
    {
        if (!GoogleSheetGetter.isFinished) return;
        current = m_dialogue;
        characterIcon.sprite = current.characterIcon;
        characterName.text = current.characterName;
        goodChoice.GetComponentInChildren<TextMeshProUGUI>().text = current.goodChoice;
        badChoice.GetComponentInChildren<TextMeshProUGUI>().text = current.badChoice;
        StartCoroutine(Talk(GoogleSheetGetter.data[current.textKey][GameSettings.language], current.talkSpeed));
    }

    IEnumerator Talk(string m_text, float m_speed)
    {
        float timer = 0f;
        text.text = m_text;
        text.maxVisibleCharacters = 0;
        text.ForceMeshUpdate();
        while (timer <= 1f)
        {
            yield return new WaitForFixedUpdate();
            timer += Time.deltaTime * (m_speed / m_text.Length);
            text.maxVisibleCharacters = (int)(m_text.Length * timer);
            text.ForceMeshUpdate();
        }

        text.maxVisibleCharacters = m_text.Length;
    }

    [Button]
    void Show()
    {
        DOVirtual.Float(0f, 1f, showSpeed, x => group.alpha = x);
    }

    [Button]
    void Hide()
    {
        DOVirtual.Float(1f, 0f, showSpeed, x => group.alpha = x);
    }
    void GoodResponse()
    {
        if(current.nextGood != null)
        {
            BeginDialogue(current.nextGood);
        }
    }

    void BadResponse()
    {
        if (current.nextBad != null)
        {
            BeginDialogue(current.nextBad);
        }
    }

    [Button]
    void SetLanguageToFrench()
    {
        GameSettings.language = "FR";
    }
    [Button]
    void SetLanguageToEnglish()
    {
        GameSettings.language = "EN";
    }
    [Button]
    void SetLanguageToEspagnol()
    {
        GameSettings.language = "SP";
    }
}