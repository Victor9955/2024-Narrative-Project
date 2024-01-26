using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "write/Dialogue")]
public class DialogueScriptableObject : ScriptableObject
{
    [Header("Character")]
    public string characterName;
    public Sprite characterIcon;

    [Header("Character Text")]
    public string textKey;
    public float talkSpeed = 5f;

    [Header("Choix")]
    public string goodChoice;
    public string badChoice;

    public DialogueScriptableObject nextGood;
    public DialogueScriptableObject nextBad;
}
