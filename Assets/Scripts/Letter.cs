using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "write/letter")]
public class Letter : ScriptableObject
{
    public Sprite letterType;
    public string title;
    public List<string> pages = new List<string>();
}
