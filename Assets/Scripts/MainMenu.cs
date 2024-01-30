using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GetToLevel (int level)
    {
        if (level != 0)
        {
            SceneManager.LoadScene(level);
        }
    }

    public void ExitGame ()
    {
        Application.Quit();
    }
}
