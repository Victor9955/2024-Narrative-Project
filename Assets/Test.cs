using NaughtyAttributes;
using NaughtyAttributes.Test;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour
{
    public List<DialogueScriptableObject> list = new List<DialogueScriptableObject>();
    [SerializeField] Sprite evangeline;
    [SerializeField] Sprite jasper;
    [SerializeField] Sprite player;


    [Button]
    void Please()
    {
        foreach (var item in GoogleSheetGetter.data.Keys)
        {
            // MyClass is inheritant from ScriptableObject base class
            DialogueScriptableObject example = ScriptableObject.CreateInstance<DialogueScriptableObject>();
            example.textKey = item;

            // path has to start at "Assets"
            string path = "Assets/DialoguesFinalFinal/" + item + ".asset";
            AssetDatabase.CreateAsset(example, path);
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = example;

        }
        
    }


    [Button]
    void TestNoun()
    {
        foreach (var item in list)
        {
            foreach (var item2 in item.textKey)
            {
                if(item2 == 'P')
                {
                    item.characterName = "Éléon";
                    item.characterIcon = player;
                    break;
                }
                else if(item2 == 'E')
                {
                    item.characterName = "Évengeline";
                    item.characterIcon = evangeline;
                    break;
                }
                else if (item2 == 'J')
                {
                    item.characterName = "Jasper";
                    item.characterIcon = jasper;
                    break;
                }
            }
        }
    }
}
