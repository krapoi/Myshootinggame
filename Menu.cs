using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public GameObject HelpMenu;
    public void GoPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoHelp()
    {
        SceneManager.LoadScene(2);
    }

    public void GameDown()
    {
        Application.Quit();
    }
}
