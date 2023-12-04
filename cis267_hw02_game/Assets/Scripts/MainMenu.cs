using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void startGame()
    {
        SceneManager.LoadScene("Hub");
    }

    public void controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
