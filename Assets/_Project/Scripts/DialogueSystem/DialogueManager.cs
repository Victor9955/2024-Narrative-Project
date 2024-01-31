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


    [Header("References")]

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] Image characterIcon;
    [SerializeField] Button goodChoice;
    [SerializeField] Button badChoice;
    [SerializeField] CanvasGroup group;
    [SerializeField] float showSpeed = 2f;

    Coroutine coroutine;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        group.interactable = false;
        group.blocksRaycasts = false;
        goodChoice.onClick.AddListener(GoodResponse);
        badChoice.onClick.AddListener(BadResponse);
    }

    public void BeginDialogue(DialogueScriptableObject m_dialogue)
    {
        if (!GoogleSheetGetter.isFinished) return;
        if (coroutine != null) return;
        current = m_dialogue;
        characterIcon.sprite = current.characterIcon;
        characterName.text = current.characterName;

        if(current.nextGood == null)
        {
            goodChoice.gameObject.SetActive(false);
        }
        else
        {
            goodChoice.gameObject.SetActive(true);
            goodChoice.GetComponentInChildren<TextMeshProUGUI>().text = current.goodChoice;
        }

        if (current.nextBad == null)
        {
            badChoice.gameObject.SetActive(false);
        }
        else
        {
            badChoice.gameObject.SetActive(true);
            badChoice.GetComponentInChildren<TextMeshProUGUI>().text = current.badChoice;
        }


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

        if (current.nextBad == null && current.nextGood == null)
        {
            yield return new WaitForSeconds(3f);
            Hide();
        }
    }

    [Button]
    public void Show()
    {
        DOVirtual.Float(0f, 1f, showSpeed, x => group.alpha = x);
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    [Button]
    void Hide()
    {
        DOVirtual.Float(1f, 0f, showSpeed, x => group.alpha = x);
        group.interactable = false;
        group.blocksRaycasts = false;
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