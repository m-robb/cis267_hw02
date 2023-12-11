using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		GameObject dontDestroyOnLoad;

		dontDestroyOnLoad = GameObject.Find("DontDestroyOnLoad");

		if (dontDestroyOnLoad != null) {
			Destroy(dontDestroyOnLoad);
		}
    }

    public void playAgain()
    {
        SceneManager.LoadScene("Hub");
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
