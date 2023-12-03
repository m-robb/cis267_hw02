using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPCBool : MonoBehaviour
{
    private bool nearObject;
    public GameObject questObject;
    public NPCDialog2 npcDialog;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (nearObject == true && Input.GetAxis("Jump") > 0)
        {
            npcDialog.quest = true;
            Destroy(questObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nearObject = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nearObject = false;
        }
    }
}
