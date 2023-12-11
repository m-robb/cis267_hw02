using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public float speed;
    public float startPos;
    public float endPos;

    RectTransform rectTransform;
    public TextMeshProUGUI text;



    // Start is called before the first frame update
    void Start()
    {
        endPos = 1850f;

        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(autoScrollText());
    }

    private void Update()
    {
        startGame();
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            SceneManager.LoadScene("Hub");
        }
    }

    private void startGame()
    {
        if(rectTransform.localPosition.y >= endPos)
        {
            SceneManager.LoadScene("Hub");
        }
    }


    IEnumerator autoScrollText()
    {
        while (rectTransform.localPosition.y < endPos)
        {
            rectTransform.Translate(Vector3.up * speed * Time.deltaTime);
            yield return null;
        }
    }
}
