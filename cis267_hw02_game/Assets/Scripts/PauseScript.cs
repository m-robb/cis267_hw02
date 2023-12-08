using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private bool paused;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (paused == true)
            {
                Time.timeScale = 1.0f;
                pauseMenu.SetActive(false);
                paused = false;
            }
            else
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                paused = true;
            }
        }
    }
}
