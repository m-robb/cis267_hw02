using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCDialog : MonoBehaviour
{
    public GameObject dialogPanel;
    public TMP_Text dialogText;
    public TMP_Text dialogName;
    public Image dialogFace;
    private bool nearNPC;
    public string npcName;
    public string npcText;
    public Sprite npcFace;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nearNPC == true && Input.GetAxis("Jump") > 0)
        {
            dialogText.text = npcText;
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
        if(collision.gameObject.CompareTag("Player"))
        {
            nearNPC = false;
        }
    }
}
