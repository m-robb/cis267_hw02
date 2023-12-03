using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialog2 : MonoBehaviour
{
    public GameObject dialogPanel;
    public TMP_Text dialogText;
    public TMP_Text dialogName;
    public Image dialogFace;
    private bool nearNPC;
    public string npcName;
    public string npcText;
    public string npcText2;
    public Sprite npcFace;
    public bool quest;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (nearNPC == true && Input.GetAxis("Jump") > 0)
        {
            if (quest == false)
            {
                dialogText.text = npcText;
            }
            if (quest == true)
            {
                dialogText.text = npcText2;
            }
            dialogName.text = npcName;
            dialogFace.sprite = npcFace;
            dialogPanel.SetActive(true);
        }
        if (nearNPC == false)
        {
            dialogPanel.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nearNPC = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nearNPC = false;
        }
    }
}
