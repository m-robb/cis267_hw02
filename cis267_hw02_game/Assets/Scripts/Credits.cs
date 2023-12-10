using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public float speed;
    public float startPos;
    public float endPos;
    RectTransform rectTransform;
    public TextMeshProUGUI text;



    // Start is called before the first frame update
    void Start()
    {
        speed = 200f;
        startPos = -590f;
        endPos = 2880f;

        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(autoScrollText());

    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    IEnumerator autoScrollText()
    {
        while(rectTransform.localPosition.y < endPos)
        {
            rectTransform.Translate(Vector3.up * speed * Time.deltaTime);
            yield return null;
        }
    }
}
