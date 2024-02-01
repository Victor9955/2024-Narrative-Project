using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogueType
{
    Choice,
    Wait,
    ShowMap
}


[CreateAssetMenu(menuName = "write/Dialogue")]
public class DialogueScriptableObject : ScriptableObject
{
    [Header("Character")]
    public string characterName;
    public Sprite characterIcon;

    [Header("Character Text")]
    public string textKey;
    public float talkSpeed = 25f;


    public DialogueType type;


    bool isWait
    {
        get
        {
            return type == DialogueType.Wait || type == DialogueType.ShowMap;
        }
    }


    [ShowIf("isWait")]
    public float waitTime = 4f;
    [ShowIf("isWait")]
    public DialogueScriptableObject next;


    [Header("Choix")]
    public string goodChoice;
    public string badChoice;


    public DialogueScriptableObject nextGood;
    public DialogueScriptableObject nextBad;
}
