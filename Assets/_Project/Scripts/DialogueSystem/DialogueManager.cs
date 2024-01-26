using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public DialogueScriptableObject test;


    [Header("References")]

    public TextMeshProUGUI text;
    public TextMeshProUGUI characterName;
    public Image characterIcon;
    public Button goodChoice;
    public Button badChoice;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    [Button("Test Dialogue")]
    public void BeginDialogue()
    {
        StartCoroutine(Talk("Je vais te taper", 5));
    }

    IEnumerator Talk(string m_text, float m_speed)
    {
        float beginTime = Time.time;
        float endTime = beginTime + m_text.Length / m_speed;
        text.text = m_text;
        text.maxVisibleCharacters = 0;
        text.ForceMeshUpdate();
        while (Time.time < endTime)
        {
            beginTime += Time.deltaTime;
            text.maxVisibleCharacters = (int)(m_text.Length * (beginTime / endTime));
            yield return null;
        }
        text.maxVisibleCharacters = m_text.Length;
    }
}