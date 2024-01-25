using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<Letter> letterList = new List<Letter>();

    private static GameManager _instance;
    public static GameManager Instance
    {
        get { 
            if (_instance == null)
            {
                Debug.LogError("game manager est nul");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
}
