using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public static DialogueStarter instance;


    [SerializeField] List<DialogueScriptableObject> dialogues = new List<DialogueScriptableObject>();

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    [Button]
    void TestFirst()
    {
        StartTalking(0);
    }



    public void StartTalking(int index)
    {
        //Debug.Log(index);
        if(index <= dialogues.Count - 1)
        {
            DialogueManager.instance.Show();
            DialogueManager.instance.BeginDialogue(dialogues[index]);
        }
        
    }
}
