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

    public void SetLanguage(int id)
    {
        switch (id)
        {
            case 0:
                GameSettings.language = "FR";
                break;
            case 1:
                GameSettings.language = "EN";
                break;
            case 2:
                GameSettings.language = "SP";
                break;
        }
    }

    public void ExitGame ()
    {
        Application.Quit();
    }
}
