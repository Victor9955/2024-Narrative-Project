using NaughtyAttributes;
using NaughtyAttributes.Test;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Button]
    void Please()
    {
        foreach (var item in GoogleSheetGetter.data.Keys)
        {
            // MyClass is inheritant from ScriptableObject base class
            DialogueScriptableObject example = ScriptableObject.CreateInstance<DialogueScriptableObject>();
            example.textKey = item;
            if (ContainsWord(item,"Player"))
            {
                example.name = "Éléon";
            }
            else if(ContainsWord(item, "Evangeline"))
            {
                example.name = "Évangéline";
            }
            else if(ContainsWord(item, "Jasper"))
            {
                example.name = "Jasper";
            }

            // path has to start at "Assets"
            string path = "Assets/DialoguesFinalFinal/" + item + ".asset";
            AssetDatabase.CreateAsset(example, path);
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = example;

        }
        
    }

    static bool ContainsWord(string inputString, string targetWord)
    {
        return inputString.IndexOf(targetWord, StringComparison.OrdinalIgnoreCase) >= 0;
    }
}
